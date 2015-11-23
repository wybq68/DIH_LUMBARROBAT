using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Communication;

namespace LumbarRobot.Common
{
    public class GlobalVar
    {
        private static MyLock dispatcherLock = new MyLock();

        public static MyLock DispatcherLock
        {
            get { return dispatcherLock; }
        }

        private static long startTimeTicks;

        public static long StartTimeTicks
        {
            get { return startTimeTicks; }
            set
            {
                startTimeTicks = value;
            }
        }

        static IDataCommunication dataCommunication = null;

        public static IDataCommunication DataCommunication
        {
            get { return dataCommunication; }
            set { dataCommunication = value; }
        }

        public static object MainWindows
        {
            get;
            set;
        }
    }
}
