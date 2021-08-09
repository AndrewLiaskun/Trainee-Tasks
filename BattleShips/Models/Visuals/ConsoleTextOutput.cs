// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract.Visuals;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.Models.Visuals
{
    internal class ConsoleTextOutput : ITextOutput
    {
        public void SetBackgroundColor(ShellColor color) => Console.BackgroundColor = (ConsoleColor)color;

        public void ResetColor() => Console.ResetColor();

        public void SetForegroundColor(ShellColor color) => Console.ForegroundColor = (ConsoleColor)color;

        public ITextOutput EndLine() => DoAction(Console.WriteLine);

        public ITextOutput PrintText(string value, Point? position = null, bool? centered = null)
        {
            var point = position ?? Point.Empty;

            if (centered == true)
            {
                var length = value.Length;
                point.X = (Console.WindowWidth - length) / 2;
            }

            if (position != null || centered == true)
                Console.SetCursorPosition(point.X, point.Y);

            return DoAction(() => Console.Write(value));
        }

        public ITextOutput PrintChar(char character, Point? position = null)
        {
            if (position.HasValue)
                Console.SetCursorPosition(position.Value.X, position.Value.Y);

            return DoAction(() => Console.Write(character));
        }

        public string ReadText()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); // true = hide input
            }

            return Console.ReadLine();
        }

        public void Reset() => Console.Clear();

        private ITextOutput DoAction(Action action)
        {
            action();
            return this;
        }
    }
}