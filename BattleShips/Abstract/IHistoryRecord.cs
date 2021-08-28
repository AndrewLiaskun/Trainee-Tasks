// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    public interface IHistoryRecord
    {
        ShipState Ship { get; }

        PlayerType PlayerType { get; }

        Point Point { get; }

        string Shooter { get; }
    }
}