// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models.Players;
using BattleShips.Ships.Generators;

namespace BattleShips.Models
{

    internal class AiPlayer : AbstractPlayer
    {

        public AiPlayer(IShell shell, PlayerBoardConfig config) : base(PlayerType.Computer, shell, config)
        {
        }

        protected override IShipFactory CreateShipFactory() => new PlayerShipGenerator();
    }
}