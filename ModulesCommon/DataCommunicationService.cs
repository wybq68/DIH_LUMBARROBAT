using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;



namespace LumbarRobot.ModulesCommon
{
    /// <summary>
    /// UDP数据服务类，提供UDP数据服务
    /// </summary>
    public class DataCommunicationService
    {
        static byte[] buffer = new byte[1024];
        static Socket udpSock = null;
        bool isOpen = false;
        EndPoint remote = new IPEndPoint(IPAddress.Any, 0) as EndPoint;
        IDataCommunication dataCommunication = null;
       
        public DataCommunicationService(IDataCommunication dataCommunication)
        {
            this.dataCommunication = dataCommunication;
        }

        public void Start(IPEndPoint localEndPoint)
        {
            if(udpSock == null) udpSock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if(!udpSock.IsBound) udpSock.Bind(localEndPoint);
            dataCommunication.DataReceived += new DataRecievedHandler(dataCommunication_DataReceived);

            //启动新线程接收数据
            Thread thread = new Thread(Receive);
            isOpen = true;
            thread.Start();
        }

        void dataCommunication_DataReceived(DataRecievedArgs e)
        {
            if (udpSock != null && isOpen)
            {
                //lock (remote)
                //{
                var buffer = e.Buffer.GetBytes();
                if (buffer != null && buffer.Length > 0)
                {
                    udpSock.SendTo(e.Buffer.GetBytes(), remote);
                }
                //}
            }
        }

        public void Close()
        {
            isOpen = false;
            if (udpSock != null) udpSock.Close();
        }

        void Receive()
        {
            while (isOpen)
            {
                //lock (remote)
                //{
                    int num = udpSock.ReceiveFrom(buffer, ref remote);
                    if (num > 0)
                    {
                        byte[] data = new byte[num];
                        for(int i=0;i<num;i++)
                        {
                            data[i] = buffer[i];
                        }
                        dataCommunication.SendData(data);
                    }
                    Thread.Sleep(5);
                //}
            }
        }
    }

}
