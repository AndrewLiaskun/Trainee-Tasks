// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IShell
    {
        event EventHandler<KeyboardHookEventArgs> KeyPressed;

        void StartRunLoop();

        void Clear();

        void Fill(string[] array);

        void Fill(Point position, string[] array);

        void SetCursorPosition(Point point);

        void PrintTextLine(string value);

        void PrintTextLine(string value, Point cursorPosition);

        void PrintChar(char character);

        void PrintChar(char character, Point cursorPosition);

        string ReadText();

        void BackgroundColor(ShellColor color);

        void ResetColor();

        void PrintText(string value, Point cursorPosition);

        void WriteColor(ShellColor color);
    }
}