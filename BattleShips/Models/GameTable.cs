// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    public class GameTable
    {
        private static readonly string[] _boardTemplate = { "    1  2  3  4  5  6  7  8  9 10",
                                                            "   __ __ __ __ __ __ __ __ __ __",
                                                            " |  |  |  |  |  |  |  |  |  |  |",
                                                            "  |__|__|__|__|__|__|__|__|__|__|" };

        private CoordinatesMap _coordinates;
        private string[] _emptyBoard;

        public GameTable(Point start, IShell shell)
        {
            Start = start;
            Shell = shell;

            _coordinates = new CoordinatesMap(start, GameConstants.BoardMeasures.Offset);
            _emptyBoard = GenerateBoard();
        }

        public Point Start { get; }

        public Point ZeroCell => _coordinates.ZeroPoint;

        protected IShell Shell { get; }

        public void Draw()
        {
            Shell.Fill(Start, _emptyBoard);
        }

        public void DrawBoardCells(IBattleShipBoard board)
        {
            foreach (var item in board.Cells)
            {
                if (item.Value != GameConstants.Empty)
                    WriteCellValue(item.Point, item.Value);
            }
        }

        public void DrawShip(IShip ship)
        {
            if (!ship.IsValid || ship.Health < ship.Deck)
                Shell.SetForegroundColor(ShellColor.Red);
            else
                Shell.SetForegroundColor(ShellColor.Blue);

            var absolutePoint = _coordinates.GetAbsolutePosition(ship.Start);
            for (int i = 0; i < ship.Deck; i++)
            {
                Shell.PrintText("__", new Point(absolutePoint.X, absolutePoint.Y - 1));
                for (int k = 0; k < 2; ++k)
                {
                    if (k == 0)
                        Shell.PrintText("|  |", new Point(absolutePoint.X - 1, absolutePoint.Y));
                    else
                        Shell.PrintText("|__|", new Point(absolutePoint.X - 1, absolutePoint.Y + 1));
                }

                if (ship.Direction == ShipDirection.Horizontal)
                    absolutePoint.X += GameConstants.BoardMeasures.Offset.X;
                else
                    absolutePoint.Y += GameConstants.BoardMeasures.Offset.Y;
            }

            Shell.ResetColor();
        }

        public void WriteCellValue(Point point, char value)
        {
            var realPos = _coordinates.GetAbsolutePosition(point);
            Shell.SetForegroundColor(ShellColor.Red);
            if (value == GameConstants.Got)
            {
                ShipCell(realPos);
            }
            else if (value == GameConstants.Miss)
            {
                Shell.PrintText(",,", realPos);
            }
            else
                Shell.PrintText(" ", realPos);
            Shell.ResetColor();
        }

        public void SetCursorPosition(Point cell)
        {
            var realPos = _coordinates.GetAbsolutePosition(cell);
            Shell.SetCursorPosition(realPos);
        }

        public void DrawCursor(Point point)
        {
            point = _coordinates.GetAbsolutePosition(point);

            Shell.SetBackgroundColor(ShellColor.DarkYellow);

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                    Shell.PrintText("  ", new Point(point.X, point.Y));
                else
                {
                    Shell.PrintText("__", new Point(point.X, point.Y + 1));
                }
            }

            Shell.ResetColor();
        }

        private void ShipCell(Point realPos)
        {
            Shell.PrintText("__", new Point(realPos.X, realPos.Y - 1));
            for (int k = 0; k < 2; ++k)
            {
                if (k == 0)
                    Shell.PrintText("|  |", new Point(realPos.X - 1, realPos.Y));
                else
                    Shell.PrintText("|__|", new Point(realPos.X - 1, realPos.Y + 1));
            }
        }

        private string[] GenerateBoard()
        {
            Shell.SetCursorPosition(Start);
            var lines = new List<string>();

            const int headerCount = 2;
            const int lineTemplateIndex = 2;
            const int endLineCount = 3;

            lines.AddRange(_boardTemplate.Take(headerCount));

            int i = (int)(char)'A';
            int limit = (int)(char)'J';

            for (; i <= limit; i++)
            {
                lines.Add(((char)i) + _boardTemplate[lineTemplateIndex]);
                lines.Add(_boardTemplate[endLineCount]);
            }

            return lines.ToArray();
        }
    }
}