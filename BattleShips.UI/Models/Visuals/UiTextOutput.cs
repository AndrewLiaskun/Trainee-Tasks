// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using BattleShips.Abstract.Visuals;
using BattleShips.Enums;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiTextOutput : ITextOutput
    {

        public ITextOutput EndLine() => throw new NotImplementedException();

        public ITextOutput PrintChar(char character, Point? position = null) => DoAction(() => new TextBlock { Text = character.ToString() });

        public ITextOutput PrintText(string value, Point? position = null, bool? centered = null) => DoAction(() => new TextBlock { Text = value });

        public string ReadText() => throw new NotImplementedException();

        public void Reset() => throw new NotImplementedException();

        public void ResetColor() => throw new NotImplementedException();

        public void SetBackgroundColor(ShellColor color) => throw new NotImplementedException();

        public void SetForegroundColor(ShellColor color) => throw new NotImplementedException();

        private ITextOutput DoAction(Action action)
        {
            action();
            return this;
        }
    }
}