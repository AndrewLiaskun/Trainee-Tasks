// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe
{
    public struct Point
    {

        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public static Point Empty => new Point();

        public int X { get; set; }

        public int Y { get; set; }

        public static bool operator ==(Point thisPoint, Point otherPoint)
        {
            if (thisPoint.X == otherPoint.X && thisPoint.Y == otherPoint.Y)
                return true;
            else
                return false;
        }

        public static bool operator !=(Point thisPoint, Point otherPoint)
        {
            return !(thisPoint == otherPoint);
        }
    }
}