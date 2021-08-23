// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public class HistoryRecord
    {
        public HistoryRecord(PlayerType shooter, Point point, bool isShipCell)
        {
            Shooter = shooter;
            Point = point;
            IsShipCell = isShipCell;
        }

        public PlayerType Shooter { get; }

        public Point Point { get; }

        public bool IsShipCell { get; }
    }
}