// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IShip : IEquatable<IShip>
    {
        event EventHandler<ShipChangedEventArgs> ShipChanged;

        int Health { get; }

        bool IsFrozen { get; }

        Point Start { get; }

        Point End { get; }

        string Name { get; }

        ShipDirection Direction { get; }

        /// <summary>
        /// Gets the count of decks in a ship
        /// </summary>
        int Deck { get; }

        bool IsAlive { get; }

        bool IsValid { get; set; }

        /// <summary>
        /// For the impossibility of change
        /// </summary>
        void Freeze();

        void ChangeStartPoint(Point point);

        void ChangeDirection(ShipDirection direction);

        bool TryDamageShip(Point shot);

        void ApplyDamage(bool damaged);

        bool Includes(Point point);

        bool IsValidDistance(Point start, IShip ship);

        bool IsAtCriticalDistance(Point point);

        bool IsValidEndPosition(Point start, ShipDirection direction);
    }
}