// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static TicTacToe.Utils.MouseHook;

namespace TicTacToe.Utils
{
    public class MouseHookEventArgs : EventArgs
    {
        private Point _p;

        public MouseHookEventArgs(Point p)
        {
            _p = p;
        }

        public Point MouseKey => _p;
    }
}