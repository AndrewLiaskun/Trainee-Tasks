// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Ships
{
    public class TorpedoBoat : AbstractShip
    {
        public TorpedoBoat(Point point) : base(point, 1, "Torpedo Boat")
        {
        }
    }
}