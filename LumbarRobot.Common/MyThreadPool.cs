using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LumbarRobot.Common
{
    public class MyThreadPool
    {
        int interval = 1;


        object _lock = new object();

        List<WaitCallbackItem> _Queue = new List<WaitCallbackItem>();

        public MyThreadPool()
        {
            //ThreadPool.QueueUserWorkItem(Process,null);
            Thread thread = new Thread(Process);
            thread.Start();
        }

        public MyThreadPool(int interval)
        {
            //ThreadPool.QueueUserWorkItem(Process,null);
            this.interval = interval;
            Thread thread = new Thread(Process);
            thread.Start();
        }

        public void QueueUserWorkItem(WaitCallback item, object state)
        {
            WaitCallbackItem callBackItem = new WaitCallbackItem();
            callBackItem.Item = item;
            callBackItem.State = state;
            lock (_lock)
            {
                _Queue.Add(callBackItem);
            }
        }

        private void Process()
        {
            while (true)
            {
                int count;
                WaitCallbackItem item = null;
                lock (_lock)
                {
                    count = _Queue.Count;
                    if (count > 0)
                    {
                        item = _Queue[0];
                        _Queue.RemoveAt(0);
                    }
                }
                if (item != null)
                {
                    item.Item(item.State);
                }
                if (count <= 1)
                {
                    Thread.Sleep(interval);
                }
            }
        }

        class WaitCallbackItem
        {
            public WaitCallback Item { get; set; }

            public object State { get; set; }
        }
    }
}
