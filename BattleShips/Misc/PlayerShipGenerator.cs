// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Ships;

using TicTacToe;

namespace BattleShips.Misc
{
    public class PlayerShipGenerator : AbstractShipGenerator
    {

        public PlayerShipGenerator() : base()
        {
        }

        public IShip GenerateShip(Point point)
        {

            var currentShip = _availableShips.FirstOrDefault(x => x.Value > 0).Key;
            IShip newShip = null;
            if (currentShip == ShipType.Battleship)
            {
                newShip = new Battleship(point);
                _availableShips[currentShip]--;
            }

            if (currentShip == ShipType.Cruiser)
            {
                newShip = new Cruiser(point);
                _availableShips[currentShip]--;
            }

            if (currentShip == ShipType.Destroyer)
            {
                newShip = new Destroyer(point);
                _availableShips[currentShip]--;
            }

            if (currentShip == ShipType.TorpedoBoar)
            {
                newShip = new TorpedoBoat(point);
                _availableShips[currentShip]--;
            }
            return newShip;
        }
    }
}