// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Models
{
    public class CellChangedEventArgs : EventArgs
    {
        public CellChangedEventArgs(BoardCell oldValue, BoardCell newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public BoardCell OldValue { get; }

        public BoardCell NewValue { get; }
    }
}