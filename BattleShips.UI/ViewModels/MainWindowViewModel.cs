// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Basic;
using BattleShips.UI.Models.Visuals;
using BattleShips.UI.ViewModels;

using TicTacToe;

namespace BattleShips.UI
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IBattleshipGame _battleShipsGame;
        private ICommand _startGameCommand;

        public MainWindowViewModel()
        {
            _battleShipsGame = new BattleshipsGame(new UiVisualContext(), new PlayerBoardConfig(Point.Empty));

            _battleShipsGame.Start();
        }

        public ICommand StartGame
        {
            get => _startGameCommand ?? (_startGameCommand = new RelayCommand(_battleShipsGame.StartNewGame,
                       () => _battleShipsGame.State != BattleShipsState.Game));
        }
    }
}