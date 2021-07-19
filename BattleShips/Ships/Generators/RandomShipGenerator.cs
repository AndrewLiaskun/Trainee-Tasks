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

namespace BattleShips.Ships.Generators
{
    public class RandomShipGenerator
    {
        private IPlayer _player;
        private IShip _ship;
        private Random _generator = new Random();
        private Point _point;
        private List<Point> _generatedPoints;

        public RandomShipGenerator(IPlayer player)
        {
            _player = player;

            FillGenerator();
            _ship = _player.ShipGenerator.CreateShip(_point);
        }

        public void PlaceShips()
        {

            while (_ship != null)
            {
                _point = GetRandomPoint();
                _ship.ChangeDirection(GetRandomDirection());
                _ship.ChangeStartPoint(_point);

                if (ValidateShip(_ship.Start, _ship))
                {
                    _ship?.Freeze();

                    DeletePoints(_ship.Start, _ship);
                    _player.Board.AddShip(_ship);
                    _ship = null;

                    _ship = _player.ShipGenerator.CreateShip(_point);
                }
            }
            _player.Board.Draw();
        }

        public bool ValidateShip(Point point, IShip ship)
        {
            var rest = _player.Board.Ships.Except(new IShip[] { ship }).ToArray();

            if (rest.Length == 0)
                return true;

            return !rest.Any(x => x.IsValidDistance(point, ship));
        }

        private ShipDirection GetRandomDirection()
        {
            return (ShipDirection)_generator.Next(2);
        }

        private void DeletePoints(Point start, IShip ship)
        {
            var isHorizontal = ship.Direction == ShipDirection.Horizontal;
            int startIndex = isHorizontal ? start.X : start.Y;
            for (int i = startIndex; i < startIndex + ship.Deck; i++)
            {
                var shipPoints = isHorizontal ? new Point(i, start.Y) : new Point(start.X, i);

                _generatedPoints.Remove(shipPoints);
            }
        }

        private void FillGenerator()
        {
            _generatedPoints = _player.Board.Cells.Select(x => x.Point).ToList();
        }

        private Point GetRandomPoint()
        {
            var index = _generator.Next(_generatedPoints.Count);
            return _generatedPoints[index];
        }
    }
}