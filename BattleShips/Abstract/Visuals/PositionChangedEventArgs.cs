// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe;

namespace BattleShips.Abstract.Visuals
{
    public class PositionChangedEventArgs : EventArgs
    {
        public PositionChangedEventArgs(Point oldPoint, Point newPoint)
        {
            OldPoint = oldPoint;
            NewPoint = newPoint;
        }

        public Point OldPoint { get; }

        public Point NewPoint { get; }
    }
}