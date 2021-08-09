// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract.Ships;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models.Players;

namespace BattleShips.Models
{

    internal class AiPlayer : AbstractPlayer
    {

        public AiPlayer(IVisualContext shell, PlayerBoardConfig config) : base(PlayerType.Computer, shell, config)
        {
        }

        protected override IShipFactory CreateShipFactory() => new PlayerShipGenerator(this);
    }
}