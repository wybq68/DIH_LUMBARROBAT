using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LumbarRobot
{
    class LongTimeTaskAbc : ILongTimeTask
    {
        private Thread m_threadWorking;
        private WaitingDlg m_dlgWaiting;

        public void Start(WaitingDlg dlg)
        {
            m_dlgWaiting = dlg;
            m_threadWorking = new Thread(Working);
            m_threadWorking.Start();
        }

        private void Working()
        {
            Thread.Sleep(2000);
            m_dlgWaiting.TaskEnd(null);
        }
    }
}
