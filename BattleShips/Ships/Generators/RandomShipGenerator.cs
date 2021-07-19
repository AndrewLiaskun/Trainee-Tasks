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

        public RandomShipGenerator(IPlayer player)
        {
            _player = player;
            _point = GetRandomPoint();
            _ship = _player.CreateShip(_point);
        }

        public void RandomShipPlacement()
        {

            while (_ship != null)
            {
                _player.Board.MoveShip(_point, _ship, GetRandomDirection());

                while (!_ship.IsValid || _ship.End == new Point())
                {
                    _point = GetRandomPoint();
                    _player.Board.MoveShip(_point, _ship, GetRandomDirection());
                }

                _ship?.Freeze();
                _ship = null;

                _point = GetRandomPoint();
                _ship = _player.CreateShip(_point);
            }
        }

        private ShipDirection GetRandomDirection()
        {
            return (ShipDirection)_generator.Next(2);
        }

        private Point GetRandomPoint()
        {

            var randomX = _generator.Next(10);
            var randomY = _generator.Next(10);

            var point = new Point(randomX, randomY);

            return point;
        }
    }
}