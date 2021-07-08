// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

namespace BattleShips.Misc
{
    public static class GameConstants
    {
        public const char Ship = '█';
        public const char Select = '░';
        public const char Miss = '*';
        public const char Got = 'X';

        public static class EnemyBoard
        {
            public const int MinHeight = 13;

            public const int MaxHeight = 4;

            public const int MinWidth = 34;

            public const int MaxWidth = 52;
        }

        public static class PlayerBoard
        {
            public const int MinHeight = 13;

            public const int MaxHeight = 4;

            public const int MinWidth = 3;

            public const int MaxWidth = 21;
        }

        public static class Step
        {
            public const int Up = -1;

            public const int Down = 1;

            public const int Left = -2;

            public const int Right = 2;
        }
    }
}