// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class Player : IPlayer
    {
        private int _score = 0;

        public Player(string name = "human", int id = 1)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; }

        public string Name { get; }

        public int Score => _score;

        public void SetWinner()
        {
            _score++;
        }
    }
}