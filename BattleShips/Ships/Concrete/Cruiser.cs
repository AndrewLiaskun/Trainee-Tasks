// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Resources.ShipConcrete;

namespace BattleShips.Ships
{
    public class Cruiser : AbstractShip
    {
        public Cruiser(Point point) : base(point, int.Parse(CruiserDeck), CruiserName)
        {
        }

        public override ShipType ShipKind => ShipType.Cruiser;
    }
}