// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IPlayer
    {
        IBattleShipBoard Board { get; }

        IBattleShipBoard PolygonBoard { get; }

        void ShowBoards();

        void MakeShot(Point point, bool isEmpty);

        IShip CreateShip(Point point);

        void MakeMove(Point point);
    }
}