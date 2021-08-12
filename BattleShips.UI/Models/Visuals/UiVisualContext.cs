// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract.Visuals;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiVisualContext : IVisualContext
    {
        public UiVisualContext()
        {
            Output = new UiTextOutput();
        }

        public event EventHandler<KeyboardPressedEventArgs> KeyPressed = delegate { };

        public ITextOutput Output { get; }

        public IVisualTable Create(Point startPoint) => new UiGameTable(startPoint);

        public void Dispose()
        {
            // NOTE: no need to free any resources
        }

        public void RegisterKeyFilter(Func<KeyboardPressedEventArgs, bool> filter)
        {
        }

        public void SetCursorPosition(Point point)
        {
        }

        public void StartRunLoop()
        {
        }
    }
}