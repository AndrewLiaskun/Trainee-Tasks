// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Ships.Generators
{
    public class CreateOpponentShip
    {
        private IBattleShipBoard _board;
        private IShipGenerator _shipGenerator;
        private int countShipTypes = 4;

        public CreateOpponentShip(IBattleShipBoard board, IShipGenerator generator)
        {
            _board = board;
            _shipGenerator = generator;
        }

        public IShip Create(Point point)
        {
            foreach (var item in _board.Ships)
            {
                if (item.CriticalDistance(point))
                {
                    return GetShip(item, point);
                }
            }
            var ship = new TorpedoBoat(point);
            ChangeStateShip(ship, ShipDirection.Horizontal, point);
            return ship;
        }

        private static void ChangeStateShip(IShip ship, ShipDirection direction, Point startPoint)
        {
            ship.ChangeStartPoint(startPoint);
            ship.ChangeDirection(direction);
        }

        private IShip GetShip(IShip item, Point point)
        {

            var isHorizontal = item.Start.Y == point.Y ? true : false;
            var direction = isHorizontal ? ShipDirection.Horizontal : ShipDirection.Vertical;

            var isEnd = point.X > item.Start.X || point.Y > item.Start.Y;

            var startPoint = isEnd ? item.Start : point;
            var endPoint = isEnd ? point : item.Start;

            var start = isHorizontal ? startPoint.X : startPoint.Y;
            var end = isHorizontal ? endPoint.X : endPoint.Y;

            var shipDeck = Math.Abs(end - start - countShipTypes);
            var shipType = (ShipType)shipDeck;

            var ship = _shipGenerator.GetNewShip(startPoint, shipType);
            ChangeStateShip(ship, direction, startPoint);

            return ship;
        }
    }
}