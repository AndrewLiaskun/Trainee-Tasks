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
            Shell shell = new Shell();

            var numbers = new string[] { "   ", "1", " ", "2", " ", "3", " ", "4", " ", "5", " ", "6", " ", "7", " ", "8", " ", "9", " ", "10" };
            numbers.Select(x => x).ToList().ForEach(y => shell.PrintText(y));
            var board = new string[] { "  ╔════════════════════╗",
                "A ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "B ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "C ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "D ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "E ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "F ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "G ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "H ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "I ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "J ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                "  ╚════════════════════╝" };
            shell.Fill(board);
            shell.ReadText();
        }
    }
}