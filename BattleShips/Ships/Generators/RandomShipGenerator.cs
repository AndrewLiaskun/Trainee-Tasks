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

        public RandomShipGenerator(IPlayer player) => _player = player;

        public void RandomShipPlacement()
        {
            List<IShip> generatedShip = new List<IShip>();
            bool empty = false;
            IShip ship = null;
            while (!empty)
            {
                if (ship != null)
                {
                    ship = _player.CreateShip(GetRandomEmptyPoint());
                    generatedShip.Add(ship);
                    for (int i = ship.Start.X; i <= ship.End.X; i++)
                    {
                        for (int j = ship.Start.Y; j <= ship.End.Y; j++)
                        {
                            _player.Board.SetCellValue(i, j, GameConstants.Ship);
                        }
                    }
                }
                else
                {
                    empty = true;
                }
            }
        }

        private Point GetRandomEmptyPoint()
        {
            var digitGenerator = new Random();
            var randomX = digitGenerator.Next(0, 9);
            var randomY = digitGenerator.Next(0, 9);

            if (_player.Board.GetCellValue(randomX, randomY).Value == GameConstants.Empty)
                return new Point(randomX, randomY);

            return GetRandomEmptyPoint();
        }
    }
}