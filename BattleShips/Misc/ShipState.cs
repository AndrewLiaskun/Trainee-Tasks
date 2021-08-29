// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Misc
{
    public class ShipState
    {
        public ShipState(int shipId, ShipDirection direction, Point start, Point end, ShipType shipKind)
        {
            ShipId = shipId;
            Direction = direction;
            Start = start;
            End = end;
            ShipKind = shipKind;
        }

        public int ShipId { get; }

        public int Decks => Math.Max(Math.Abs(End.X - Start.X), Math.Abs(End.Y - Start.Y));

        public int? Health { get; set; }

        public bool IsFrozen { get; set; }

        public bool? IsAlive { get; set; }

        public ShipType ShipKind { get; }

        public ShipDirection Direction { get; }

        public Point Start { get; }

        public Point End { get; }

        public static ShipState FromShip(IShip ship)
        {
            return new ShipState(ship.ShipId, ship.Direction, ship.Start, ship.End, ship.ShipKind) { Health = ship.Health, IsAlive = ship.IsAlive, IsFrozen = ship.IsFrozen };
        }
    }
}