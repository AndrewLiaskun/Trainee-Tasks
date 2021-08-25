// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe
{
    public class MenuCommand<TObject> : IMenuCommand
        where TObject : class
    {
        private Action<TObject> _commandAction;

        public MenuCommand(string name, Keys key, Action<TObject> command)
        {
            Name = name;
            Key = key;
            _commandAction = command;
        }

        public MenuCommand(string name, Keys key, Action command)
        {
            Name = name;
            Key = key;

            _commandAction = (_) => command();
        }

        public string Name { get; }

        public Keys Key { get; }

        public void Execute(object parametr = null) => _commandAction((TObject)parametr);
    }

    public class MenuCommand : MenuCommand<object>
    {
        public MenuCommand(string name, Keys key, Action command) : base(name, key, command)
        {
        }
    }
}