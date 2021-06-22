// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TicTacToe.Enums;

namespace TicTacToe.Utils
{
    /// <summary>
    /// Class for intercepting low level Windows mouse hooks.
    /// </summary>
    public class MouseHook
    {
        private MouseHookHandler _proc;

        /// <summary>
        /// Low level mouse hook's ID
        /// </summary>
        private IntPtr _hookID = IntPtr.Zero;

        public MouseHook()
        {
            _proc = new MouseHookHandler(HookFunc);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                var moduleHandle = GetModuleHandle(curModule.ModuleName);
                _hookID = SetWindowsHookEx(WH_MOUSE_LL, _proc, moduleHandle, 0);
            }
        }

        /// <summary>
        /// Destructor. Unhook current hook
        /// </summary>
        ~MouseHook()
        {
            Uninstall();
        }

        public delegate void MouseHookEventHandler(MouseHookEventArgs e);

        /// <summary>
        /// Function to be called when defined even occurs
        /// </summary>
        /// <param name="mouseStruct">MSLLHOOKSTRUCT mouse structure</param>
        public delegate void MouseHookCallback(MSLLHOOKSTRUCT mouseStruct);

        /// <summary>
        /// Internal callback processing function
        /// </summary>
        private delegate IntPtr MouseHookHandler(int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam);

        public event MouseHookEventHandler MouseIntercepted;

        /// <summary>
        /// Remove low level mouse hook
        /// </summary>
        public void Uninstall()
        {
            if (_hookID == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(_hookID);
            _hookID = IntPtr.Zero;
        }

        private void OnKeyIntercepted(POINT position, MouseMessages messages)
        {
            var handler = MouseIntercepted;

            if (handler != null)
                Task.Run(() =>
                {
                    //Thread.Sleep(700);
                    //Console.WriteLine("[" + Console.CursorLeft + "\t" + Console.CursorTop + "]");
                    NativeMethods.ScreenToClient(NativeMethods.GetConsoleWindow(), ref position);
                    handler(new MouseHookEventArgs(new Point(position.x, position.y), messages));
                });
        }

        /// <summary>
        /// Callback function
        /// </summary>
        private IntPtr HookFunc(int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam)
        {
            // parse system messages
            if (nCode >= 0)
            {
                if (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    OnKeyIntercepted(lParam.pt, (MouseMessages)wParam);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, ref lParam);
        }

        #region WinAPI

        private const int WH_MOUSE_LL = 14;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion WinAPI
    }
}