using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot
{
    public interface ILongTimeTask
    {
        void Start(WaitingDlg dlg);
    }
}
