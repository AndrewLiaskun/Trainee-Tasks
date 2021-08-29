// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

namespace BattleShips.Metadata
{
    [DataContract(Name = "save-player", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class PlayerMetadate
    {
        [DataMember(Name = "player")]
        public PlayerDto Player { get; set; }

        public static PlayerMetadate FromPlayer(IPlayer user)
        {
            var player = new PlayerMetadate();

            player.Player = PlayerDto.FromPlayer(user);

            return player;
        }
    }
}