// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Ships
{
    public class Battleship : AbstractShip
    {

        public Battleship(Point point) : base(point, 4, "Battleship")
        {
        }
    }
}