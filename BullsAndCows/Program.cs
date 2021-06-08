// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BullsAndCows bullsAndCows = new BullsAndCows();
            bullsAndCows.StartGame("2453");
        }
    }
}