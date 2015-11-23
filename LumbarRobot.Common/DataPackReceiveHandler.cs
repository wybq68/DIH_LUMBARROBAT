using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class DataPackReceivedEventArgs : EventArgs
    {
        ResponsePackage responsePackageData;

        public ResponsePackage ResponsePackageData
        {
            get { return responsePackageData; }
            set { responsePackageData = value; }
        }
    }

    public delegate void DataPackReceivedHandler(DataPackReceivedEventArgs args);
}
