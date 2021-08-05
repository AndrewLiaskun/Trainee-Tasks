// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Ships
{
    public class Destroyer : AbstractShip
    {
        public Destroyer(Point point) : base(point, 2, "Destroyer")
        {
        }
    }
}