// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class PlayerBoardViewModel : BaseViewModel, IModelProvider<IBattleShipBoard>
    {

        private ObservableCollection<ShipViewModel> _ships;

        public PlayerBoardViewModel(IBattleShipBoard board)
        {
            Model = board ?? throw new ArgumentNullException(nameof(board));

            Model.ShipChanged += OnShipChanged;
            Model.ShipsCollectionChanged += OnShipsChanged;

            Cells = new ObservableCollection<BoardCellViewModel>(GetCells(Model));
            _ships = new ObservableCollection<ShipViewModel>(GetShips(Model));

            foreach (var item in Cells)
                item.Clicked += OnCellClicked;

            foreach (var item in Ships)
                item.Model.ShipChanged += OnShipChanged;
        }

        public event EventHandler<Point> PlayerShot;

        public IEnumerable<BoardCellViewModel> Cells { get; private set; }

        public IEnumerable<ShipViewModel> Ships => _ships;

        public IBattleShipBoard Model { get; }

        private static IEnumerable<BoardCellViewModel> GetCells(IBattleShipBoard board)
            => board.Cells.Select(c => new BoardCellViewModel(c));

        private static IEnumerable<ShipViewModel> GetShips(IBattleShipBoard model)
            => model.Ships.Select(s => new ShipViewModel(s));

        private void OnCellClicked(object sender, Point e)
        {
            PlayerShot?.Invoke(this, e);

            foreach (var item in Cells)
                item.RefreshAllBindings();

            RaisePropertyChanged(nameof(Cells));
        }

        private void OnShipsChanged(object sender, BoardShipsChangedEventArgs e)
        {
            ChangeShipProperties(e);
            foreach (var item in Cells)
            {
                item.RefreshAllBindings();
            }
        }

        private void ChangeShipProperties(BoardShipsChangedEventArgs e)
        {
            ShipViewModel currenItem = null;
            switch (e.ChangeType)
            {
                case BoardShipsChangeType.Add:
                    currenItem = new ShipViewModel(e.NewShip);
                    currenItem.UpdateCells(this);
                    _ships.Add(currenItem);
                    break;

                case BoardShipsChangeType.Remove:
                    currenItem = _ships.First(x => x.Model == e.OldShip);
                    _ships.Remove(currenItem);
                    currenItem.UpdateCells(this);
                    break;

                case BoardShipsChangeType.Replace:
                    currenItem = _ships.First(x => x.Model == e.OldShip);
                    _ships.Remove(currenItem);
                    currenItem.UpdateCells(this);

                    currenItem = new ShipViewModel(e.NewShip);
                    currenItem.UpdateCells(this);

                    _ships.Add(currenItem);
                    break;

                case BoardShipsChangeType.Reset:
                    _ships.Clear();
                    foreach (var item in GetShips(Model))
                    {
                        item.UpdateCells(this);
                        _ships.Add(item);
                    }
                    RaisePropertyChanged(nameof(Ships));
                    break;

                default:
                    break;
            }
        }

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
        {

            var ship = Model.GetShipAtOrDefault(e.NewValue.Start);
            var shipVM = Ships.First(x => x.Model == ship);
            var oldCells = shipVM.Cells.ToArray();

            shipVM.UpdateCells(this);

            foreach (var item in oldCells)
                item.RefreshAllBindings();
        }
    }
}