// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Utils;

using TicTacToe;

namespace BattleShips.Models
{
    public class Shell : IShell
    {
        private HookManager _hookManager;

        public Shell()
        {
            Console.CursorVisible = false;
            _hookManager = new HookManager();
            _hookManager.KeyIntercepted += HookManager_KeyIntercepted;

            ConsoleHelper.SetCurrentFont(string.Empty, 25);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyPressed = delegate { };

        public void Clear() => Console.Clear();

        public void Fill(string[] array)
        {
            PrintTextLine("\n");

            for (int i = 0; i < array.Length; i++)
                PrintTextLine(array[i]);
        }

        public void Fill(Point position, string[] array)
        {
            PrintTextLine("\n", position);

            var y = position.Y;

            for (int i = 0; i < array.Length; i++)
            {
                var point = new Point(position.X, y);
                PrintTextLine(array[i], point);
                y++;
            }
        }

        public void PrintChar(char character) => Console.Write(character);

        public void PrintChar(char character, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.Write(character);
        }

        public void PrintTextLine(string value) => Console.WriteLine(value);

        public void PrintTextLine(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.WriteLine(value);
        }

        public void PrintText(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.Write(value);
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

        public void BackgroundColor(ShellColor color) => Console.BackgroundColor = (ConsoleColor)color;

        public void ResetColor() => Console.ResetColor();

        public void WriteColor(ShellColor color) => Console.ForegroundColor = (ConsoleColor)color;

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardHookEventArgs(key));
    }
}