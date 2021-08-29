// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;

using TicTacToe;

namespace BattleShips.Metadata
{
    [DataContract(Name = "history-record", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class HistoryRecordDto
    {
        [DataMember(Name = "shooter")]
        public string Shooter { get; set; }

        [DataMember(Name = "player-type")]
        public PlayerType PlayerType { get; set; }

        [DataMember(Name = "ship-type")]
        public ShipDto Ship { get; set; }

        [DataMember(Name = "point")]
        public Point Point { get; set; }

        public static HistoryRecordDto FromHistory(IHistoryRecord record)
        {
            var recordDto = new HistoryRecordDto();

            recordDto.Shooter = record.Shooter;
            recordDto.Point = record.Point;
            recordDto.Ship = ShipDto.FromShipState(record.Ship);
            recordDto.PlayerType = record.PlayerType;

            return recordDto;
        }

        public IHistoryRecord GetRecord() => new HistoryRecord(Shooter, Ship.GetState(), Point, PlayerType);
    }
}