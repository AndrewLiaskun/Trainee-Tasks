// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe.Abstract
{
    public interface IGraphicalInterface
    {
        event EventHandler<KeyboardHookEventArgs> KeyPressed;

        void StartRunLoop();

        void Clear();

        void Fill(string[] array);

        void SetCursorPosition(Point point);

        void PrintText(string value);

        void PrintChar(char character);

        string ReadText();
    }
}