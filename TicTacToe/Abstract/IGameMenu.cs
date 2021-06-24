// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

namespace TicTacToe
{
    public interface IGameMenu
    {
        IReadOnlyList<IMenuCommand> Commands { get; }

        void HandleKey(Keys key);
        void Print();
    }
}