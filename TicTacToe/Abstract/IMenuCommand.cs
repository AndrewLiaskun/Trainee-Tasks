// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

namespace TicTacToe
{
    public interface IMenuCommand
    {
        Keys Key { get; }
        string Name { get; }

        void Execute();
    }
}