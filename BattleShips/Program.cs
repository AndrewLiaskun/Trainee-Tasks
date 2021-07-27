// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Models;
using BattleShips.Utils;

namespace BattleShips
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BattleshipsGame battleships = new BattleshipsGame();
            battleships.Start();
        }
    }
}