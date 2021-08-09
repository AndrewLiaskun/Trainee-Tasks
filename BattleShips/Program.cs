// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.Models.Visuals;

using TicTacToe;

namespace BattleShips
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var battleships = new BattleshipsGame(new ConsoleVisualContext(), new PlayerBoardConfig(Point.Empty));
            battleships.Start();
        }
    }
}