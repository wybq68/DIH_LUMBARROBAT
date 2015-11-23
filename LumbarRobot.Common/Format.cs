using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace LumbarRobot.Common
{
    public class Format
    {
        #region 时间转换
        public static string SetTime(string date)
        {
            double h = 0, m = 0, s = 0;
            if (date == null || date == "")
                date = "0";
            double time = Convert.ToDouble(date);
            h = (int)Math.Floor(time / 3600);
            if (h > 0)
            {
                m = (int)Math.Floor((time - h * 3600) / 60);
                if (m > 0)
                {
                    s = (int)Math.Floor(time - h * 3600 - m * 60);
                }
                else
                {
                    s = (int)Math.Floor(time - h * 3600);
                }
            }
            else
            {
                m = (int)Math.Floor(time / 60);
                if (m > 0)
                {
                    s = (int)Math.Floor(time - 60 * m);
                }
                else
                {
                    s = (int)Math.Floor(time);
                }
            }
            return h + "时" + m + "分" + s + "秒"; ;
        }
        #endregion
    }

    public class ChartColor
    {
       public static SolidColorBrush[] brush = { new SolidColorBrush(Color.FromArgb(255, 18, 26, 42)), new SolidColorBrush(Color.FromArgb(255, 127, 184, 14)), new SolidColorBrush(Color.FromArgb(255, 253, 185, 51)) };
    }
}
