// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;

namespace BattleShips.Misc
{
    public class AbstractShipGenerator
    {
        protected Dictionary<ShipType, int> _availableShips = new Dictionary<ShipType, int>();

        protected AbstractShipGenerator()
        {
            AddShips();
        }

        protected void AddShips()
        {
            _availableShips.Add(ShipType.Battleship, 1);
            _availableShips.Add(ShipType.Cruiser, 2);
            _availableShips.Add(ShipType.Destroyer, 3);
            _availableShips.Add(ShipType.TorpedoBoar, 4);
        }
    }
}