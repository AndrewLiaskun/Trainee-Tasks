// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.Structs;

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

        public GameTable(Point start, Size size, IShell shell)
        {
            Start = start;
            Size = size;
            Shell = shell;
            _coordinates = new CoordinatesMap(start, size);
            GenerateBoard();
        }

        public Point Start { get; }

        public Size Size { get; }

        protected IShell Shell { get; }

        public void WriteCellValue(Point point, char value)
        {
            Point realPos = new Point();
            if (Start.X > 0)
            {
                realPos = _coordinates.GetAbsolutePosition(point);
                realPos.X += 48;
            }
            Shell.PrintChar(value, realPos);
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