// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract.Visuals;

using TicTacToe;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiVisualContext : IVisualContext
    {
        public static readonly UiVisualContext Instance = new UiVisualContext();

        private UiVisualContext()
        {
            Output = new UiTextOutput();
            InteractionService = new UiInteractionService(this);
        }

        public event EventHandler<PositionChangedEventArgs> PositionChanged = delegate { };

        public event EventHandler<KeyboardPressedEventArgs> KeyPressed = delegate { };

        public ITextOutput Output { get; }

        public IUserInteractionService InteractionService { get; }

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

        public void GenerateKeyPress(Keys keys) => RaiseKeyPressed(keys);

        private void RaisePositionChanged(Point oldPoint, Point newPoint)
        {
            PositionChanged(this, new PositionChangedEventArgs(oldPoint, newPoint));
        }

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardPressedEventArgs(key));
    }
}