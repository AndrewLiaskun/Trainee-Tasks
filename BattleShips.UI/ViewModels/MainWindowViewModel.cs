// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.Models.Visuals;

using TicTacToe;

using Point = TicTacToe.Point;

namespace BattleShips.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IBattleshipGame _battleShipsGame;
        private ICommand _startGameCommand;
        private IVisualContext _context;
        private ICommand _randomShootCommand;
        private ICommand _upStepCommand;
        private ICommand _downStepCommand;
        private ICommand _leftStepCommand;
        private ICommand _rightStepCommand;
        private ICommand _enterStepCommand;

        public MainWindowViewModel()
        {
            _context = UiVisualContext.Instance;

            _battleShipsGame = new BattleshipsGame(_context, new PlayerBoardConfig(Point.Empty));
            _battleShipsGame.Start();
            _battleShipsGame.StartNewGame();
            Game = new BattleShipGameViewModel(_battleShipsGame);
        }

        public ICommand StartGame
        {
            get => _startGameCommand ?? (_startGameCommand = new RelayCommand(_battleShipsGame.StartNewGame,
                       () => _battleShipsGame.State != BattleShipsState.Game));
        }

        public ICommand RandomShootCommand
        {
            get => _randomShootCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.R)));
        }

        public ICommand UpStepCommand
        {
            get => _upStepCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Up)));
        }

        public ICommand DownStepCommand
        {
            get => _downStepCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Down)));
        }

        public ICommand LeftStepCommand
        {
            get => _leftStepCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Left)));
        }

        public ICommand RightStepCommand
        {
            get => _rightStepCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Right)));
        }

        public ICommand EnterCommand
        {
            get => _enterStepCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Enter)));
        }

        public BattleShipGameViewModel Game { get; }

        public BattleShipsState CurrentPage { get; set; } = BattleShipsState.Game;
    }
}