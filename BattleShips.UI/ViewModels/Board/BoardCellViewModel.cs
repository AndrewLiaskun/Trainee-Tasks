// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShipsWPF.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class BoardCellViewModel : BaseViewModel
    {
        public BoardCell Cell;

        public BoardCellViewModel(BoardCell boardCell)
        {
            Cell = boardCell;
        }

        public char Value
        {
            get => Cell.Value;
            set => RaisePropertyChanged(nameof(Cell.Value));
        }
    }
}