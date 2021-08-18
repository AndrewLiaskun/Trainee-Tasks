// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe;

namespace BattleShips.Abstract.Visuals
{
    public interface IVisualContext : IDisposable
    {
        event EventHandler<KeyboardPressedEventArgs> KeyPressed;

        event EventHandler<PositionChangedEventArgs> PositionChanged;

        ITextOutput Output { get; }

        IUserInteractionService InteractionService { get; }
        Point CurrentPosition { get; set; }

        void StartRunLoop();

        void RegisterKeyFilter(Func<KeyboardPressedEventArgs, bool> filter);

        void SetCursorPosition(Point point);

        IVisualTable Create(Point startPoint);

        void GenerateKeyPress(Keys keys);
    }
}