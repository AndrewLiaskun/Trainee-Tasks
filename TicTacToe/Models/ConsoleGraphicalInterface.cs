// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class ConsoleGraphicalInterface : IGraphicalInterface
    {
        private HookManager _hookManager;

        public ConsoleGraphicalInterface()
        {
            _hookManager = new HookManager();
            _hookManager.KeyIntercepted += HookManager_KeyIntercepted;
        }

        public event EventHandler<KeyboardPressedEventArgs> KeyPressed = delegate { };

        public void StartRunLoop()
        {
            MSG msg;

            while ((!NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)))
            {

                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }

        public void Clear() => Console.Clear();

        public void Fill(string[] array)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(array[i].PadLeft(60));
            }
        }

        public void SetCursorPosition(Point point) => Console.SetCursorPosition(point.X, point.Y);

        public void PrintText(string value) => Console.WriteLine(value);

        public void PrintText(string value, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.WriteLine(value);
        }

        public void PrintChar(char character) => Console.Write(character);

        public void PrintChar(char character, Point cursorPosition)
        {
            SetCursorPosition(cursorPosition);
            Console.Write(character);
        }

        public string ReadText() => Console.ReadLine();

        private void HookManager_KeyIntercepted(KeyboardPressedEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardPressedEventArgs(key));
    }
}