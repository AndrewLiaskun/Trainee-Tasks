// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Ships
{
    public class Cruiser : AbstractShip
    {
        public Cruiser(Point point) : base(point, 3, "Cruiser")
        {
        }
    }
}