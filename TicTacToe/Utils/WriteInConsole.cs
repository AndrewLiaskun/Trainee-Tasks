// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Utils
{
    public class WriteInConsole
    {
        /* Copies a number of characters to consecutive cells of a console screen buffer,
     beginning at a specified location. */

        public static uint WriteConsoleLine(string sLine)
        {

            uint cchWritten;
            if (!WriteConsole(GetStdOut(),
                         sLine,
                         (uint)sLine.Length,
                         out cchWritten,
                         (IntPtr)0))
                return 0;
            return cchWritten;
        }

        public static IntPtr GetStdOut()
        {
            const int STD_OUTPUT_HANDLE = -11;
            IntPtr iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            return iStdOut;
        }

        [DllImport("kernel32.dll")]
        private static extern bool WriteConsole(IntPtr hConsoleOutput, string lpBuffer,
           uint nNumberOfCharsToWrite, out uint lpNumberOfCharsWritten,
           IntPtr lpReserved);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
    }
}