// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Enums;

using static TicTacToe.Utils.MouseHook;

namespace TicTacToe.Utils
{
    public class MouseHookEventArgs : EventArgs
    {
        //TODO: READONLY
        private Point _p;

        private MouseMessages _mouseKey;

        public MouseHookEventArgs(Point p, MouseMessages key)
        {
            _p = p;
            _mouseKey = key;
        }

        public MouseMessages MouseKey => _mouseKey;

        public Point MousePoint => _p;
    }
}