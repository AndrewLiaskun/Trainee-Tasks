// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public IVisualTable Create(Point startPoint) => throw new NotImplementedException();

        public void Dispose()
        {
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