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
            _id = 0;
            _name = "AI";
        }

        public int _id { get; }

        public string _name { get; }

        public void Score() => throw new NotImplementedException();
    }
}