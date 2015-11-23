using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace LumbarRobot.Common
{
    public class Machine
    {
        #region 打开键盘
        /// <summary>
        /// 打开键盘
        /// </summary>
        public static void OpenTabtip()
        {
            try
            {
                Process.Start(Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + "//tabtip.exe");
            }
            catch { }
        }
        #endregion

        #region 关闭键盘
        /// <summary>
        /// 关闭键盘
        /// </summary>
        public static void ClosedTabtip()
        {
            IntPtr hwnd = WindowsAPI.FindWindow("IPTip_Main_Window", null);
            WindowsAPI.SendMessage(hwnd, WindowsAPI.WM_SYSCOMMAND, WindowsAPI.SC_CLOSE, 0);
        }
        #endregion

        #region 验证数字
        public static bool ISNumber(string p_strInput)
        {
            if (p_strInput == null)
            {
                return false;
            }
            return Regex.IsMatch(p_strInput, @"^\d+$", RegexOptions.Singleline);
        }
        #endregion
    }
}
