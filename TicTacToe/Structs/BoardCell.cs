// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class BoardCell
    {
        public const char DefaultCharValue = ' ';
        public const char ZeroChar = 'O';
        public const char CrossChar = 'X';

        public BoardCell(Point point, char value = DefaultCharValue)
        {
            Point = point;
            Value = value;
        }

        public Point Point { get; }

        public char Value { get; set; }
    }
}