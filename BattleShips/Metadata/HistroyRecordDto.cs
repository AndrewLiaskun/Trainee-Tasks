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
    public class HistroyRecordDto
    {
        [DataMember(Name = "shooter")]
        public string Shooter { get; set; }

        [DataMember(Name = "player-type")]
        public PlayerType PlayerType { get; set; }

        [DataMember(Name = "ship-type")]
        public ShipStateDto Ship { get; set; }

        [DataMember(Name = "point")]
        public Point Point { get; set; }

        public static HistroyRecordDto FromHistory(IHistoryRecord record)
        {
            var recordDto = new HistroyRecordDto();

            recordDto.Shooter = record.Shooter;
            recordDto.Point = record.Point;
            recordDto.Ship = CreateShipDto(record.Ship);
            recordDto.PlayerType = record.PlayerType;

            return recordDto;
        }

        public IHistoryRecord GetRecord() => new HistoryRecord(Shooter, Ship.GetState(), Point, PlayerType);

        private static ShipStateDto CreateShipDto(ShipState shipState) => ShipStateDto.FromShipState(shipState);
    }
}