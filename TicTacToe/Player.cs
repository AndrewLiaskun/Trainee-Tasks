// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class Player : IPlayer
    {
        public Player(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int _id { get; }

        public string _name { get; }

        //TODO: SCORE FOR PLAYER
        public void Score() => throw new NotImplementedException();
    }
}