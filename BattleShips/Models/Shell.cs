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

            ConsoleHelper.SetCurrentFont(string.Empty, 20);
        }

        public event EventHandler<KeyboardHookEventArgs> KeyPressed = delegate { };

        public void Clear() => Console.Clear();

        public void Fill(string[] array)
        {
            PrintText("\n");

            for (int i = 0; i < array.Length; i++)
                PrintText(array[i]);
        }

        public void Fill(Point position, string[] array)
        {
            PrintText("\n", position);

            var y = position.Y;

            for (int i = 0; i < array.Length; i++)
            {
                var point = new Point(position.X, y);
                PrintText(array[i], point).EndLine();
                y++;
            }
        }

        public IShell PrintChar(char character) => DoAction(() => Console.Write(character));

        public IShell PrintChar(char character, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            return PrintChar(character);
        }

        public IShell PrintText(string value) => DoAction(() => Console.Write(value));

        public IShell PrintText(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            return PrintText(value);
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

        public void SetBackgroundColor(ShellColor color) => Console.BackgroundColor = (ConsoleColor)color;

        public void ResetColor() => Console.ResetColor();

        public void SetForegroundColor(ShellColor color) => Console.ForegroundColor = (ConsoleColor)color;

        public IShell EndLine() => DoAction(() => Console.WriteLine());

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardHookEventArgs(key));

        private IShell DoAction(Action action)
        {
            action();
            return this;
        }
    }
}