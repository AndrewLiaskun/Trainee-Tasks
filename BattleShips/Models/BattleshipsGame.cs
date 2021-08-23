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
using static BattleShips.Resources.ResetQuestion;
using static BattleShips.Resources.Serialization;

namespace BattleShips.Models
{
    public class BattleshipsGame : IBattleshipGame
    {
        private readonly object _syncRoot = new object();

        private bool _answer;

        private BattleShipsState _state = BattleShipsState.Menu;

        private IVisualContext _shell;
        private IGameMenu _gameMenu;
        private IPlayer _player;
        private IPlayer _ai;
        private GameActionHandler _actionHandler;
        private IGameHistory _gameHistory;

        private List<Keys> _availableKeys;

        public BattleshipsGame(IVisualContext shell, PlayerBoardConfig config)
        {
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));

            AddAvailableKeys();

            _shell.RegisterKeyFilter(FilterKeys);

            _actionHandler = new GameActionHandler();

            _gameMenu = new GameMenuBar(_shell, this);

            _player = new Player(_shell, config);
            _ai = new AiPlayer(_shell, config);

            _player.CellCollectionChanged += OnCellChanged;
            _ai.CellCollectionChanged += OnCellChanged;

            _gameHistory = new GameHistory();
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

        public IBattleShipBoard ActiveBoard => State == BattleShipsState.Game ? _player.PolygonBoard : _player.Board;

        public IGameHistory GameHistory => _gameHistory;

        protected bool IsCreation => State == BattleShipsState.CreateShip;

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

        private void OnCellChanged(object sender, CellChangedEventArgs e)
        {
            if (sender == null) return;
            var isGot = Got == e.NewValue.Value;

            var record = new HistoryRecord(e.Player, e.NewValue.Point, isGot);

            _gameHistory.AddRecord(record);
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

            _answer = _shell.InteractionService.AskYesNoQuestion(Question);

            // Change State to CreateShips
            SwitchState(BattleShipsState.CreateShip);
            _ai.FillShips();
            _player.ShowBoards();
        }

        private bool FilterKeys(KeyboardPressedEventArgs args) => _availableKeys.Contains(args.KeyCode);

        /// <summary>
        /// Check if the cell does not contain GOT cell or MISS cell
        /// </summary>
        /// <returns></returns>

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
                    _actionHandler.HandleAction(new ActionContext(e.KeyCode, this, _gameMenu) { IsRandomPlacement = _answer });
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