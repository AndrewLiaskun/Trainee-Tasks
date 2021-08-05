// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Resources.ShipConcrete;

namespace BattleShips.Ships
{
    public class TorpedoBoat : AbstractShip
    {
        public TorpedoBoat(Point point) : base(point, int.Parse(TorpedoBoatDeck), TorpedoBoatName)
        {
        }

        public override ShipType ShipKind => ShipType.TorpedoBoat;
    }
}