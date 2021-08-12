// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.ViewModels.Players;

namespace BattleShips.UI.ViewModels
{
    public class BattleShipGameViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        public BattleShipGameViewModel(IBattleshipGame game)
        {
            Model = game ?? throw new ArgumentNullException(nameof(game));

            Computer = new PlayerViewModel(game.Computer);
            User = new PlayerViewModel(game.User);
        }

        public IBattleshipGame Model { get; }

        public PlayerViewModel Computer { get; }

        public PlayerViewModel User { get; }
    }
}