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
            public const int MinHeight = 3;

            public const int MaxHeight = 20;

            public const int MinWidth = 48;

            public const int MaxWidth = 74;
        }

        public static class PlayerBoard
        {
            public const int MinHeight = 3;

            public const int MaxHeight = 20;

            public const int MinWidth = 3;

            public const int MaxWidth = 29;
        }

        public static class Step
        {
            public const int Up = -2;

            public const int Down = 2;

            public const int Left = -3;

            public const int Right = 3;
        }
    }
}