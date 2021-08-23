// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public interface IHistoryRecord
    {
        bool IsShipCell { get; }

        Point Point { get; }

        PlayerType Shooter { get; }
    }
}