// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public class HistoryRecord : IHistoryRecord
    {
        public HistoryRecord(string shooter, Point point, IShip ship)
        {
            Shooter = shooter;
            Point = point;
            Ship = ship;
        }

        public string Shooter { get; }

        public Point Point { get; }

        public IShip Ship { get; }

        public override string ToString()
        {
            var isDamaged = IsShipCell ? "damage ship!" : "miss!";
            return $"{Shooter} shoot in {Point} and this is {isDamaged}";
        }
    }
}