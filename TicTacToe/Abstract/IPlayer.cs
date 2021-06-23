// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

namespace TicTacToe.Abstract
{
    public interface IPlayer
    {
        int ID { get; }

        string Name { get; }

        int Score { get; }

        void SetWin();
    }
}