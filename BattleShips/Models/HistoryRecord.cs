// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;
using static BattleShips.Resources.Сonversations.GameConverstations;
using TicTacToe;
using BattleShips.Misc;
using BattleShips.Resources.Сonversations;

namespace BattleShips.Models
{
    public class HistoryRecord : IHistoryRecord
    {

        public HistoryRecord(string shooter, ShipState ship, Point point, PlayerType playerType = PlayerType.Unknown)
        {
            Shooter = shooter;
            PlayerType = playerType;
            Ship = ship;
            Point = point;
        }

        public string Shooter { get; }

        public PlayerType PlayerType { get; }

        public ShipState Ship { get; }

        public Point Point { get; }

        public override string ToString()
        {
            var shipState = Ship.IsAlive == true ? DefaultMessage : PlayerAnswer();
            return $"{Shooter}:\n {shipState}";
        }

        private string PlayerAnswer() => ResourceManager.GetString($"{PlayerType}{Ship.ShipKind}");
    }
}