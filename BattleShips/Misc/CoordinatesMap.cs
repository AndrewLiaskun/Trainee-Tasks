// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Structs;

using TicTacToe;

namespace BattleShips.Misc
{
    // NOTE: Need to use this class inside BattleShipBoard
    public class CoordinatesMap
    {
        public CoordinatesMap(Point point, Size size)
        {
            Point = point;
            Size = size;

            if (size.Width > 10)
                Point = GetRelativePosition(point);
            else
                Point = GetAbsolutePosition(point);
        }

        public Point Point { get; }

        public Size Size { get; }

        // TODO: write this logic correctly (using only start point and size)
        public Point GetRelativePosition(Point absolute)
        {
            if (absolute.X > 48)
            {
                return new Point((int)((absolute.X - 48) / 2.9), (absolute.Y - 2) / 2);
            }
            else
            {
                return new Point((int)((absolute.X - 3) / 2.9), (absolute.Y - 2) / 2);
            }
        }

        // TODO: write this logic correctly (using only start point and size)
        public Point GetAbsolutePosition(Point relative)
        {
            return new Point((int)((relative.X + 3) * 2.9), (relative.Y + 2) * 2);
        }
    }
}