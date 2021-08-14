// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Basic;
using BattleShips.UI.Models.Visuals;

using Point = TicTacToe.Point;

namespace BattleShips.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IBattleshipGame _battleShipsGame;
        private ICommand _startGameCommand;

        public MainWindowViewModel()
        {
            _battleShipsGame = new BattleshipsGame(new UiVisualContext(), new PlayerBoardConfig(Point.Empty));
            _battleShipsGame.StartNewGame();
            _battleShipsGame.Start();
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