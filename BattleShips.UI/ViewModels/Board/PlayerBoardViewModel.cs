// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

namespace BattleShips.UI.ViewModels.Board
{
    public class PlayerBoardViewModel : BaseViewModel, IModelProvider<IBattleShipBoard>
    {
        public PlayerBoardViewModel(IBattleShipBoard board)
        {
            Model = board ?? throw new ArgumentNullException(nameof(board));

            Model.ShipChanged += OnShipChanged;
            Model.ShipsCollectionChanged += OnShipsChanged;

            Cells = new ObservableCollection<BoardCellViewModel>(GetCells(Model));
        }

        public IEnumerable<BoardCellViewModel> Cells { get; }

        public IBattleShipBoard Model { get; }

        private static IEnumerable<BoardCellViewModel> GetCells(IBattleShipBoard board)
            => board.Cells.Select(c => new BoardCellViewModel(c));

        private void OnShipsChanged(object sender, BoardShipsChangedEventArgs e)
            => RaisePropertyChanged(nameof(Cells));

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
            => RaisePropertyChanged(nameof(Cells));
    }
}