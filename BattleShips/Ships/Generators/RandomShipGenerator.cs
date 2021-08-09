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
        private const int MaxPosition = GameConstants.BoardMeasures.MaxIndex;

        private IPlayer _player;

        private Random _generator = new Random();
        private List<Point> _availablePoints;

        public RandomShipGenerator(IPlayer player)
        {
            _availablePoints = new List<Point>();
            _player = player;
        }

        public void PlaceShips()
        {
            Reset();

            var point = GeneratePoint();
            var ship = _player.ShipFactory.CreateShip(point);

            while (ship != null)
            {
                ship.ChangeDirection(GenerateDirection());
                ship.ChangeStartPoint(point);

                if (ship.IsValidEndPosition(ship.Start, ship.Direction) && _player.Board.ValidateShip(ship.Start, ship) && ship.Start == point)
                {
                    ship?.Freeze();

                    DeletePoints(ship.Start, ship);

                    _player.Board.AddShip(ship);

                    PrintShip(ship);

                    ship = _player.ShipFactory.CreateShip(point);
                }

                point = GeneratePoint();
            }
        }

        #region Implementation details

        private void PrintShip(IShip ship)
        {
            for (int i = ship.Start.X; i <= ship.End.X; i++)
            {
                for (int j = ship.Start.Y; j <= ship.End.Y; j++)
                {
                    _player.Board.SetCellValue(i, j, GameConstants.Ship);
                }
            }
        }

        private ShipDirection GenerateDirection() => (ShipDirection)_generator.Next(2);

        private void DeletePoints(Point start, IShip ship)
        {
            var isHorizontal = ship.Direction == ShipDirection.Horizontal;
            int startPos = isHorizontal ? start.X : start.Y;

            for (int i = startPos; i < startPos + ship.Deck; i++)
            {
                var currentPos = isHorizontal ? new Point(i, start.Y) : new Point(start.X, i);

                _availablePoints.Remove(currentPos);

                ExcludeAroundArea(i, currentPos);
            }
        }

        private void ExcludeAroundArea(int iteration, Point current)
        {
            for (int j = -1; j <= 1; ++j)
            {
                for (int k = -1; k <= 1; ++k)
                {
                    if (current.X + iteration > MaxPosition || current.X + iteration < 0)
                        continue;
                    if (current.Y + j > MaxPosition || current.Y + j < 0)
                        continue;

                    var indexX = current.X + iteration;
                    var indexY = current.Y + j;

                    var aroundShip = new Point(indexX, indexY);
                    _availablePoints.Remove(aroundShip);
                }
            }
        }

        private void Reset()
        {
            _availablePoints.Clear();
            _availablePoints.AddRange(_player.Board.Cells.Select(x => x.Point));
        }

        private Point GeneratePoint()
        {
            var index = _generator.Next(_availablePoints.Count);
            return _availablePoints[index];
        }

        #endregion Implementation details
    }
}