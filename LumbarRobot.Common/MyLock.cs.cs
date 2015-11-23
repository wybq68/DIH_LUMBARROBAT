using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LumbarRobot.Common
{
    public delegate void CallMethod();

    /// <summary>
    /// 锁，如果对象已被锁则调用的方法不执行，直接返回
    /// </summary>
    public class MyLock
    {
        object _lock;

        MyLock childLock = null;

        MyLock parentLock = null;

        bool isLock = false;

        public MyLock()
        {
            _lock = new object();
        }

        public MyLock(object lockObj)
        {
            _lock = lockObj;
        }

        public bool IsLock
        {
            get
            {
                if (childLock != null)//如果存在子锁则认为是锁状态
                {
                    return true;
                }
                return isLock;
            }

            set { isLock = value; }
        }


        public virtual void DoAction(CallMethod callMethod, bool isCreateNewThread = false)
        {
            lock (_lock)
            {
                if (IsLock)
                {
                    return;
                }
                else isLock = true;
            }

            try
            {
                if (isCreateNewThread)
                {
                    ThreadPool.QueueUserWorkItem((args) =>
                    {
                        try
                        {
                            callMethod();
                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                        finally
                        {
                            lock (_lock)
                            {
                                isLock = false;
                                if (parentLock != null)
                                {
                                    parentLock.childLock = null;
                                }
                            }
                        }
                    });
                }
                else
                {
                    callMethod();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (!isCreateNewThread)
                {
                    lock (_lock)
                    {
                        isLock = false;
                        if (parentLock != null)
                        {
                            parentLock.childLock = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建子锁
        /// </summary>
        /// <returns></returns>
        public MyLock CreateChildLock()
        {
            lock (_lock)
            {
                
                if (isLock)
                {
                    MyLock childLock = new MyLock(_lock);
                    childLock.parentLock = this;
                    this.childLock = childLock;
                    return childLock;
                }
                else
                {
                    return new NullMyLock();
                }
            }
        }

    }

    /// <summary>
    /// 空锁
    /// </summary>
    public class NullMyLock : MyLock
    {
        public override void DoAction(CallMethod callMethod, bool isCreateNewThread = false)
        {
            return;
        }
    }

}

