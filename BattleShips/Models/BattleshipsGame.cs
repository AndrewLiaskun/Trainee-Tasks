// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Menu;
using BattleShips.Misc;
using BattleShips.Utils;

using TicTacToe;

using static BattleShips.Misc.GameConstants;
using static BattleShips.Misc.GameConstants.BoardMeasures;
using static BattleShips.Resources.Serialization;
using static BattleShips.Resources.ResetQuestion;
using static BattleShips.Resources.EndGame;

namespace BattleShips.Models
{
    public class BattleshipsGame : IBattleshipGame
    {
        private readonly object _syncRoot = new object();

        private bool _answer;
        private ShipDirection _shipDirection = ShipDirection.Horizontal;
        private IShip _tempShip;

        private IGameMenu _gameMenu;
        private Point _currentPosition;

        private BattleShipsState _currentState = BattleShipsState.Menu;

        private IShell _shell;
        private IPlayer _player;
        private IPlayer _ai;
        private List<Keys> _availableKeys;

        private ShootAlgorithm _aiShooter;
        private ShootAlgorithm _playerShooter;

        public BattleshipsGame()
        {
            _shell = new ConsoleShell();
            AddAvailibleKeys();
            _shell.RegisterKeyFilter(FilterKeys);

            _gameMenu = new GameMenuBar(_shell, this);

            var config = new PlayerBoardConfig(new Point());

            _player = new Player(_shell, config);
            _ai = new AiPlayer(_shell, config);

            _aiShooter = new ShootAlgorithm();
            _playerShooter = new ShootAlgorithm();
        }

        protected bool IsCreation => _currentState == BattleShipsState.CreateShip;

        public void SwitchState(BattleShipsState state) => _currentState = state;

        public void Start()
        {
            _shell.KeyPressed -= OnShellKeyPressed;

            _gameMenu.Print();

            _shell.KeyPressed += OnShellKeyPressed;
            _shell.StartRunLoop();
        }

        public void StartNewGame()
        {
            SwitchState(BattleShipsState.Game);

            _shell.Clear();

            Reset();
        }

        public void Resume()
        {
            SwitchState(BattleShipsState.Game);
            _shell.Clear();
            _player.ShowBoards();
        }

        public string GetAboutText()
        {
            return Resources.AboutAuthor.Text;
        }

        #region Implementation details

        private IBattleShipBoard ActiveBoard => _currentState == BattleShipsState.Game ? _player.PolygonBoard : _player.Board;

        public void LoadGame()
        {
            SwitchState(BattleShipsState.LoadGame);
            _shell.Clear();
            _shell.SetForegroundColor(ShellColor.Yellow);
            _shell.PrintTextInCenter(LoadPath, new Point());

            _shell.PrintTextInCenter(string.Empty, new Point(0, 3));

            var path = _shell.ReadText();

            if (GameSerializer.TryLoad(path, out var game))
            {
                _player.Load(game.Players[0]);
                _ai.Load(game.Players[1]);
                Resume();
            }
            else _shell.PrintTextInCenter(PathEx, new Point(0, 5));
        }

        public void SaveGame()
        {
            SwitchState(BattleShipsState.SaveGame);
            _shell.Clear();
            _shell.SetForegroundColor(ShellColor.Yellow);

            _shell.PrintTextInCenter(SavePath, new Point());
            _shell.PrintTextInCenter(string.Empty, new Point(0, 3));

            var path = _shell.ReadText();

            if (GameSerializer.TrySave(GameMetadata.FromGame(_player, _ai), path))
                _shell.PrintTextInCenter(SuccessfulSave, new Point(0, 5));
            else
                _shell.PrintTextInCenter(PathEx, new Point(0, 5));
            _shell.ResetColor();
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
            _availableKeys.Add(Keys.R);
            _availableKeys.Add(Keys.D);
        }

