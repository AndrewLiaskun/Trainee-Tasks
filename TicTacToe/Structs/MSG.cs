// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    [Serializable]
    public struct MSG
    {
        public IntPtr hwnd;

        public IntPtr lParam;

        public int message;

        public int pt_x;

        public int pt_y;

        public int time;

        public IntPtr wParam;
    }
}