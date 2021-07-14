// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Structs;

using TicTacToe;

namespace BattleShips.Misc
{
    public class CoordinatesMap // Закинути в борду плеєра та противника
    {
        public Point Point;

        public CoordinatesMap(Point point, Size size)
        {
            if (size.Width > 10)
                Point = GetRelativePosition(point);
            else
                Point = GetAbsolutePosition(point);
        }

        private Point GetRelativePosition(Point absolute)
        {
            int X;
            int Y;

            if (absolute.X >= GameConstants.EnemyBoard.MinWidth - 1)
            {
                X = (absolute.X - GameConstants.EnemyBoard.MinWidth) / GameConstants.Step.Right;
                Y = (absolute.Y - GameConstants.EnemyBoard.MinHeight) / GameConstants.Step.Down;
            }
            else
            {
                X = (absolute.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right;
                Y = (absolute.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down;
            }

            return new Point(X, Y);
        }

        private Point GetAbsolutePosition(Point relative)
        {
            int X = relative.X * GameConstants.Step.Right + GameConstants.EnemyBoard.MinWidth;
            int Y = relative.Y * GameConstants.Step.Down + GameConstants.EnemyBoard.MinHeight;
            return new Point(X, Y);
        }
    }
}