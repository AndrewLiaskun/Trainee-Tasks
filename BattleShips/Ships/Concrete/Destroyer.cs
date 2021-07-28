// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Resources.ShipConcrete;

namespace BattleShips.Ships
{
    public class Destroyer : AbstractShip
    {
        public Destroyer(Point point) : base(point, int.Parse(DestroyerDeck), DestroyerName)
        {
        }

        public override ShipType ShipKind => ShipType.Destroyer;
    }
}