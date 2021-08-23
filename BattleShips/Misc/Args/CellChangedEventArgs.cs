// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public class CellChangedEventArgs : EventArgs
    {
        public CellChangedEventArgs(BoardCell oldValue, BoardCell newValue, PlayerType player)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Player = player;
        }

        public BoardCell OldValue { get; }

        public BoardCell NewValue { get; }

        public PlayerType Player { get; }
    }
}