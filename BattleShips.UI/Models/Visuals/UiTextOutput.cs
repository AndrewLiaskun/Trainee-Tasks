// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Text;

using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.UI.Basic;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiTextOutput : BaseViewModel, ITextOutput
    {
        private StringBuilder _text;

        public UiTextOutput()
        {
            _text = new StringBuilder(4096);
        }

        public string Text => _text.ToString();

        public ITextOutput EndLine() => DoAction(() => _text.AppendLine());

        public ITextOutput PrintChar(char character, Point? position = null) => throw new NotImplementedException();

        public ITextOutput PrintText(string value, Point? position = null, bool? centered = null) => throw new NotImplementedException();

        public string ReadText() => throw new NotImplementedException();

        public void Reset() => _text.Clear();

        public void ResetColor() => throw new NotImplementedException();

        public void SetBackgroundColor(ShellColor color) => throw new NotImplementedException();

        public void SetForegroundColor(ShellColor color) => throw new NotImplementedException();

        private ITextOutput DoAction(Action action)
        {
            action();

            RaisePropertyChanged(nameof(Text));

            return this;
        }
    }
}