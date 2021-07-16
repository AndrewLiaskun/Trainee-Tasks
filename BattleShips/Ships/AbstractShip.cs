﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Ships
{
    public class AbstractShip : IShip
    {
        protected AbstractShip(Point start, int deck, string name)
        {
            Start = start;
            Deck = deck;
            Name = name;
            Health = Deck;
        }

        public event EventHandler<ShipChangedEventArgs> ShipChanged;

        public bool IsFrozen { get; private set; }

        public Point Start { get; protected set; }

        public Point End { get; protected set; }

        public string Name { get; }

        public ShipDirection Direction { get; protected set; }

        public int Deck { get; protected set; }

        public int Health { get; private set; }

        public bool IsAlive => Health > 0;

        public bool IsValid { get; set; }

        public void ApplyDamage(Point point, bool damaged) => throw new NotImplementedException();

        public void ChangeDirection(ShipDirection direction)
        {
            if (IsFrozen)
                throw new InvalidOperationException();

            if (direction == Direction) return;

            if (IsValidEndPosition(Start, direction))
            {
                var current = GetCurrentState();

                Direction = direction;
                End = GetFutureEnd(Start, direction);

                RaiseShipChanged(current, GetCurrentState());
            }
        }

        public void ChangeStartPoint(Point point)
        {
            if (IsFrozen)
                throw new InvalidOperationException();

            if (IsValidEndPosition(point, Direction))
            {
                var current = GetCurrentState();

                Start = point;
                End = GetFutureEnd(Start, Direction);

                RaiseShipChanged(current, GetCurrentState());
            }
        }

        public bool Equals(IShip other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Name.Equals(other.Name)
                && Start.Equals(other.Start)
                && End.Equals(other.End);
        }

        public override bool Equals(object obj) => Equals(obj as IShip);

        public override int GetHashCode() => Name.GetHashCode() ^ Start.GetHashCode() ^ End.GetHashCode();

        public void Freeze() => IsFrozen = true;

        public bool IsInsideShip(Point point) => throw new NotImplementedException();

        public bool TryDamageShip(Point shot) => throw new NotImplementedException();

        public bool Includes(Point point)
        {
            var includeX = Start.X <= point.X && End.X >= point.X;
            var includeY = Start.Y <= point.Y && End.Y >= point.Y;

            return includeX && includeY;
        }

        public bool LowDistance(Point point)
        {

            for (int i = -1; i <= 1; ++i)
            {
                if (point.X + i < 0 || point.X + i >= GameConstants.BoardMeasures.MaxIndex)
                    continue;

                for (int j = -1; j <= 1; ++j)
                {
                    if (point.Y + j < 0 || point.Y + j >= GameConstants.BoardMeasures.MaxIndex)
                        continue;

                    if (Includes(new Point(point.X + i, point.Y + j)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IntersectsWith(Point start, IShip ship)
        {
            var isHorizontal = ship.Direction == ShipDirection.Horizontal;
            int startIndex = isHorizontal ? start.X : start.Y;

            for (int i = startIndex; i < startIndex + ship.Deck; i++)
            {
                var p = isHorizontal ? new Point(i, start.Y) : new Point(start.X, i);
                if (Includes(p) || LowDistance(p))
                    return true;
            }
            return false;
        }

        protected void RaiseShipChanged(ShipState previous, ShipState current)
        {
            ShipChanged?.Invoke(this, new ShipChangedEventArgs(previous, current));
        }

        private bool IsValidEndPosition(Point start, ShipDirection direction)
        {
            Point p = GetFutureEnd(start, direction);

            if (p.X <= GameConstants.BoardMeasures.MaxIndex && p.X >= GameConstants.BoardMeasures.MinIndex
                && p.Y >= GameConstants.BoardMeasures.MinIndex && p.Y <= GameConstants.BoardMeasures.MaxIndex)
                return true;
            return false;
        }

        private Point GetFutureEnd(Point start, ShipDirection direction)
        {
            if (direction == ShipDirection.Horizontal)
                return new Point(start.X + GameConstants.BoardMeasures.Step * Deck - 1, start.Y);

            return new Point(start.X, start.Y + GameConstants.BoardMeasures.Step * Deck - 1);
        }

        private ShipState GetCurrentState() => ShipState.FromShip(this);
    }
}