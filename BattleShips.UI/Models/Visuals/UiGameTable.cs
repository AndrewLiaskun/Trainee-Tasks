// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    public class UiGameTable : IVisualTable
    {
        public UiGameTable(Point startPoint)
        {
            Start = startPoint;
        }

        public Point Start { get; }

        public Point ZeroCell { get; } = Point.Empty;

        public Point CurrentPosition => throw new NotImplementedException();

        public void Draw()
        {
        }

        public void DrawBoardCells(IBattleShipBoard board)
        {
        }

        public void DrawCursor(Point point)
        {
        }

        public void DrawShip(IShip ship)
        {
        }

        public void SetCursorPosition(Point point)
        {
        }

        public void WriteCellValue(Point point, char value)
        {
        }
    }
}