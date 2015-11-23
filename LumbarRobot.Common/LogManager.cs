using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class LogManager
    {
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="e"></param>
        public static void LogException(Exception e)
        {
            Exception ex = e;
            while (true)
            {
                if (ex.InnerException != null) ex = ex.InnerException;
                else break;
            }
            log4net.LogManager.GetLogger("ExceptionLog").Error(ex.GetType().ToString() + ":" + ex.Message + "\r\n" + ex.StackTrace);
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="error"></param>
        public static void LogException(string error)
        {
            log4net.LogManager.GetLogger("Exception").Error(error);
        }

        /// <summary>
        /// 记录事务日志
        /// </summary>
        /// <param name="log"></param>
        public static void Log(string log)
        {
            log4net.LogManager.GetLogger("Log").Info(log);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="messsage"></param>
        public static void Debug(string message)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Debug"] != null &&
                System.Configuration.ConfigurationManager.AppSettings["Debug"].ToLower() == "true")
            {
                log4net.LogManager.GetLogger("Debug").Info(message);
            }
        }

        public static void Alarm(string message)
        {
            log4net.LogManager.GetLogger("Alarm").Info(message);
        }
    }
}
