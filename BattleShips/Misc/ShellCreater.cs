﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace BattleShips.Misc
{
    public class ShellCreater
    {
        public void Create(IGraphicalInterface shell)
        {
            var board = new string[] {  "   1 2 3 4 5 6 7 8 9 10\t\t\t\t\t   1 2 3 4 5 6 7 8 9 10",
                                        "  ╔════════════════════╗\t\t\t\t  ╔════════════════════╗",
                                        "A ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tA ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "B ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tB ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "C ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tC ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "D ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tD ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "E ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tE ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "F ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tF ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "G ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tG ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "H ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tH ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "I ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tI ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "J ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║\t\t\t\tJ ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║",
                                        "  ╚════════════════════╝\t\t\t\t  ╚════════════════════╝" };
            shell.Fill(board);
        }
    }
}