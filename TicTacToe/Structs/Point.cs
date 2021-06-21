// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public struct Point
    {
        public Point(int x = 0, int y = 0)
        {
            _x = x;
            _y = y;
        }

        public int _x { get; set; }

        public int _y { get; set; }

        public override string ToString() => $"Point:({_x},{_y})";
    }
}