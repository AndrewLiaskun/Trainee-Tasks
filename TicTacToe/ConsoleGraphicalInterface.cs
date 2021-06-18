// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;
using TicTacToe.Utils;

namespace TicTacToe
{
    public class ConsoleGraphicalInterface : IGraphicalInterface
    {
        //private string[] _field = { " @ | @ | @  ", "---+---+---", " @ | @ | @  ", "---+---+---", " @ | @ | @  " };
        private HookManager HookManager;

        public ConsoleGraphicalInterface()
        {
            HookManager = new HookManager();
            HookManager.KeyIntercepted += HookManager_KeyIntercepted;
            HookManager.MouseIntercepted += HookManager_MouseIntercepted;
        }

        public event EventHandler<KeyboardHookEventArgs> KeyPressed = delegate { };

        public event EventHandler<Point> MousePressed = delegate { };

        public void Clear() => throw new NotImplementedException();

        public void Fill(string[] array)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(array[i].PadLeft(60));
            }
        }

        public void MoveCursor(Point point)
        {
            Console.SetCursorPosition(point._x, point._y);
        }

        public void PrintText(string value)
        {
            Console.WriteLine(value);
        }

        private void HookManager_MouseIntercepted(Utils.MouseHookEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HookManager_KeyIntercepted(KeyboardHookEventArgs e)
        {
            RaiseKeyPressed(e.KeyCode);
        }

        private void RaiseKeyPressed(Keys key)
        {
            KeyPressed(this, new KeyboardHookEventArgs(key));
        }
    }
}