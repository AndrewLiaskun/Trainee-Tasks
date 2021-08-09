// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Models
{
    public interface IGameTable
    {
        Point Start { get; }
        Point ZeroCell { get; }

        void Draw();
        void DrawBoardCells(IBattleShipBoard board);
        void DrawCursor(Point point);
        void DrawShip(IShip ship);
        void SetCursorPosition(Point cell);
        void WriteCellValue(Point point, char value);
    }
}