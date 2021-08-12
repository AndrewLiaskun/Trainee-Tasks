// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;

using BattleShipsWPF.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class PlayerBoardViewModel : BaseViewModel
    {
        public PlayerBoardViewModel(IBattleShipBoard board)
        {
            Items = new ObservableCollection<BoardCellViewModel>(board.Cells.Select(c => new BoardCellViewModel(c)));
        }

        public IEnumerable<BoardCellViewModel> Items { get; }
    }
}