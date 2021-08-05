// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Abstract.Ships
{
    public interface IShipGenerator
    {
        IShip CreateShip(Point point);

        IShip GetNewShip(Point point, ShipType shipType);
    }
}