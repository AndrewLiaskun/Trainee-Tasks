// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using TicTacToe.Structs;

namespace TicTacToe
{
    /// <summary>
    /// Delegate for KeyboardHook event handling.
    /// </summary>
    /// <param name="e">An instance of InterceptKeysEventArgs.</param>
    public delegate void KeyboardHookEventHandler(KeyboardPressedEventArgs e);

    public delegate IntPtr HookHandlerDelegate(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam);

    // Structure returned by the hook whenever a key is pressed

    public class HookManager : IDisposable
    {

        private const int WH_KEYBOARD_LL = 13;

        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYUP = 0x0105;

        //Variables used in the call to SetWindowsHookEx
        private HookHandlerDelegate _proc;

        private IntPtr _hookID = IntPtr.Zero;

        private Func<KeyboardPressedEventArgs, bool> _keyFilter;

        /// <summary>
        /// Sets up a keyboard hook to trap all keystrokes without
        /// passing any to other applications.
        /// </summary>
        public HookManager()
        {
            _proc = new HookHandlerDelegate(HookCallback);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                var moduleHandle = NativeMethods.GetModuleHandle(curModule.ModuleName);
                _hookID = NativeMethods.SetWindowsHookEx(WH_KEYBOARD_LL, _proc, moduleHandle, 0);
            }
        }

        /// <summary>
        /// Event triggered when a keystroke is intercepted by the
        /// low-level hook.
        /// </summary>
        public event KeyboardHookEventHandler KeyIntercepted;

        public void RegisterFilter(Func<KeyboardPressedEventArgs, bool> filter)
        {
            _keyFilter += filter;
        }

        /// <summary>
        /// Releases the keyboard hook.
        /// </summary>
        public void Dispose()
        {
            NativeMethods.UnhookWindowsHookEx(_hookID);
        }

        /// <summary>
        /// Processes the key event captured by the hook.
        /// </summary>
        private IntPtr HookCallback(int nCode, IntPtr wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            //Filter wParam for KeyUp events only
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
                {
                    OnKeyIntercepted(new KeyboardPressedEventArgs((Keys)lParam.vkCode));
                }
            }
            //Pass key to next application
            return NativeMethods.CallNextHookEx(_hookID, nCode, wParam, ref lParam);
        }

        /// <summary>
        /// Raises the KeyIntercepted event.
        /// </summary>
        /// <param name="e">An instance of KeyPressEventArgs</param>
        private void OnKeyIntercepted(KeyboardPressedEventArgs e)
        {
            if (_keyFilter?.Invoke(e) == false)
                return;

            var handler = KeyIntercepted;
            if (handler != null)
                Task.Run(() => handler(e));
        }
    }
}