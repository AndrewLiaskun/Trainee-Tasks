// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe
{
    public class MenuCommand : IMenuCommand
    {
        private Action _commandAction;

        public MenuCommand(string name, Keys key, Action command)
        {
            Name = name;
            Key = key;
            _commandAction = command;
        }

        public string Name { get; }

        public Keys Key { get; }

        public void Execute() => _commandAction();
    }
}