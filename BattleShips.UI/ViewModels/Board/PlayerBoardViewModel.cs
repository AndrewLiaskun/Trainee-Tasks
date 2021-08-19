// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

using TicTacToe;

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
            Ships = new ObservableCollection<ShipViewModel>(GetShips(Model));
            foreach (var item in Cells)
            {
                item.Clicked += OnCellClicked;
            }
            foreach (var item in Ships)
            {
                item.PropertyChanged += OnShipPropertysChanged;
            }
        }

        public event EventHandler<Point> PlayerShot;

        public event EventHandler<Point> MoveShip;

        public IEnumerable<BoardCellViewModel> Cells { get; private set; }

        public IEnumerable<ShipViewModel> Ships { get; private set; }

        public IBattleShipBoard Model { get; }

        private static IEnumerable<BoardCellViewModel> GetCells(IBattleShipBoard board)
            => board.Cells.Select(c => new BoardCellViewModel(c));

        private static IEnumerable<ShipViewModel> GetShips(IBattleShipBoard model)
            => model.Ships.Select(s => new ShipViewModel(s));

        private void OnShipPropertysChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (var item in Ships)
            {
                item.RefreshAllBindings();
            }
        }

        private void OnCellClicked(object sender, Point e)
        {
            PlayerShot?.Invoke(this, e);
            foreach (var item in Cells)
                item.RefreshAllBindings();
            RaisePropertyChanged(nameof(Cells));
        }

        private void OnShipsChanged(object sender, BoardShipsChangedEventArgs e)
        {
            foreach (var item in Cells)
                item.RefreshAllBindings();

            //TODO: update ships property

            RaisePropertyChanged(nameof(Cells));
        }

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
        {
            foreach (var item in Cells)
                item.RefreshAllBindings();

            //TODO: update ships property
            RaisePropertyChanged(nameof(Cells));
        }
    }
}