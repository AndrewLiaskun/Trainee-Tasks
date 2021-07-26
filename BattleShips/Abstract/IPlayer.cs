// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract.Ships;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IPlayer
    {
        IBattleShipBoard Board { get; }

        IBattleShipBoard PolygonBoard { get; }

        IShipFactory ShipFactory { get; }

        void ShowBoards();

        void MakeShot(Point point, bool isEmpty, bool isAlive);

        IShip CreateShip(Point point);

        void MakeMove(Point point);

        void FillShips();
    }
}