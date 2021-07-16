// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Misc
{
    // NOTE: Need to use this class inside BattleShipBoard
    public class CoordinatesMap
    {
        public CoordinatesMap(Point point, Point offset)
        {
            Point = point;
            Offset = offset;

            ZeroPoint = new Point(Point.X + Offset.X, Point.Y + Offset.Y);
        }

        public Point Point { get; }

        public Point Offset { get; }

        public Point ZeroPoint { get; }

        public Point GetRelativePosition(Point absolute)
        {
            var x = (absolute.X - ZeroPoint.X) / 3;
            var y = (absolute.Y - ZeroPoint.Y) / 2;

            return new Point(x, y);
        }

        public Point GetAbsolutePosition(Point relative)
        {
            var x = ZeroPoint.X + relative.X * 3;
            var y = ZeroPoint.Y + relative.Y * 2;

            return new Point(x, y);
        }
    }
}