﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Ships;

using TicTacToe;

namespace BattleShips.Misc
{
    public abstract class AbstractShipGenerator : IShipGenerator
    {
        protected Dictionary<ShipType, int> _availableShips = new Dictionary<ShipType, int>();
        protected Dictionary<ShipType, Func<Point, IShip>> _creators = new Dictionary<ShipType, Func<Point, IShip>>();

        protected ShipType _currentType;

        protected AbstractShipGenerator()
        {
            _currentType = ShipType.Battleship;

            FillShips();
            RegisterCreators();
        }

        public IShip CreateShip(Point point)
        {
            if (_currentType == ShipType.Unknown)
                return null;

            if (_availableShips[_currentType] > 0)
                _availableShips[_currentType]--;

            ShipType nextShipType = _currentType;
            if (_availableShips[_currentType] == 0)
            {
                if (nextShipType != ShipType.TorpedoBoat)
                    nextShipType++;
                else
                    nextShipType = ShipType.Unknown;
            }

            var ship = GetNewShip(point, _currentType);
            _currentType = nextShipType;

            return ship;
        }

        protected void FillShips()
        {
            _availableShips.Add(ShipType.Battleship, 1);
            _availableShips.Add(ShipType.Cruiser, 2);
            _availableShips.Add(ShipType.Destroyer, 3);
            _availableShips.Add(ShipType.TorpedoBoat, 4);
        }

        protected IShip GetNewShip(Point point, ShipType shipType)
        {
            if (_creators.TryGetValue(shipType, out var creator))
                return creator(point);

            return null;
        }

        private void RegisterCreators()
        {
            _creators.Add(ShipType.Battleship, (x) => new Battleship(x));
            _creators.Add(ShipType.Cruiser, (x) => new Cruiser(x));
            _creators.Add(ShipType.Destroyer, (x) => new Destroyer(x));
            _creators.Add(ShipType.TorpedoBoat, (x) => new TorpedoBoat(x));
        }
    }
}