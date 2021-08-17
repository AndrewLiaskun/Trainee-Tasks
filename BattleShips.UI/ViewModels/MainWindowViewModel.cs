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

        public MainWindowViewModel()
        {
            _context = UiVisualContext.Instance;

            _battleShipsGame = new BattleshipsGame(_context, new PlayerBoardConfig(Point.Empty));

            _battleShipsGame.Start();
            _battleShipsGame.StartNewGame();

            _context.GenerateKeyPress(Keys.Enter);

            Game = new BattleShipGameViewModel(_battleShipsGame);
        }

        public ICommand StartGame
        {
            get => _startGameCommand ?? (_startGameCommand = new RelayCommand(_battleShipsGame.StartNewGame,
                       () => _battleShipsGame.State != BattleShipsState.Game));
        }

        public BattleShipGameViewModel Game { get; }

        public BattleShipsState CurrentPage { get; set; } = BattleShipsState.Game;
    }
}