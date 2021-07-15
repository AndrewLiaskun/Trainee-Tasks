// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Structs;

using TicTacToe;

namespace BattleShips.Misc
{
    // NOTE: Need to use this class inside BattleShipBoard
    public class CoordinatesMap
    {
        public CoordinatesMap(Point point)
        {
            Point = point;
        }

        public Point Point { get; }

        public Size Size { get; }

        // TODO: write this logic correctly (using only start point and size)
        public Point GetRelativePosition(Point absolute)
        {

            return new Point((int)((absolute.X - Point.X) / 2.9), (absolute.Y - Point.Y) / 2);
        }

        // TODO: write this logic correctly (using only start point and size)
        public Point GetAbsolutePosition(Point relative)
        {
            return new Point((int)((relative.X + Point.X) * 2.9), (relative.Y + Point.Y) * 2);
        }
    }
}