// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

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
        private int _size = GameConstants.BoardMeasures.MaxIndex;

        public RandomShipGenerator(IPlayer player)
        {
            _player = player;

            FillGenerator();

            _point = GetRandomPoint();
            _ship = _player.ShipGenerator.CreateShip(_point);
        }

        public void PlaceShips()
        {
            while (_ship != null)
            {
                _ship.ChangeDirection(GetRandomDirection());
                _ship.ChangeStartPoint(_point);
                if (_ship.IsValidEndPosition(_ship.Start, _ship.Direction) && _player.Board.ValidateShip(_ship.Start, _ship) && _ship.Start == _point)
                {
                    _ship?.Freeze();

                    DeletePoints(_ship.Start, _ship);
                    _player.Board.AddShip(_ship);

                    _ship = null;

                    _ship = _player.ShipGenerator.CreateShip(_point);
                }
                _point = GetRandomPoint();
            }
            _player.Board.Draw();
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

                _generatedPoints?.Remove(shipPoints);

                for (int j = -1; j <= 1; ++j)
                {

                    for (int k = -1; k <= 1; ++k)
                    {
                        var indexX = shipPoints.X + i > _size || shipPoints.X + i < 0 ? shipPoints.X : shipPoints.X + i;
                        var indexY = shipPoints.Y + j > _size || shipPoints.Y + j < 0 ? shipPoints.Y : shipPoints.Y + j;
                        var arroundShip = new Point(indexX, indexY);
                        _generatedPoints?.Remove(arroundShip);
                    }
                }
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