// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;

namespace BattleShips.Models.Visuals
{
    public class QuestionParams
    {
        private bool? _isWin = null;

        public ShellColor ForegroundColor { get; set; } = ShellColor.Red;

        public bool? IsWin
        {
            get => _isWin;
            set
            {
                _isWin = value;
                if (_isWin == true)
                    ForegroundColor = ShellColor.Blue;
            }
        }
    }
}