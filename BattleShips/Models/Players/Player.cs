// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract.Ships;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models.Players;

namespace BattleShips.Models
{
    internal class Player : AbstractPlayer
    {
        public Player(IVisualContext shell, PlayerBoardConfig config) : base(PlayerType.User, shell, config)
        {
        }

        public Player(IVisualContext shell, PlayerBoardConfig config, string name) : base(name, PlayerType.User, shell, config)
        {
        }

        protected override IShipFactory CreateShipFactory() => new PlayerShipGenerator(this);
    }
}