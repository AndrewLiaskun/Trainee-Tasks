// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    public class UiGameTable : IVisualTable
    {
        public Point Start => new Point();

        public Point ZeroCell => new Point();

        public void Draw() => throw new NotImplementedException();

        public void DrawBoardCells(IBattleShipBoard board) => throw new NotImplementedException();

        public void DrawCursor(Point point) => throw new NotImplementedException();

        public void DrawShip(IShip ship) => throw new NotImplementedException();

        public void SetCursorPosition(Point point)
        {
        }

        public void WriteCellValue(Point point, char value) => throw new NotImplementedException();
    }
}