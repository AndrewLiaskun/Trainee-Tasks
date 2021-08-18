// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Utils;

using TicTacToe;

using static BattleShips.Resources.GameDesignation;

namespace BattleShips.Models.Visuals
{
    public class ConsoleGameTable : IVisualTable
    {
        private static readonly string[] _boardTemplate = Table.Split('\n');

        private CoordinatesMap _coordinates;
        private string[] _gameFieldTemplate;

        private Point _currentPosition;

        public ConsoleGameTable(Point start, IVisualContext shell)
        {
            Start = start;
            Shell = shell;

            _coordinates = new CoordinatesMap(start, GameConstants.BoardMeasures.Offset);
            _gameFieldTemplate = GenerateBoard();

            Shell.SetCursorPosition(ZeroCell);
        }

        public Point Start { get; }

        public Point ZeroCell => _coordinates.ZeroPoint;

        public Point CurrentPosition => _currentPosition;

        protected IVisualContext Shell { get; }

        public void Draw()
        {
            Shell.Output.SetForegroundColor(ShellColor.Yellow);

            Shell.Fill(Start, _gameFieldTemplate);

            Shell.Output.ResetColor();
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
            SetShipColor(ship);

            var absolutePoint = _coordinates.GetAbsolutePosition(ship.Start);
            for (int i = 0; i < ship.Deck; i++)
            {
                Shell.Output.PrintText(ShipHeader, new Point(absolutePoint.X, absolutePoint.Y - 1));
                for (int k = 0; k < 2; ++k)
                {
                    if (k == 0)
                        Shell.Output.PrintText(ShipBody, new Point(absolutePoint.X - 1, absolutePoint.Y));
                    else
                        Shell.Output.PrintText(ShipFooter, new Point(absolutePoint.X - 1, absolutePoint.Y + 1));
                }

                if (ship.Direction == ShipDirection.Horizontal)
                    absolutePoint.X += GameConstants.BoardMeasures.Offset.X;
                else
                    absolutePoint.Y += GameConstants.BoardMeasures.Offset.Y;
            }

            Shell.Output.ResetColor();
        }

        public void WriteCellValue(Point point, char value)
        {
            var realPos = _coordinates.GetAbsolutePosition(point);

            Shell.Output.SetForegroundColor(ShellColor.Red);

            if (value == GameConstants.Got)
                DrawShipCell(realPos);

            if (value == GameConstants.Miss)
                Shell.Output.PrintText(Miss, realPos);

            Shell.Output.ResetColor();
        }

        public void SetCursorPosition(Point cell)
        {
            _currentPosition = cell;
            Shell.SetCursorPosition(_coordinates.GetAbsolutePosition(cell));
        }

        public void DrawCursor(Point point)
        {
            point = _coordinates.GetAbsolutePosition(point);

            Shell.Output.SetBackgroundColor(ShellColor.DarkYellow);

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                    Shell.Output.PrintText(CursorBody, new Point(point.X, point.Y));
                else
                {
                    Shell.Output.PrintText(CursorFooter, new Point(point.X, point.Y + 1));
                }
            }

            Shell.Output.ResetColor();
        }

        private void SetShipColor(IShip ship)
        {
            if (!ship.IsValid)
                Shell.Output.SetForegroundColor(ShellColor.Red);
            else
                Shell.Output.SetForegroundColor(ShellColor.Blue);
        }

        private void DrawShipCell(Point realPos)
        {
            Shell.Output.PrintText(ShipHeader, new Point(realPos.X, realPos.Y - 1));

            for (int k = 0; k < 2; ++k)
            {
                if (k == 0)
                    Shell.Output.PrintText(ShipBody, new Point(realPos.X - 1, realPos.Y));
                else
                    Shell.Output.PrintText(ShipFooter, new Point(realPos.X - 1, realPos.Y + 1));
            }
        }

        private string[] GenerateBoard()
        {
            var lines = new List<string>();

            const int headerCount = 2;
            const int lineTemplateIndex = 2;
            const int endLineCount = 3;

            lines.AddRange(_boardTemplate.Take(headerCount));

            int i = (int)StartLetter[0];
            int limit = (int)EndLetter[0];

            for (; i <= limit; i++)
            {
                lines.Add(((char)i) + _boardTemplate[lineTemplateIndex]);
                lines.Add(_boardTemplate[endLineCount]);
            }

            return lines.ToArray();
        }
    }
}