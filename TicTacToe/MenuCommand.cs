// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class MenuCommand
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