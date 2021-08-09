// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe;

namespace BattleShips.Abstract.Visuals
{
    public interface IVisualContext : IDisposable
    {
        event EventHandler<KeyboardPressedEventArgs> KeyPressed;

        ITextOutput Output { get; }

        void StartRunLoop();

        void RegisterKeyFilter(Func<KeyboardPressedEventArgs, bool> filter);

        void SetCursorPosition(Point point);

        IVisualTable Create(Point startPoint);
    }
}