// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Utils;

using TicTacToe;

namespace BattleShips.Models
{
    public class ConsoleShell : IShell
    {
        private readonly object _syncRoot = new object();

        private HookManager _hookManager;
        private bool _isRunLoopActive;

        public ConsoleShell()
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

        public void RegisterKeyFilter(Func<KeyboardHookEventArgs, bool> filter)
        {
            _hookManager.RegisterFilter(filter);
        }

        public void StartRunLoop()
        {
            // NOTE: we need this to:
            // a) avoid console drawing problems
            // b) avoid running a run loop several times (we MUST do it only ONCE)
            // c) overall better performance and work
            lock (_syncRoot)
            {
                if (_isRunLoopActive)
                    return;

                _isRunLoopActive = true;
            }

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

        public IShell PrintTextInCenter(string text, Point point)
        {
            var length = text.Length;
            point.X = (Console.WindowWidth - length) / 2;
            return PrintText(text, point);
        }

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardHookEventArgs(key));

        private IShell DoAction(Action action)
        {
            action();
            return this;
        }
    }
}