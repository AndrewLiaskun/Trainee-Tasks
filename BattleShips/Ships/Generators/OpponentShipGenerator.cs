// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Ships.Generators
{
    public class OpponentShipGenerator
    {
        private const int CountShipTypes = 4;

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

            foreach (var item in _board.Ships)
            {
                if (item.IsAtCriticalDistance(point))
                {
                    ship = GetShip(item, point, isAlive);
                    return ship;
                }
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

        private IShip GetShip(IShip item, Point point, bool isAlive)
        {
            var isHorizontal = item.Start.Y == point.Y;
            var direction = isHorizontal ? ShipDirection.Horizontal : ShipDirection.Vertical;

            var isEnd = point.X > item.Start.X || point.Y > item.Start.Y;

            var startPoint = PeekOne(isEnd, item.Start, point);
            var endPoint = PeekOne(isEnd, point, item.Start);

            var start = PeekOne(isHorizontal, startPoint.X, startPoint.Y);
            var end = PeekOne(isHorizontal, endPoint.X, endPoint.Y);

            var shipDeck = Math.Abs(end - start - CountShipTypes);
            var shipType = (ShipType)shipDeck;

            var ship = _shipFactory.GetNewShip(startPoint, shipType);
            ChangeShipState(ship, direction, startPoint);

            if (!isAlive)
                ship.Kill();

            return ship;
        }
    }
}