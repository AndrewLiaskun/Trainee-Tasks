// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Basic;
using BattleShips.UI.Models.Visuals;
using BattleShips.UI.ViewModels;

using TicTacToe;

using Point = TicTacToe.Point;

namespace BattleShips.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IBattleshipGame _battleShipsGame;
        private ICommand _startGameCommand;
        private Window _window;

        public MainWindowViewModel(Window window)
        {
            _window = window;
            _battleShipsGame = new BattleshipsGame(new UiVisualContext(), new PlayerBoardConfig(Point.Empty));
            _battleShipsGame.StartNewGame();
            _battleShipsGame.Start();
            var game = new BattleShipGameViewModel(_battleShipsGame);
        }

        public ICommand StartGame
        {
            get => _startGameCommand ?? (_startGameCommand = new RelayCommand(_battleShipsGame.StartNewGame,
                       () => _battleShipsGame.State != BattleShipsState.Game));
        }

        public BattleShipsState CurrentPage { get; set; } = BattleShipsState.Game;
    }
}