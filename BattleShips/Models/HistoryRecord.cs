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

namespace BattleShips.Models
{
    public class HistoryRecord : IHistoryRecord
    {

        public HistoryRecord(string shooter, IShip ship, PlayerType playerType = PlayerType.Unknown)
        {
            Shooter = shooter;
            PlayerType = playerType;
            Ship = ship;
        }

        public string Shooter { get; }

        public PlayerType PlayerType { get; }

        public IShip Ship { get; }

        public override string ToString()
        {
            var shipState = Ship.IsAlive ? DefaultMessage : PlayerAnswer();
            return $"{Shooter}:\n {shipState}";
        }

        private string PlayerAnswer()
        {
            string shipTrigger = null;
            var user = PlayerType == PlayerType.User;
            switch (Ship.Deck)
            {
                case 1:
                    shipTrigger = user ? PlayerTorpedoboat : ComputerTorpedoboat;
                    break;

                case 2:
                    shipTrigger = user ? PlayerDestroyer : ComputerDestroyer;
                    break;

                case 3:
                    shipTrigger = user ? PlayerCruiser : ComputerCruiser;
                    break;

                case 4:
                    shipTrigger = user ? PlayerBattleship : ComputerBattleship;
                    break;

                default:
                    break;
            }
            return shipTrigger;
        }
    }
}