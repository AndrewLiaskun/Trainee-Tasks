// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Ships.Generators
{
    public class OpponentShipGenerator
    {
        private IBattleShipBoard _board;
        private IShipFactory _shipFactory;

        public OpponentShipGenerator(IBattleShipBoard board, IShipFactory factory)
        {
            _board = board;
            _shipFactory = factory;
        }

        public IShip Create(Point point, bool isAlive)
        {
            IShip ship;
            var oldShips = _board.Ships.Where(x => x.IsAtCriticalDistance(point)).ToArray();
            if (oldShips.Any())
            {
                ship = GetShip(oldShips, point, isAlive);
                return ship;
            }

            ship = _shipFactory.GetNewShip(point, ShipType.TorpedoBoat);
            ChangeShipState(ship, ShipDirection.Horizontal, point);

            if (!isAlive)
                ship.Kill();

            return ship;
        }

        private static void ChangeShipState(IShip ship, ShipDirection direction, Point startPoint)
        {
            ship.ChangeStartPoint(startPoint);
            ship.ChangeDirection(direction);
        }

        private static T PeekOne<T>(bool flag, T one, T another) => flag ? one : another;

        private IShip GetShip(IReadOnlyList<IShip> ships, Point point, bool isAlive)
        {
            var isHorizontal = ships[0].Start.Y == point.Y;
            var direction = isHorizontal ? ShipDirection.Horizontal : ShipDirection.Vertical;

            var min = isHorizontal ?
                Math.Min(ships.Min(x => x.Start.X), point.X) :
                Math.Min(ships.Min(x => x.Start.Y), point.Y);

            var startX = PeekOne(isHorizontal, min, point.X);
            var startY = PeekOne(isHorizontal, point.Y, min);

            var startPoint = new Point(startX, startY);

            var shipType = (ShipType)(Enum.GetValues(typeof(ShipType)).Length - ships.Aggregate(0, (res, x) => res += x.Deck) - 1);

            var ship = _shipFactory.GetNewShip(startPoint, shipType);
            ChangeShipState(ship, direction, startPoint);

            if (!isAlive)
                ship.Kill();

            return ship;
        }
    }
}