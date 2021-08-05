// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe;

namespace BattleShips.Misc
{
    public class PlayerBoardConfig
    {
        public PlayerBoardConfig()
        {
        }

        public PlayerBoardConfig(Point point)
        {
            SelfBoardStartPoint = point;
            OpponentBoardStartPoint = new Point(point.X + 44, point.Y);
        }

        public Point SelfBoardStartPoint { get; set; }

        public Point OpponentBoardStartPoint { get; set; }
    }
}