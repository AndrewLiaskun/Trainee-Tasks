// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;

namespace BattleShips.Misc
{
    public class PlayerShipGenerator : AbstractShipGenerator
    {
        public PlayerShipGenerator(IPlayer player) : base(player)
        {
        }
    }
}