// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models.Players;

namespace BattleShips.Models
{
    internal class Player : AbstractPlayer
    {
        public Player(IShell shell, PlayerBoardConfig config) : base(PlayerType.User, shell, config)
        {
        }

        protected override IShipFactory CreateShipFactory() => new PlayerShipGenerator();
    }
}