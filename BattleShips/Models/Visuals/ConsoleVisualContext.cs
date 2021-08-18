﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Text;

using BattleShips.Abstract.Visuals;
using BattleShips.Misc;
using BattleShips.Utils;

using TicTacToe;

namespace BattleShips.Models.Visuals
{
    public class ConsoleVisualContext : IVisualContext
    {
        private readonly object _syncRoot = new object();

        private HookManager _hookManager;

        private bool _isDisposed = false;
        private bool _isRunLoopActive;
        private Point _currentPoint;

        public ConsoleVisualContext()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.UTF8;

            _hookManager = new HookManager();
            _hookManager.KeyIntercepted += HookManager_KeyIntercepted;

            ConsoleHelper.SetCurrentFont(string.Empty, 20);

            Output = new ConsoleTextOutput();
            InteractionService = new ConsoleInteractionService(this);
        }

        public event EventHandler<KeyboardPressedEventArgs> KeyPressed = delegate { };

        public event EventHandler<PositionChangedEventArgs> PositionChanged = delegate { };

        public Point CurrentPosition
        {
            get => _currentPoint;
            private set
            {
                var oldValue = _currentPoint;
                _currentPoint = value;
                RaisePositionChanged(oldValue, _currentPoint);
            }
        }

        public ITextOutput Output { get; }

        public IUserInteractionService InteractionService { get; }

        public void SetCursorPosition(Point point)
        {
            Console.SetCursorPosition(point.X, point.Y);
            CurrentPosition = point;
        }

        public void RegisterKeyFilter(Func<KeyboardPressedEventArgs, bool> filter)
            => _hookManager.RegisterFilter(filter);

        public void StartRunLoop()
        {
            // NOTE: we need this to:
            // a) avoid console drawing problems
            // b) avoid running a run loop several times (we MUST do it only ONCE)
            // c) overall better performance and work
            lock (_syncRoot)
            {
                if (_isRunLoopActive)
                    return;

                _isRunLoopActive = true;
            }

            MSG msg;

            while ((NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0)))
            {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }

        public IVisualTable Create(Point startPoint) => new ConsoleGameTable(startPoint, this);

        public void GenerateKeyPress(Keys keys) => RaiseKeyPressed(keys);

        private void RaisePositionChanged(Point oldPoint, Point newPoint)
        {
            PositionChanged(this, new PositionChangedEventArgs(oldPoint, newPoint));
        }

        private void HookManager_KeyIntercepted(KeyboardPressedEventArgs e) => RaiseKeyPressed(e.KeyCode);

        private void RaiseKeyPressed(Keys key) => KeyPressed(this, new KeyboardPressedEventArgs(key));

        #region IDisposable Support

        ~ConsoleVisualContext()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // NOTE: object is already dead :(
            if (_syncRoot == null)
                return;

            lock (_syncRoot)
            {
                if (!_isDisposed)
                {
                    if (disposing) { }

                    _hookManager.Dispose();

                    _isDisposed = true;
                }
            }
        }

        #endregion IDisposable Support
    }
}