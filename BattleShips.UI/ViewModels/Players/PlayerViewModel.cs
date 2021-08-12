// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.ViewModels.Board;

using BattleShipsWPF.Basic;

namespace BattleShips.UI.ViewModels.Players
{
    public class PlayerViewModel : BaseViewModel
    {
        public PlayerViewModel(IPlayer player)
        {
            Board = new PlayerBoardViewModel(player.Board);
            Poligon = new PlayerBoardViewModel(player.PolygonBoard);
        }

        public PlayerBoardViewModel Board { get; }

        public PlayerBoardViewModel Poligon { get; }
    }
}