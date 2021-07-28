// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Metadata;
using BattleShips.Ships;

using TicTacToe;

using static BattleShips.Resources.ShipConcrete;

namespace BattleShips.Misc
{
    public abstract class AbstractShipGenerator : IShipFactory
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

        public IShip CreateShip(ShipDto shipDto)
        {
            var newShip = GetNewShip(shipDto.Start, shipDto.Type);

            if (shipDto.Health != shipDto.Deck)
                newShip.TryDamageShip(shipDto.Start, shipDto.Deck - shipDto.Health);

            newShip.IsValid = shipDto.IsValid;

            newShip.ChangeDirection(shipDto.Direction);

            if (shipDto.IsFrozen)
                newShip.Freeze();

            return newShip;
        }

        public IShip GetNewShip(Point point, ShipType shipType)
        {
            if (_creators.TryGetValue(shipType, out var creator))
                return creator(point);

            return null;
        }

        protected void FillShips()
        {
            _availableShips.Add(ShipType.Battleship, int.Parse(BattleshipCount));
            _availableShips.Add(ShipType.Cruiser, int.Parse(CruiserCount));
            _availableShips.Add(ShipType.Destroyer, int.Parse(DestroyerCount));
            _availableShips.Add(ShipType.TorpedoBoat, int.Parse(TorpedoBoatCount));
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