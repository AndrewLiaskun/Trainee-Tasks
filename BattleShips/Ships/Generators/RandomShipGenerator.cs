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

        public RandomShipGenerator(IPlayer player) => _player = player;

        public void RandomShipPlacement()
        {
            var point = GetRandomPoint();
            _ship = _player.CreateShip(point);
            _player.Board.MoveShip(point, _ship, GetRandomDirection());

            while (_ship != null)
            {

                if (_ship.IsValid)
                {
                    _ship?.Freeze();
                    _ship = null;
                }
                else
                {
                    point = GetRandomPoint();
                    _player.Board.MoveShip(point, _ship, GetRandomDirection());
                }

                if (_ship == null)
                {
                    point = GetRandomPoint();
                    _ship = _player.CreateShip(point);
                    _player.Board.MoveShip(point, _ship, GetRandomDirection());
                    if (_ship == null)
                        continue;
                }
            }
        }

        private ShipDirection GetRandomDirection()
        {
            var directionGenerator = new Random();
            var direction = (ShipDirection)directionGenerator.Next(2);
            return direction;
        }

        private Point GetRandomPoint()
        {
            var digitGenerator = new Random();

            var randomX = digitGenerator.Next(0, 9);
            var randomY = digitGenerator.Next(0, 9);

            return new Point(randomX, randomY);
        }
    }
}