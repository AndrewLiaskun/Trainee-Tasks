// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IShip : IEquatable<IShip>
    {
        Point Start { get; }

        Point End { get; }

        string Name { get; }

        /// <summary>
        /// Gets the count of decks in a ship
        /// </summary>
        int Deck { get; }

        bool IsAlive { get; }

        bool IsInsideShip(Point point);

        bool TryDamageShip(Point shot);

        void ApplyDamage(Point point, bool damaged);
    }
}