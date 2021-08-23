﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models
{
    public class ActionContext
    {
        public ActionContext(Keys key, IBattleshipGame game, IGameMenu menu)
        {
            Game = game ?? throw new ArgumentNullException(nameof(game));
            Key = key;
            GameMenu = menu;
            CurrentState = game.State;
        }

        public IBattleshipGame Game { get; }

        public Keys Key { get; set; }

        public IPlayer Player => Game.User;

        public IPlayer Ai => Game.Computer;

        public Point ActiveBoardPosition => ActiveBoard.CurrentPosition;

        public IGameMenu GameMenu { get; set; }

        public bool IsRandomPlacement { get; set; }

        public BattleShipsState CurrentState { get; }

        public List<HistoryRecord> GameHistory { get; set; }

        public IBattleShipBoard ActiveBoard => Game.ActiveBoard;
    }
}