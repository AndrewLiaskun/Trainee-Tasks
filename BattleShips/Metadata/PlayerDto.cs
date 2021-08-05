// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;

namespace BattleShips.Metadata
{
    [DataContract(Name = "player", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class PlayerDto
    {
        [DataMember(Name = "board")]
        public BoardDto Board { get; set; }

        [DataMember(Name = "polygon")]
        public BoardDto Polygon { get; set; }

        [DataMember(Name = "type")]
        public PlayerType Type { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public static PlayerDto FromPlayer(IPlayer player)
        {
            var metadata = new PlayerDto();

            metadata.Board = BoardDto.FromBoard(player.Board);
            metadata.Polygon = BoardDto.FromBoard(player.PolygonBoard);
            metadata.Type = player.Type;
            metadata.Name = player.Name;

            return metadata;
        }
    }
}