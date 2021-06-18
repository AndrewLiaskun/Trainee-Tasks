// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.SmallBasic.Library;

namespace TicTacToe
{

    public static class Keyboard
    {

        public static void detect()
        {
            short oldState = GetKeyState(0x01);

            while (true)
            {
                short newState = GetKeyState(0x01);

                if (oldState != newState)
                {
                    oldState = newState;

                    if (newState < 0)
                    {
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int vKey);
    }
}