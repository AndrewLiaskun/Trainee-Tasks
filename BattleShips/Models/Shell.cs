// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Utils;

using TicTacToe;

namespace BattleShips.Models
{
    public class Shell : IShell
    {
        private HookManager _hookManager;

        public Shell()
        {
            _hookManager = new HookManager();
            _hookManager.KeyIntercepted += HookManager_KeyIntercepted;
        }

        public event EventHandler<KeyboardHookEventArgs> KeyPressed;

        public void Clear() => Console.Clear();

        public void Fill(string[] array)
        {
            PrintText("\n");
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 2)
                {
                    for (int j = 65; j < 75; j++)
                    {
                        PrintText(((char)j) + array[i]);
                    }
                }
                else
                    PrintText(array[i]);
            }
        }

        public void Fill(string[] array1, string[] array2, bool isCreate = false)
        {
            PrintText("\n");
            int k = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                if (i == 2)
                {
                    var letter = ((char)65);
                    while (letter != 75)
                    {
                        if (isCreate)
                        {
                            if (letter == 66 || letter == 68 || letter == 70 || letter == 72)
                            {

                                PrintText(letter + array1[i].PadRight(30) + letter + array2[k]);
                                k++;
                            }
                            else
                                PrintText(letter + array1[i].PadRight(30) + letter + array1[i]);
                        }
                        else
                        {
                            PrintText(letter + array1[i].PadRight(30) + letter + array2[i]);
                        }
                        letter++;
                    }
                }
                else
                    PrintText(array1[i] + array1[i].PadLeft(31));
            }
        }

        public void PrintChar(char character) => Console.Write(character);

        public void PrintChar(char character, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.Write(character);
        }

        public void PrintText(string value) => ConsoleHelper.SetCurrentFont(value, 25);

        public void PrintText(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            ConsoleHelper.SetCurrentFont(value, 25);
        }

        public string ReadText() => Console.ReadLine();

        public void SetCursorPosition(Point point) => Console.SetCursorPosition(point.X, point.Y);

        public void StartRunLoop()
        {
            MSG msg;

            while ((NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)))
            {

                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardHookEventArgs(key));
    }
}