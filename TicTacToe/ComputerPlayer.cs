// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class ComputerPlayer : IPlayer
    {
        public ComputerPlayer()
        {
            ID = 0;
            Name = "AI";
        }

        public int ID { get; }

        public string Name { get; }

        public void Score() => throw new NotImplementedException();
    }
}