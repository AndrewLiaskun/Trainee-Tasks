// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Enums;
using BattleShips.Metadata;

using TicTacToe;

namespace BattleShips.Abstract.Ships
{
    public interface IShipFactory
    {
        IShip CreateShip(Point point);

        IShip GetNewShip(Point point, ShipType shipType);

        IShip CreateShip(ShipDto shipDto);
    }
}