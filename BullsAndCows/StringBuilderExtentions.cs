// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    internal static class StringBuilderExtentions
    {
        public static int IndexOf(this StringBuilder builder, char symbol)
        {
            for (int i = 0; i < builder.Length; i++)
            {
                if (builder[i] == symbol)
                    return i;
            }

            return -1;
        }

        public static bool Contains(this StringBuilder builder, char symbol)
            => builder.IndexOf(symbol) >= 0;
    }
}