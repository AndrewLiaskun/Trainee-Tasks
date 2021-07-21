// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe;

namespace BattleShips.Misc
{
    public static class GameConstants
    {
        public const char Ship = '█';
        public const char Miss = '*';
        public const char Got = 'X';
        public const char Empty = ' ';

        public static class Step
        {
            public const int Up = -2;

            public const int Down = 2;

            public const int Left = -3;

            public const int Right = 3;
        }

        public static class BoardMeasures
        {
            public const int MinIndex = 0;
            public const int MaxIndex = 9;
            public const int Step = 1;

            public static readonly Point Offset = new Point(3, 2);

            public static bool IsInValidRange(int coordinate)
            {
                return coordinate >= MinIndex && coordinate <= MaxIndex;
            }
        }
    }
}