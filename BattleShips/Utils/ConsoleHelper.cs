// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Runtime.InteropServices;

using BattleShips.Structs;

namespace BattleShips.Utils
{

    public static class ConsoleHelper
    {

        private const int FixedWidthTrueType = 54;
        private const int StandardOutputHandle = -11;

        private static readonly IntPtr ConsoleOutputHandle = NativeApi.GetStdHandle(StandardOutputHandle);

        public static FontInfo[] SetCurrentFont(string font, short fontSize = 0)
        {
            Console.WriteLine(font);

            FontInfo before = new FontInfo
            {
                cbSize = Marshal.SizeOf<FontInfo>()
            };

            if (NativeApi.GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref before))
            {

                FontInfo set = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>(),
                    FontIndex = 0,
                    FontFamily = FixedWidthTrueType,
                    FontName = font,
                    FontWeight = 400,
                    FontSize = fontSize > 0 ? fontSize : before.FontSize
                };

                // Get some settings from current font.
                if (!NativeApi.SetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref set))
                {
                    var ex = Marshal.GetLastWin32Error();
                    Console.WriteLine("Set error " + ex);
                    throw new System.ComponentModel.Win32Exception(ex);
                }

                FontInfo after = new FontInfo
                {
                    cbSize = Marshal.SizeOf<FontInfo>()
                };
                NativeApi.GetCurrentConsoleFontEx(ConsoleOutputHandle, false, ref after);

                return new[] { before, set, after };
            }
            else
            {
                var er = Marshal.GetLastWin32Error();
                Console.WriteLine("Get error " + er);
                throw new System.ComponentModel.Win32Exception(er);
            }
        }
    }
}