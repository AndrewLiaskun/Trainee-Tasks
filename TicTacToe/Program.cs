// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe.Abstract;
using TicTacToe.Enums;
using TicTacToe.Utils;

namespace TicTacToe
{

    internal class Program
    {

        private static void Main(string[] args)
        {

            TicTacToe ticTacToe = new TicTacToe();
            ticTacToe.Start();
            Console.ReadLine();
        }
    }
}