using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace LumbarRobot.Common
{
    public class WindowsAPI
    {
        #region  API函数

        [StructLayout(LayoutKind.Sequential, Pack = 1)]

        internal struct TokPriv1Luid
        {
            public int Count;

            public long Luid;

            public int Attr;

        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary> 
        /// 得到当前活动的窗口 
        /// </summary> 
        /// <returns></returns> 
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();


        /// <summary>
        /// 获得句柄
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);

        [DllImport("kernel32.dll", ExactSpelling = true)]

        public static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        public static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]

        public static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]

        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,

        ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]

        public static extern bool ExitWindowsEx(int flg, int rea);

        public const int SE_PRIVILEGE_ENABLED = 0x00000002;

        public const int TOKEN_QUERY = 0x00000008;

        public const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;

        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        public const int EWX_LOGOFF = 0x00000000;

        public const int EWX_SHUTDOWN = 0x00000001;

        public const int EWX_REBOOT = 0x00000002;

        public const int EWX_FORCE = 0x00000004;

        public const int EWX_POWEROFF = 0x00000008;

        public const int EWX_FORCEIFHUNG = 0x00000010;

        public const int WM_SYSCOMMAND = 0x0112;

        public const int SC_CLOSE = 0xF060;



        public static void DoExitWin(int flg)
        {
            bool ok;

            TokPriv1Luid tp;

            IntPtr hproc = GetCurrentProcess();

            IntPtr htok = IntPtr.Zero;

            ok = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);

            tp.Count = 1;

            tp.Luid = 0;

            tp.Attr = SE_PRIVILEGE_ENABLED;

            ok = LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tp.Luid);

            ok = AdjustTokenPrivileges(htok, false, ref tp, 0, IntPtr.Zero, IntPtr.Zero);

            ok = ExitWindowsEx(flg, 0);
        }

        #endregion
    }
}
