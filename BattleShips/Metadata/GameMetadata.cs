// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;
using System.Runtime.Serialization;

using BattleShips.Abstract;
using BattleShips.Metadata;
using BattleShips.Utils;

using TicTacToe;

namespace BattleShips.Misc
{
    [DataContract(Name = "game", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class GameMetadata : IMetadata
    {
        [DataMember(Name = "players")]
        public PlayerDto[] Players { get; set; }

        public static GameMetadata FromGame(IPlayer player, IPlayer opponent)
        {
            var game = new GameMetadata();

            game.Players = new[] { PlayerDto.FromPlayer(player), PlayerDto.FromPlayer(opponent) };

            return game;
        }
    }
}