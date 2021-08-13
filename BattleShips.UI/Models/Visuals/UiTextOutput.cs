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

        public ITextOutput PrintChar(char character, Point? position = null) => DoAction(() => _text.Append(character));

        public ITextOutput PrintText(string value, Point? position = null, bool? centered = null) => DoAction(() => _text.Append(value));

        public string ReadText() => Text;

        public void Reset() => _text.Clear();

        public void ResetColor()
        {
        }

        public void SetBackgroundColor(ShellColor color)
        {
        }

        public void SetForegroundColor(ShellColor color)
        {
        }

        private ITextOutput DoAction(Action action)
        {
            action();

            RaisePropertyChanged(nameof(Text));

            return this;
        }
    }
}