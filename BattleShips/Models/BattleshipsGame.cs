// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Menu;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    public class BattleshipsGame : IBattleshipGame
    {
        private readonly object _syncRoot = new object();

        private string _answer = "";
        private Direction _shipDirection = Direction.Horizontal;
        private IShip _tempShip;

        private IGameMenu _gameMenu;
        private Point _currentPosition;

        private BattleShipsState _currentState = BattleShipsState.Menu;

        private IShell _shell;
        private IPlayer _player;
        private IPlayer _ai;

        public BattleshipsGame()
        {
            _shell = new ConsoleShell();
            _gameMenu = new GameMenuBar(_shell, this);

            var config = new PlayerBoardConfig(new Point());

            _player = new Player(_shell, config);
            _ai = new AiPlayer(_shell, config);
        }

        protected bool IsCreation => _currentState == BattleShipsState.CreateShip;

        public void SwitchState(BattleShipsState state) => _currentState = state;

        public void StartGame()
        {
            _shell.KeyPressed -= OnShellKeyPressed;
            _shell.Clear();

            if (_currentState == BattleShipsState.Menu)
            {
                _gameMenu.Print();
            }
            else if (_currentState == BattleShipsState.Game)
            {
                PreGameSettings(_player);
            }

            _shell.KeyPressed += OnShellKeyPressed;
            _shell.StartRunLoop();
        }

        public string GetAboutText()
        {
            return "For Global Logic trainee course by Andrii Liaskun";
        }

        #region Implementation details

        /// <summary>
        /// This method must ask some questions for random create board or by myself
        /// </summary>
        /// <param name="player">The player</param>
        private void PreGameSettings(IPlayer player)
        {
            do
            {
                _shell.PrintText("Do you want to randomly place ships? (enter y/n)").EndLine();

                _answer = _shell.ReadText().ToLower();
                _shell.Clear();
            }
            while (_answer != "y" && _answer != "n");

            // Change State to CreateShips
            if (_answer == "y")
                _currentState = BattleShipsState.CreateShip;
            else
                _currentState = BattleShipsState.Game;

            _currentPosition = IsCreation ? new Point(3, 4) : new Point(47, 4);
            _player.ShowBoards();
        }

        private void BackToMenu()
        {
            SwitchState(BattleShipsState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(KeyboardHookEventArgs e)
        {
            var maxHeight = !IsCreation ? GameConstants.EnemyBoard.MaxHeight : GameConstants.PlayerBoard.MaxHeight;
            var minHeight = !IsCreation ? GameConstants.EnemyBoard.MinHeight : GameConstants.PlayerBoard.MinHeight;
            var maxWidth = !IsCreation ? GameConstants.EnemyBoard.MaxWidth : GameConstants.PlayerBoard.MaxWidth;
            var minWidth = !IsCreation ? GameConstants.EnemyBoard.MinWidth : GameConstants.PlayerBoard.MinWidth;

            if (e.KeyCode == Keys.Up && _currentPosition.Y > minHeight)
                _currentPosition.Y += GameConstants.Step.Up;

            if (e.KeyCode == Keys.Down && _currentPosition.Y < maxHeight)
                _currentPosition.Y += GameConstants.Step.Down;

            if (e.KeyCode == Keys.Left && _currentPosition.X > minWidth)
                _currentPosition.X += GameConstants.Step.Left;

            if (e.KeyCode == Keys.Right && _currentPosition.X < maxWidth)
                _currentPosition.X += GameConstants.Step.Right;
        }

        private void HandleShipCreation(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tempShip != null)
            {
                _player.AddShip(_tempShip);
                _tempShip?.Freeze();
                _tempShip = null;
            }

            if (_tempShip is null)
            {
                _tempShip = _player.CreateShip(_currentPosition);

                if (_tempShip is null)
                {
                    _currentPosition = new Point(47, 4);
                    _currentState = BattleShipsState.Game;
                }
            }
            else
            {
                _player.Board.MoveShip(_currentPosition, _tempShip, _shipDirection);
            }
            _shell.SetCursorPosition(_currentPosition);
        }

        private void GameControl(KeyboardHookEventArgs e)
        {
            SetGamePoint(e);
            _shell.SetCursorPosition(_currentPosition);
        }

        private void ChangeDirection(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                if (_shipDirection == Direction.Horizontal)
                    _shipDirection = Direction.Vertical;
                else
                    _shipDirection = Direction.Horizontal;
            }
        }

        private void OnShellKeyPressed(object sender, KeyboardHookEventArgs e)
        {
            // NOTE: if not put a lock here
            // then we have a lot of threads and actually synchronization issues
            // because each thread tries to write and thus will eventually just
            // spoil our data
            // ALSO KNOWS AS 'RACE CONDITION'
            lock (_syncRoot)
            {
                try
                {
                    if (e.KeyCode == Keys.Escape)
                        BackToMenu();

                    if (_currentState == BattleShipsState.Game || _currentState == BattleShipsState.CreateShip)
                    {
                        GameControl(e);
                        if (_currentState == BattleShipsState.CreateShip)
                        {
                            ChangeDirection(e);
                            HandleShipCreation(e);
                        }
                        else
                        {
                            _player.MakeMove(_currentPosition);
                            _shell.SetCursorPosition(_currentPosition);
                        }
                    }
                    else
                    {
                        if (_currentState == BattleShipsState.Menu)
                            _gameMenu.HandleKey(e.KeyCode);
                    }
                }
                catch (Exception ex)
                {
                    _shell.PrintText("ERROR:" + ex).EndLine();
                }
            }
        }

        #endregion Implementation details
    }
}