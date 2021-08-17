// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Models
{
    public class ActionContext
    {
        public ActionContext(IBattleshipGame game, Keys key)
        {
            Game = game;
            Key = key;
        }

        public IBattleshipGame Game { get; set; }

        public Keys Key { get; set; }
    }
}