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

        void FillAtCenter(string[] text, Point position);

        void Fill(Point position, string[] array);

        void SetCursorPosition(Point point);

        IShell PrintText(string value);

        void RegisterKeyFilter(Func<KeyboardHookEventArgs, bool> filter);

        IShell PrintChar(char character);

        IShell PrintChar(char character, Point cursorPosition);

        IShell EndLine();

        string ReadText();

        void SetBackgroundColor(ShellColor color);

        void ResetColor();

        IShell PrintText(string value, Point cursorPosition);

        void SetForegroundColor(ShellColor color);

        IShell PrintTextInCenter(string text, Point point);
    }
}