// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IShell
    {
        event EventHandler<KeyboardHookEventArgs> KeyPressed;

        void StartRunLoop();

        void Clear();

        void Fill(string[] array);

        void Fill(string[] array1, string[] array2, bool isCreate = false);

        void SetCursorPosition(Point point);

        void PrintText(string value);

        void PrintText(string value, Point cursorPosition);

        void PrintChar(char character);

        void PrintChar(char character, Point cursorPosition);

        string ReadText();
    }
}