// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Abstract
{
    public interface IBattleShipBoard : IBoard
    {
        Point Position { get; }

        IReadOnlyList<IShip> Ships { get; }

        int ShipsCount { get; }

        void Draw();

        void ProcessShot(Point point);
    }
}