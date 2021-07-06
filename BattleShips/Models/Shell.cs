// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Utils;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Models
{
    public class Shell : IGraphicalInterface
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
                ConsoleHelper.SetCurrentFont(array[i], 25);
                //Console.WriteLine(array[i]);
            }
        }

        public void PrintChar(char character) => Console.Write(character);

        public void PrintChar(char character, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.Write(character);
        }

        public void PrintText(string value) => Console.WriteLine(value);

        public void PrintText(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.WriteLine(value);
        }

        public string ReadText() => Console.ReadLine();

        public void SetCursorPosition(Point point) => Console.SetCursorPosition(point.X, point.Y);

        public void StartRunLoop()
        {
            MSG msg;

            while ((!TicTacToe.NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)))
            {

                TicTacToe.NativeMethods.TranslateMessage(ref msg);
                TicTacToe.NativeMethods.DispatchMessage(ref msg);
            }
        }

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardHookEventArgs(key));
    }
}