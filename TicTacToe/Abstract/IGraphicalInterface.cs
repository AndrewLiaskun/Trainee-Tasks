// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Utils;

namespace TicTacToe.Abstract
{

    public interface IGraphicalInterface
    {
        event EventHandler<KeyboardHookEventArgs> KeyPressed;

        event EventHandler<MouseHookEventArgs> MousePressed;

        void StartRunLoop();

        void Clear();

        void Fill(string[] array);

        void SetCursorPosition(Point point);

        void PrintText(string value);

        void PrintChar(char character);
    }
}