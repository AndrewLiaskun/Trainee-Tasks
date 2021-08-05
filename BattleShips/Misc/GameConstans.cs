// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Misc
{
    public static class GameConstans
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
    }
}