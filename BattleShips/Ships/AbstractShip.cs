// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Direction Direction { get; protected set; }

        public int Deck { get; protected set; }

        public int Health { get; private set; }

        public bool IsAlive => Health > 0;

        public void ApplyDamage(Point point, bool damaged) => throw new NotImplementedException();

        public void ChangeDirection(Direction direction)
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

        protected void RaiseShipChanged(ShipState previous, ShipState current)
        {
            ShipChanged?.Invoke(this, new ShipChangedEventArgs(previous, current));
        }

        private bool IsValidEndPosition(Point start, Direction direction)
        {
            Point p = GetFutureEnd(start, direction);

            if (p.X <= GameConstants.BoardMeasures.MaxIndex && p.X >= GameConstants.BoardMeasures.MinIndex
                && p.Y >= GameConstants.BoardMeasures.MinIndex && p.Y <= GameConstants.BoardMeasures.MaxIndex)
                return true;
            return false;
        }

        private Point GetFutureEnd(Point start, Direction direction)
        {
            if (direction == Direction.Horizontal)
                return new Point(start.X + GameConstants.BoardMeasures.Step * Deck, start.Y);

            return new Point(start.X, start.Y + GameConstants.BoardMeasures.Step * Deck);
        }

        private ShipState GetCurrentState() => ShipState.FromShip(this);
    }
}