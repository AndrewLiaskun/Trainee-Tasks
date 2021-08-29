// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Models;

namespace BattleShips.Metadata
{
    [DataContract(Name = "battleship-game-history", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class GameHistoryDto
    {
        [DataMember(Name = "history")]
        public List<HistoryRecordDto> History { get; set; }

        public static GameHistoryDto FromGame(IGameHistory history)
        {
            var gameHistoryDto = new GameHistoryDto();
            gameHistoryDto.History = history.Select(HistoryRecordDto.FromHistory).ToList();

            return gameHistoryDto;
        }
    }
}