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
        public static int IndexOf(this StringBuilder stringBuilder, char input)
        {
            string str = stringBuilder.ToString();
            return str.IndexOf(input);
        }

        public static bool Contains(this StringBuilder stringBuilder, char input)
        {
            string str = stringBuilder.ToString();
            return str.Contains(input);
        }
    }
}