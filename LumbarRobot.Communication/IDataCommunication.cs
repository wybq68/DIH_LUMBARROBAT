using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Communication
{
    public class DataRecievedArgs : EventArgs
    {
        //byte[] buffer1;

        //public byte[] Buffer1
        //{
        //    get { return buffer1; }
        //    set { buffer1 = value; }
        //}

        ByteBuffer buffer;

        public ByteBuffer Buffer
        {
            get { return buffer; }
            set { buffer = value; }
        }
    }

    public delegate void DataRecievedHandler(DataRecievedArgs e);

    public delegate void LogEventHandler(string log);

    /// <summary>
    /// 通讯接口
    /// </summary>
    public interface IDataCommunication
    {
        /// <summary>
        /// 打开连接
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        void SendData(byte[] buffer);

        /// <summary>
        /// 数据接收事件
        /// </summary>
        event DataRecievedHandler DataReceived;

        /// <summary>
        /// 日志事件
        /// </summary>
        event LogEventHandler LogEvent;

        /// <summary>
        /// 连接是否打开
        /// </summary>
        bool IsOpen { get; set; }
    }
}
