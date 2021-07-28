// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Resources.ShipConcrete;

namespace BattleShips.Ships
{
    public class Battleship : AbstractShip
    {

        public Battleship(Point point) : base(point, int.Parse(BattleshipDeck), BattleshipName)
        {
        }

        public override ShipType ShipKind => ShipType.Battleship;
    }
}