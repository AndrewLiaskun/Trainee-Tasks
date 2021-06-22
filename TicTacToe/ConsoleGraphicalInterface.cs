// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TicTacToe.Abstract;
using TicTacToe.Enums;
using TicTacToe.Utils;

using static TicTacToe.Utils.MouseHook;

namespace TicTacToe
{
    public class ConsoleGraphicalInterface : IGraphicalInterface
    {
        private HookManager _hookManager;
        private MouseHook _mouseHook;

        public ConsoleGraphicalInterface()
        {
            _hookManager = new HookManager();
            _mouseHook = new MouseHook();
            _hookManager.KeyIntercepted += HookManager_KeyIntercepted;
            _mouseHook.MouseIntercepted += _mouseHook_MouseIntercepted;
        }

        public event EventHandler<KeyboardHookEventArgs> KeyPressed = delegate { };

        public event EventHandler<MouseHookEventArgs> MousePressed = delegate { };

        public void StartRunLoop()
        {
            MSG msg;

            while ((!NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)))
            {

                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void Fill(string[] array)
        {
            for (int i = 0; i < 5; i++)
            {
                //Console.WriteLine(String.Format("{0," + (Console.BufferWidth - 4) + "}", array[i]));
                Console.WriteLine(array[i].PadLeft(60));
            }
        }

        public void SetCursorPosition(Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
        }

        public void PrintText(string value)
        {
            Console.WriteLine(value);
        }

        public void PrintChar(char character) => Console.Write(character);

        private void _mouseHook_MouseIntercepted(MouseHookEventArgs e)
        {
            RaiseMousePresed(e);
        }

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e)
        {
            RaiseKeyPressed(e.KeyCode);
        }

        private void RaiseKeyPressed(Keys key)
        {
            KeyPressed(this, new KeyboardHookEventArgs(key));
        }

        private void RaiseMousePresed(MouseHookEventArgs e)
        {
            MousePressed(this, e);
        }
    }
}