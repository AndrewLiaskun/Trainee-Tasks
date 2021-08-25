// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.ViewModels.Board;

namespace BattleShips.UI.ViewModels
{
    public class ShipViewModel : BaseViewModel, IModelProvider<IShip>
    {

        private List<BoardCellViewModel> _cells;

        public ShipViewModel(IShip ship)
        {
            Model = ship;
            _cells = new List<BoardCellViewModel>();
        }

        public IShip Model { get; }

        public IEnumerable<BoardCellViewModel> Cells => _cells;

        public void UpdateCells(PlayerBoardViewModel board)
        {
            _cells.ForEach(x => x.RefreshAllBindings());

            _cells.Clear();

            _cells.AddRange(board.Cells.Where(x => Model.Includes(x.Model.Point)));

            SetImage();
        }

        private void SetImage()
        {
            var count = 0;
            var ship = $"{Model.Name}_{Model.Direction}";

            _cells.ForEach(x => x.Image = BoardCellViewModel.ShipImages[$"{ship}_{count++}"] as ImageBrush);
            _cells.ForEach(x => x.RefreshAllBindings());
        }
    }
}