using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Threading;

namespace LumbarRobot.Common
{
    public static class Parameter
    {
        public static string printname = ConfigurationManager.AppSettings["PrintName"];

        static Parameter()
        {
            printname = ConfigurationManager.AppSettings["PrintName"];
        }
    }
}
