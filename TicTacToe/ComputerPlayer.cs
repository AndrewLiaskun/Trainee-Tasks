// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class ComputerPlayer : IPlayer
    {
        private int _score = 0;

        public ComputerPlayer()
        {
            ID = 0;
            Name = "AI";
        }

        public int ID { get; }

        public string Name { get; }

        public int Score => _score;

        public void SetWin()
        {
            _score++;
        }
    }
}