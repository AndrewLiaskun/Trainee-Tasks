// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Utils
{
    /// <summary>
    /// Class for intercepting low level Windows mouse hooks.
    /// </summary>
    public class MouseHook
    {
        private MouseHookHandler proc;

        /// <summary>
        /// Low level mouse hook's ID
        /// </summary>
        private IntPtr hookID = IntPtr.Zero;

        public MouseHook()
        {
            proc = new MouseHookHandler(HookFunc);
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                var moduleHandle = GetModuleHandle(curModule.ModuleName);
                hookID = SetWindowsHookEx(WH_MOUSE_LL, proc, moduleHandle, 0);
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

        public event MouseHookCallback LeftButtonDown;

        public event MouseHookEventHandler MouseIntercepted;

        /// <summary>
        /// Remove low level mouse hook
        /// </summary>
        public void Uninstall()
        {
            if (hookID == IntPtr.Zero)
                return;

            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }

        private void OnKeyIntercepted(MouseHookEventArgs e)
        {
            if (MouseIntercepted != null)
                MouseIntercepted(e);
        }

        /// <summary>
        /// Callback function
        /// </summary>
        private IntPtr HookFunc(int nCode, IntPtr wParam, ref MSLLHOOKSTRUCT lParam)
        {
            // parse system messages
            if (nCode >= 0)
            {
                if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {

                    if (LeftButtonDown != null)
                    {

                        //LeftButtonDown((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
                        OnKeyIntercepted(new MouseHookEventArgs(lParam.pt));
                    }
                }
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        #region WinAPI

        private const int WH_MOUSE_LL = 14;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, MSLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public Point pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        #endregion WinAPI
    }
}