        /// <summary>
        /// This method must ask some questions for random create board or by myself
        /// </summary>
        /// <param name="player">The player</param>
        private void Reset()
        {
            _player.Reset();
            _ai.Reset();
            string answer = string.Empty;

            _shell.SetForegroundColor(ShellColor.Red);
            do
            {
                _shell.PrintTextInCenter(Question, new Point());

                _shell.PrintTextInCenter(string.Empty, new Point(0, 2));
                answer = _shell.ReadText().Trim().ToLower();

                _shell.Clear();
            }
            while (answer != PositiveAnswer && answer != NegativeAnswer);

            _answer = answer == PositiveAnswer;

            _shell.ResetColor();

            // Change State to CreateShips
            SwitchState(BattleShipsState.CreateShip);
            _ai.FillShips();

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
            var step = BoardMeasures.Step;
            var newPoint = _currentPosition;

            if (arg.KeyCode == Keys.Up || arg.KeyCode == Keys.Down)
                newPoint.Y += arg.KeyCode == Keys.Up ? -step : step;

            if (arg.KeyCode == Keys.Left || arg.KeyCode == Keys.Right)
                newPoint.X += arg.KeyCode == Keys.Left ? -step : step;

            if (IsInValidRange(newPoint.X)
                && IsInValidRange(newPoint.Y))
                _currentPosition = newPoint;
        }

        private void HandleShipCreation(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _tempShip != null)
            {
                if (!_tempShip.IsValid)
                    return;

                _tempShip?.Freeze();
                _tempShip = null;
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
                    SwitchState(BattleShipsState.Game);
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

        private void CheckWinner(bool isWin, KeyboardHookEventArgs args)
        {

            _shell.SetForegroundColor(isWin ? ShellColor.Blue : ShellColor.Red);
            _shell.PrintTextInCenter(isWin ? Win : Lose, new Point(0, 10));
            _shell.PrintTextInCenter(MakeChoise, new Point(0, 12));

            _shell.ResetColor();
            if (args.KeyCode == Keys.Enter)
            {
                SwitchState(BattleShipsState.Game);
                StartNewGame();
            }
            if (args.KeyCode == Keys.Escape)
                BackToMenu();
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

        private bool FilterKeys(KeyboardHookEventArgs args) => _availableKeys.Contains(args.KeyCode);

        private void RandomPlaceShips(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _player.Board.Ships.Count != 0)
            {
                SwitchState(BattleShipsState.Game);
                return;
            }
            else
                _player.Reset();

            _player.FillShips();
            _player.ShowBoards();

            _shell.PrintTextInCenter(Resources.PlaceShipInformation.Text, new Point(30, 25));
        }

        /// <summary>
        /// Check if the cell does not contain GOT cell or MISS cell
        /// </summary>
        /// <returns></returns>
        private bool IsValidCell()
        {
            var isEmpty = _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == Empty;
            var isShip = _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == Ship;
            var isGot = _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == Got;
            var isMiss = _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == Miss;

            return isEmpty || isShip && !isGot && !isMiss;
        }

        /// <summary>
        /// Check if the cell is empty
        /// </summary>
        /// <returns></returns>
        private bool IsEmptyCell() => _ai.Board.GetCellValue(_currentPosition.X, _currentPosition.Y).Value == Empty;

        private void Shoot()
        {
            var isAlive = true;

            _ai.Board.ProcessShot(_currentPosition);
            foreach (var item in _ai.Board.Ships)
            {
                if (item.Includes(_currentPosition) && !item.IsAlive)
                {
                    isAlive = false;
                    break;
                }
            }

            if (IsValidCell())
            {
                _player.MakeShot(_currentPosition, IsEmptyCell(), isAlive);

                if (IsEmptyCell())
                {
                    _ai.Board.SetCellValue(_currentPosition.X, _currentPosition.Y, Miss);

                    _aiShooter.EaseModShoot(_ai, _player);
                }
                _player.ShowBoards();
            }
        }

        private void RandomShoot()
        {
            _playerShooter.EaseModShoot(_player, _ai);
            _aiShooter.EaseModShoot(_ai, _player);
            _player.ShowBoards();
        }

        private void GameActions(KeyboardHookEventArgs args)
        {
            if (args.KeyCode == Keys.Enter)
            {
                Shoot();
            }
            else if (args.KeyCode == Keys.R)
            {
                RandomShoot();
            }
            else if (args.KeyCode == Keys.D)
            {
                Debag();
            }
            else
            {
                _player.MakeMove(_currentPosition);
                ActiveBoard.SetCursor(_currentPosition);
            }
        }

        private void Debag()
        {
            _shell.Clear();
            _ai.ShowBoards();
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
                            if (!_answer)
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
                            if (_ai.Board.AliveShips == 0 || _player.Board.AliveShips == 0)
                            {
                                _shell.Clear();
                                var winner = _ai.Board.CheckWinner() == Loser;
                                CheckWinner(winner, e);
                            }
                            else
                            {
                                GameActions(e);
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