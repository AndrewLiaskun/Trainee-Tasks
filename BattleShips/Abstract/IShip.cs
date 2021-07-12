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

        Direction Direction { get; }

        /// <summary>
        /// Gets the count of decks in a ship
        /// </summary>
        int Deck { get; }

        bool IsAlive { get; }

        void Freeze();

        // For the impossibility of change

        void ChangeStartPoint(Point point);

        void ChangeDirection(Direction direction);

        bool IsInsideShip(Point point);

        bool TryDamageShip(Point shot);

        void ApplyDamage(Point point, bool damaged);
    }
}