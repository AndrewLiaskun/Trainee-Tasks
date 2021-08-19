// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    public class UiGameTable : IVisualTable
    {
        private Point _currentPosition;

        public UiGameTable(Point startPoint, IVisualContext shell)
        {
            Start = startPoint;
            Shell = shell;
        }

        public Point Start { get; }

        public Point ZeroCell { get; } = Point.Empty;

        public Point CurrentPosition => _currentPosition;

        protected IVisualContext Shell { get; }

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
            _currentPosition = point;
            //Shell.SetCursorPosition(_coordinates.GetAbsolutePosition(cell));
        }

        public void WriteCellValue(Point point, char value)
        {
        }
    }
}