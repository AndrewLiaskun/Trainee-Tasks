// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public interface IHistoryRecord
    {
        IShip Ship { get; }

        PlayerType PlayerType { get; }

        string Shooter { get; }
    }
}