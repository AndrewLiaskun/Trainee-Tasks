// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe
{

    /// <summary>
    /// Event arguments for the KeyboardHook class's KeyIntercepted event.
    /// </summary>
    public class KeyboardHookEventArgs : EventArgs
    {

        private string _keyName;
        private Keys _keyCode;

        public KeyboardHookEventArgs(Keys evtKeyCode)
        {
            _keyName = evtKeyCode.ToString();
            _keyCode = evtKeyCode;
        }

        /// <summary>
        /// The name of the key that was pressed.
        /// </summary>
        public string KeyName
        {
            get { return _keyName; }
        }

        /// <summary>
        /// The virtual key code of the key that was pressed.
        /// </summary>
        public Keys KeyCode
        {
            get { return _keyCode; }
        }
    }
}