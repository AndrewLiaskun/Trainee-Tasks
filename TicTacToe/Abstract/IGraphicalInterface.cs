// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe.Abstract
{
    public interface IGraphicalInterface
    {
        event EventHandler<KeyboardPressedEventArgs> KeyPressed;

        void StartRunLoop();

        void Clear();

        void Fill(string[] array);

        void SetCursorPosition(Point point);

        void PrintText(string value);

        void PrintText(string value, Point cursorPosition);

        void PrintChar(char character);

        void PrintChar(char character, Point cursorPosition);

        string ReadText();
    }
}