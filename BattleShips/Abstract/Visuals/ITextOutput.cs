// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Abstract.Visuals
{
    public interface ITextOutput
    {
        void SetForegroundColor(ShellColor color);

        void SetBackgroundColor(ShellColor color);

        void ResetColor();

        ITextOutput PrintText(string value, Point? position = null, bool? centered = null);

        ITextOutput PrintChar(char character, Point? position = null);

        ITextOutput EndLine();

        string ReadText();

        void Reset();
    }
}