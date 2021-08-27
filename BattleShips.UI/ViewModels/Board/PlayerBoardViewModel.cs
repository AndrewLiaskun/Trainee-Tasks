﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

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

            Model.CellChanged += OnCellChanged;
        }

        public event EventHandler<Point> PlayerShot;

        public IEnumerable<BoardCellViewModel> Cells { get; private set; }

        public IEnumerable<ShipViewModel> Ships => _ships;

        public IBattleShipBoard Model { get; }

        private static IEnumerable<BoardCellViewModel> GetCells(IBattleShipBoard board)
            => board.Cells.Select(c => new BoardCellViewModel(c));

        private static IEnumerable<ShipViewModel> GetShips(IBattleShipBoard model)
            => model.Ships.Select(s => new ShipViewModel(s));

        private void OnCellChanged(object sender, BoardCell e)
        {
            var cellVm = Cells.First(x => x.Model.Point == e.Point);
            cellVm.RefreshAllBindings();
        }

        private void OnCellClicked(object sender, Point e)
        {
            PlayerShot?.Invoke(this, e);
        }

        private void OnShipsChanged(object sender, BoardShipsChangedEventArgs e)
        {
            HandleShipsUpdate(e);

            RefreshAllCells();
        }

        private void HandleShipsUpdate(BoardShipsChangedEventArgs e)
        {
            List<ShipViewModel> currentItem = new List<ShipViewModel>();

            switch (e.ChangeType)
            {
                case BoardShipsChangeType.Add:
                    currentItem.Add(new ShipViewModel(e.NewShip));
                    currentItem.FirstOrDefault().UpdateCells(this);
                    _ships.Add(currentItem.FirstOrDefault());
                    break;

                case BoardShipsChangeType.Remove:
                    foreach (var item in e.OldShip)
                        currentItem.Add(_ships.First(x => x.Model == item));

                    currentItem.ForEach(x => _ships.Remove(x));
                    currentItem.ForEach(x => x.UpdateCells(this));
                    break;

                case BoardShipsChangeType.Replace:
                    foreach (var item in e.OldShip)
                        currentItem.Add(_ships.First(x => x.Model == item));

                    currentItem.ForEach(x => _ships.Remove(x));
                    currentItem.ForEach(x => x.UpdateCells(this));

                    currentItem.Clear();

                    currentItem.Add(new ShipViewModel(e.NewShip));
                    currentItem.FirstOrDefault().UpdateCells(this);

                    _ships.Add(currentItem.FirstOrDefault());

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
            }
        }

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
        {
            var ship = Model.GetShipAtOrDefault(e.NewValue.Start);
            var shipVm = Ships.First(x => x.Model == ship);

            var oldCells = shipVm.Cells.ToArray();

            shipVm.UpdateCells(this);

            foreach (var item in oldCells)
                item.RefreshAllBindings();
        }

        private void RefreshAllCells()
        {
            // NOTE: hugest NAIL ever
            foreach (var item in Cells)
                item.RefreshAllBindings();
        }
    }
}