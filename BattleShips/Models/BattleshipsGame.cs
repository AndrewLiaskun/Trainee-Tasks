// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Menu;
using BattleShips.Misc;
using BattleShips.Misc.Args;
using BattleShips.Resources;
using BattleShips.Utils;

using TicTacToe;

using static BattleShips.Misc.GameConstants;
using static BattleShips.Misc.GameConstants.BoardMeasures;
using static BattleShips.Resources.EndGame;
using static BattleShips.Resources.ResetQuestion;
using static BattleShips.Resources.Serialization;

namespace BattleShips.Models
{
    public class BattleshipsGame : IBattleshipGame
    {
        private readonly object _syncRoot = new object();

        private bool _answer;

        private ShipDirection _shipDirection = ShipDirection.Horizontal;
        private IShip _tempShip;

        private BattleShipsState _state = BattleShipsState.Menu;

        private IGameMenu _gameMenu;
        private Point _currentPosition;
        private IVisualContext _shell;

        private IPlayer _player;
        private IPlayer _ai;

        private List<Keys> _availableKeys;

        private ShootAlgorithm _aiShooter;
        private ShootAlgorithm _playerShooter;

        public BattleshipsGame(IVisualContext shell, PlayerBoardConfig config)
        {
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));

            AddAvailableKeys();

            _shell.RegisterKeyFilter(FilterKeys);

            _gameMenu = new GameMenuBar(_shell, this);

            _player = new Player(_shell, config);
            _ai = new AiPlayer(_shell, config);

            _aiShooter = new ShootAlgorithm();
            _playerShooter = new ShootAlgorithm();
        }

        public event EventHandler<BattleShipsStateChangedEventArgs> StateChanged;

        public BattleShipsState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                var old = _state;
                _state = value;

                RaiseStateChanged(old, _state);
            }
        }

        public IPlayer Computer => _ai;

        public IPlayer User => _player;

        protected bool IsCreation => State == BattleShipsState.CreateShip;

        private IBattleShipBoard ActiveBoard => State == BattleShipsState.Game ? _player.PolygonBoard : _player.Board;

        public void SwitchState(BattleShipsState state) => State = state;

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

            _shell.Output.Reset();

            Reset();
        }

        public void Resume()
        {
            SwitchState(BattleShipsState.Game);

            _shell.Output.Reset();
            _player.ShowBoards();
        }

        public string GetAboutText() => AboutAuthor.Text;

        public void LoadGame()
        {
            SwitchState(BattleShipsState.LoadGame);

            _shell.Output.Reset();

            _shell.Output.SetForegroundColor(ShellColor.Yellow);
            _shell.Output.PrintText(LoadPath, Point.Empty, true);

            _shell.Output.PrintText(string.Empty, new Point(0, 3), true);

            var path = _shell.Output.ReadText();

            if (GameSerializer.TryLoad(path, out var game))
            {
                _player.Load(game.Players[0]);
                _ai.Load(game.Players[1]);
                Resume();
            }
            else _shell.Output.PrintText(PathEx, new Point(0, 5), true);
        }

        public void SaveGame()
        {
            SwitchState(BattleShipsState.SaveGame);

            _shell.Output.Reset();
            _shell.Output.SetForegroundColor(ShellColor.Yellow);

            _shell.Output.PrintText(SavePath, Point.Empty, true);
            _shell.Output.PrintText(string.Empty, new Point(0, 3), true);

            var path = _shell.Output.ReadText();

            if (GameSerializer.TrySave(GameMetadata.FromGame(_player, _ai), path))
                _shell.Output.PrintText(SuccessfulSave, new Point(0, 5), true);
            else
                _shell.Output.PrintText(PathEx, new Point(0, 5), true);

            _shell.Output.ResetColor();
        }

        #region Implementation details

        private void AddAvailableKeys()
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

            var comparison = StringComparison.OrdinalIgnoreCase;

            string answer = string.Empty;

            _shell.Output.SetForegroundColor(ShellColor.Red);

            do
            {
                _shell.Output.PrintText(Question, Point.Empty, true);

                _shell.Output.PrintText(string.Empty, new Point(0, 2), true);
                answer = _shell.Output.ReadText().Trim();
                answer = "y";
                _shell.Output.Reset();
            }
            while (!answer.Equals(PositiveAnswer, comparison) && !answer.Equals(NegativeAnswer, comparison));

            _answer = answer.Equals(PositiveAnswer, comparison);

            _shell.Output.ResetColor();

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

        private void SetGamePoint(KeyboardPressedEventArgs arg)
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

        private void HandleShipCreation(KeyboardPressedEventArgs e)
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

        private void GameControl(KeyboardPressedEventArgs e)
        {
            SetGamePoint(e);

            ActiveBoard.SetCursor(_currentPosition);
        }

        private void CheckWinner(bool isWin, KeyboardPressedEventArgs args)
        {
            _shell.Output.SetForegroundColor(isWin ? ShellColor.Blue : ShellColor.Red);
            _shell.Output.PrintText(isWin ? Win : Lose, new Point(0, 10), true);
            _shell.Output.PrintText(MakeChoise, new Point(0, 12), true);

            _shell.Output.ResetColor();

            if (args.KeyCode == Keys.Enter)
            {
                SwitchState(BattleShipsState.Game);
                StartNewGame();
            }

            if (args.KeyCode == Keys.Escape)
                BackToMenu();
        }

        private void ChangeDirection(KeyboardPressedEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                if (_shipDirection == ShipDirection.Horizontal)
                    _shipDirection = ShipDirection.Vertical;
                else
                    _shipDirection = ShipDirection.Horizontal;
            }
        }

        private bool FilterKeys(KeyboardPressedEventArgs args) => _availableKeys.Contains(args.KeyCode);

        private void RandomPlaceShips(KeyboardPressedEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _player.Board.Ships.Count != 0)
            {
                SwitchState(BattleShipsState.Game);
                return;
            }

            _player.Reset();

            _player.FillShips();
            _player.ShowBoards();

            _shell.Output.PrintText(Resources.PlaceShipInformation.Text, new Point(30, 25), true);
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

                    _aiShooter.MakeShoot(_ai, _player);
                }
                _player.ShowBoards();
            }
        }

        private void RandomShoot()
        {
            _playerShooter.MakeShoot(_player, _ai);
            _aiShooter.MakeShoot(_ai, _player);
            _player.ShowBoards();
        }

        private void GameActions(KeyboardPressedEventArgs args)
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
                Debug();
            }
            else
            {
                _player.MakeMove(_currentPosition);
                ActiveBoard.SetCursor(_currentPosition);
            }
        }

        private void Debug()
        {
            _shell.Output.Reset();
            _ai.ShowBoards();
        }

        private void OnShellKeyPressed(object sender, KeyboardPressedEventArgs e)
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

                    if (State == BattleShipsState.Menu)
                    {
                        _gameMenu.HandleKey(e.KeyCode);
                    }
                    else if (State == BattleShipsState.Game || IsCreation)
                    {
                        GameControl(e);

                        if (IsCreation)
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
                                _shell.Output.Reset();
                                var winner = _ai.Board.CheckWinner() == Loser;
                                CheckWinner(winner, e);
                            }
                            else
                            {
                                GameActions(e);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _shell.SetCursorPosition(new Point(0, 30));
                    _shell.Output.PrintText("ERROR:" + ex).EndLine();
                }
            }
        }

        private void RaiseStateChanged(BattleShipsState oldState, BattleShipsState newState)
            => StateChanged?.Invoke(this, new BattleShipsStateChangedEventArgs(oldState, newState));

        #endregion Implementation details
    }
}