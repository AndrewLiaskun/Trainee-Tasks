// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace TicTacToe
{

    /// <summary>
    /// Event arguments for the KeyboardHook class's KeyIntercepted event.
    /// </summary>
    public class KeyboardHookEventArgs : EventArgs
    {

        private string keyName;
        private Keys keyCode;

        public KeyboardHookEventArgs(Keys evtKeyCode)
        {
            keyName = evtKeyCode.ToString();
            keyCode = evtKeyCode;
        }

        /// <summary>
        /// The name of the key that was pressed.
        /// </summary>
        public string KeyName
        {
            get { return keyName; }
        }

        /// <summary>
        /// The virtual key code of the key that was pressed.
        /// </summary>
        public Keys KeyCode
        {
            get { return keyCode; }
        }
    }
}