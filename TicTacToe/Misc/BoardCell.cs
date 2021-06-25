// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Runtime.Serialization;

namespace TicTacToe
{
    public class BoardCell
    {
        public const char DefaultCharValue = ' ';
        public const char ZeroChar = 'O';
        public const char CrossChar = 'X';
        public const char TieChar = 'T';

        public BoardCell(Point point, char value = DefaultCharValue)
        {
            Point = point;
            Value = value;
        }

        public BoardCell()
        {
        }

        public Point Point { get; set; }

        public char Value { get; set; }
    }
}