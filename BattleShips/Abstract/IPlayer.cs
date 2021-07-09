// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IPlayer
    {
        IBattleShipBoard Board { get; }

        void ShowBoards();

        void MakeShot(Point point, bool isEmpty);

        void CreateShip(Point point, bool isEmpty);

        void MakeMove(Point point);
    }
}