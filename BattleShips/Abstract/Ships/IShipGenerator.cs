// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Abstract.Ships
{
    public interface IShipGenerator
    {
        IShip CreateShip(Point point);
    }
}