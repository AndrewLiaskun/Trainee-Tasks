// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace TicTacToe
{
    [Serializable]
    public class Player : IPlayer
    {
        public Player(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; }

        public string Name { get; }

        //TODO: SCORE FOR PLAYER
        public void Score() => throw new NotImplementedException();
    }
}