// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace BullsAndCows
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var game = new BullsAndCows(4);
            game.StartGame();

            Console.ReadKey();
        }
    }

    //1334   3342
    //3342   1313
}