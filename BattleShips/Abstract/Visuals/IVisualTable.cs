// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Abstract.Visuals
{
    public interface IVisualTable
    {
        Point Start { get; }

        Point ZeroCell { get; }
        Point CurrentPosition { get; }

        void Draw();

        void DrawBoardCells(IBattleShipBoard board);

        void DrawCursor(Point point);

        void DrawShip(IShip ship);

        void SetCursorPosition(Point point);

        void WriteCellValue(Point point, char value);
    }
}