// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Menu;
using BattleShips.Misc;
using BattleShips.Ships.Generators;

using TicTacToe;

namespace BattleShips.Models
{
    public class BattleshipsGame : IBattleshipGame
    {
        private readonly object _syncRoot = new object();

        private string _answer = "";
        private ShipDirection _shipDirection = ShipDirection.Horizontal;
        private IShip _tempShip;

        private IGameMenu _gameMenu;
        private Point _currentPosition;

        private BattleShipsState _currentState = BattleShipsState.Menu;

        private IShell _shell;
        private IPlayer _player;
        private IPlayer _ai;
        private List<Keys> _availableKeys;

        public BattleshipsGame()
        {
            _shell = new ConsoleShell();
            AddAvailibleKeys();
            _shell.RegisterKeyFilter(FilterKeys);

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
                PreGameSettings();
            }

            _shell.KeyPressed += OnShellKeyPressed;
            _shell.StartRunLoop();
        }

        public string GetAboutText()
        {
            return "For Global Logic trainee course by Andrii Liaskun";
        }

        private void AddAvailibleKeys()
        {
            _availableKeys = new List<Keys>();
            _availableKeys.Add(Keys.Up);
            _availableKeys.Add(Keys.Down);
            _availableKeys.Add(Keys.Left);
            _availableKeys.Add(Keys.Right);
            _availableKeys.Add(Keys.Escape);
            _availableKeys.Add(Keys.Enter);
            _availableKeys.Add(Keys.A);
            _availableKeys.Add(Keys.S);
            _availableKeys.Add(Keys.C);
            _availableKeys.Add(Keys.L);
            _availableKeys.Add(Keys.Q);
            _availableKeys.Add(Keys.N);
        }

        #region Implementation details

        private IBattleShipBoard ActiveBoard => _currentState == BattleShipsState.Game ? _player.PolygonBoard : _player.Board;

        /// <summary>
        /// This method must ask some questions for random create board or by myself
        /// </summary>
        /// <param name="player">The player</param>
        private void PreGameSettings()
        {
            do
            {
                _shell.PrintText("Do you want to randomly place ships? (enter y/n)").EndLine();

                _answer = _shell.ReadText().ToLower();
                _shell.Clear();
            }
            while (_answer != "y" && _answer != "n");

            // Change State to CreateShips
            _currentState = BattleShipsState.CreateShip;
            _ai.FillBoard();

            _currentPosition = new Point();
            _player.ShowBoards();
        }

        private void BackToMenu()
        {
            SwitchState(BattleShipsState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(KeyboardHookEventArgs arg)
        {
            var step = GameConstants.BoardMeasures.Step;
            var newPoint = _currentPosition;

            if (arg.KeyCode == Keys.Up || arg.KeyCode == Keys.Down)
                newPoint.Y += arg.KeyCode == Keys.Up ? -step : step;

            if (arg.KeyCode == Keys.Left || arg.KeyCode == Keys.Right)
                newPoint.X += arg.KeyCode == Keys.Left ? -step : step;

            if (GameConstants.BoardMeasures.IsInValidRange(newPoint.X)
                && GameConstants.BoardMeasures.IsInValidRange(newPoint.Y))
                _currentPosition = newPoint;
        }

        private void HandleShipCreation(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tempShip != null)
            {
                if (_tempShip.IsValid)
                {
                    _tempShip?.Freeze();
                    _tempShip = null;
                }
                else
                {
                    return;
                }
            }

            if (_tempShip is null)
            {
                _tempShip = _player.CreateShip(_currentPosition);
                if (_tempShip != null)
                {
                    _tempShip.ChangeDirection(_shipDirection);
                    ActiveBoard.MoveShip(_currentPosition, _tempShip, _shipDirection);
                }

                if (_tempShip is null)
                {
                    _currentPosition = new Point();
                    _currentState = BattleShipsState.Game;
                }
            }
            else
                ActiveBoard.MoveShip(_currentPosition, _tempShip, _shipDirection);

            ActiveBoard.SetCursor(_currentPosition);
        }

        private void GameControl(KeyboardHookEventArgs e)
        {
            SetGamePoint(e);

            ActiveBoard.SetCursor(_currentPosition);
        }

        private void ChangeDirection(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                if (_shipDirection == ShipDirection.Horizontal)
                    _shipDirection = ShipDirection.Vertical;
                else
                    _shipDirection = ShipDirection.Horizontal;
            }
        }

        private bool FilterKeys(KeyboardHookEventArgs args)
        {
            return _availableKeys.Contains(args.KeyCode);
        }

        private void RandomPlaceShips(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _player.Board.Ships.Count != 0)
            {
                _currentState = BattleShipsState.Game;
                return;
            }
            else
            {
                var config = new PlayerBoardConfig(new Point());
                _player = new Player(_shell, config);
            }
            _player.FillBoard();
            _player.ShowBoards();
            _shell.PrintText("Press any arrow keys to replace ships. Or press \"ENTER\" to Start Game", new Point(30, 25));
        }

        private void MakeShoot()
        {
            var isEmpty = _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == GameConstants.Empty;
            var isAlive = true;
            _ai.Board.ProcessShot(_currentPosition);
            foreach (var item in _ai.Board.Ships)
            {
                if (item.Includes(_currentPosition))
                    if (!item.IsAlive)
                    {
                        isAlive = false;
                        continue;
                    }
            }
            _player.MakeShot(_currentPosition, isEmpty, isAlive);
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
                            if (_answer == "y")
                            {
                                ChangeDirection(e);
                                HandleShipCreation(e);
                            }
                            else
                            {
                                RandomPlaceShips(e);
                            }
                        }
                        else
                        {
                            if (e.KeyCode == Keys.Enter)
                            {
                                MakeShoot();
                            }
                            else
                            {
                                _player.MakeMove(_currentPosition);
                                ActiveBoard.SetCursor(_currentPosition);
                            }
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
                    _shell.SetCursorPosition(new Point(0, 30));
                    _shell.PrintText("ERROR:" + ex).EndLine();
                }
            }
        }

        #endregion Implementation details
    }
}