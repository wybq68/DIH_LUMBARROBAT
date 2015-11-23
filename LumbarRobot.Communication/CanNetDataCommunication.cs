using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace LumbarRobot.Communication
{
    public class CanNetDataCommunication : IDataCommunication
    {
        const uint DEVICETYPE = 17;
        const uint STATUS_OK = 1;
        const uint STATUS_ERR = 0;

        const uint CMD_DESIP = 0;
        const uint CMD_DESPORT = 1;

        const uint CNDInd = 0;
        const uint DeviceInd = 0;

        private ByteBuffer buffer = new ByteBuffer(102400);

        private byte[] tempBuffer = new byte[1024];

        private bool isOpen = false;

        private string ip;
        private int port;

       
        

        Thread readThread = null;

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                isOpen = value;
            }
        }

        public CanNetDataCommunication(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        void IDataCommunication.Open()
        {
            if (isOpen)
            {
                try
                {
                    VCI_CloseDevice(DEVICETYPE, DeviceInd);
                    isOpen = false;
                }
                catch
                {
                    return;
                }
            }
            if (readThread != null && readThread.ThreadState == ThreadState.Running)
            {
                readThread.Abort();
            }
            //打开设备
            if (VCI_OpenDevice(DEVICETYPE, DeviceInd, 0) != STATUS_OK)
            {
                WriteLog("打开设备失败!");
                return;
            }

            //char[20] = new char[]{'1','9'};

            //VCI_SetReference(DEVICETYPE,DeviceInd,0,CMD_DESIP,

            //初始化设备
            VCI_INIT_CONFIG init_config = new VCI_INIT_CONFIG();
            init_config.AccCode = 0;
            init_config.AccMask = 0xffffffff;
            init_config.Filter = 0;
            init_config.Mode = 0;
            init_config.Timing0 = 0; //波特率设为500Kbps，定时器1为0，2为0x1c
            init_config.Timing1 = 0x1c;
            //init_config.Timing0 = 0; //波特率设为1Mbps，定时器1为0，2为0x14
            //init_config.Timing1 = 0x14;

            //init_config.Timing0 = 0x01; //波特率设为250Kbps，定时器1为1，2为0x1c
            //init_config.Timing1 = 0x1c;

            IntPtr intPtrIP;
            IntPtr intPtrPort;

            unsafe
            {
                byte[] byteIp = Encoding.ASCII.GetBytes(ip);
                fixed (void* pIP = &byteIp[0])
                {
                    intPtrIP = new IntPtr(pIP);
                }
                fixed (void* pPort = &port)
                {
                    intPtrPort = new IntPtr(pPort);
                }
            }

            VCI_SetReference(DEVICETYPE, DeviceInd, 0, CMD_DESIP, intPtrIP);
            VCI_SetReference(DEVICETYPE, DeviceInd, 0, CMD_DESPORT, intPtrPort);

            //if (VCI_InitCAN(DEVICETYPE, DeviceInd, CNDInd, ref init_config) != STATUS_OK)
            //{
            //    WriteLog("初始化设备失败!");
            //    return;
            //}

            //启动设备
            uint r = VCI_StartCAN(DEVICETYPE, DeviceInd, CNDInd);
            if (r != STATUS_OK)
            {
                WriteLog("启动失败!");
                return;
            }

            isOpen = true;

            //开始读数据
            readThread = new Thread(Recieve);
            readThread.Start();
        }

        void IDataCommunication.Close()
        {
            if (isOpen)
            {
                VCI_CloseDevice(DEVICETYPE, DeviceInd);
                isOpen = false;
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        void IDataCommunication.SendData(byte[] buffer)
        {
            VCI_CAN_OBJ frameinfo = new VCI_CAN_OBJ();
            frameinfo.Data = new byte[buffer.Length - 4];
            for (int i = 4; i < buffer.Length; i++)
            {
                frameinfo.Data[i - 4] = buffer[i];
            }
            frameinfo.DataLen = BitConverter.GetBytes(buffer.Length - 4)[0];
            frameinfo.RemoteFlag = 0;
            frameinfo.ExternFlag = 0;
            frameinfo.SendType = 0;
            frameinfo.ID = Convert.ToUInt32((buffer[3] << 8) + buffer[2]);
            frameinfo.Reserved = new byte[3];

            if (VCI_Transmit(DEVICETYPE, DeviceInd, CNDInd, ref frameinfo, 1) != STATUS_OK)
            {
                WriteLog("写入失败!");
                if (VCI_Transmit(DEVICETYPE, DeviceInd, CNDInd, ref frameinfo, 1) != STATUS_OK)//如果出错，再写一次
                {
                    WriteLog("二次写入失败!");
                }
            }
        }

        public event DataRecievedHandler DataReceived;

        public event LogEventHandler LogEvent;

        /// <summary>
        /// 接收数据
        /// </summary>
        private void Recieve()
        {
            uint len = 0;
            VCI_ERR_INFO errinfo = new VCI_ERR_INFO();
            VCI_CAN_OBJ[] frameinfos = new VCI_CAN_OBJ[1];
            while (isOpen)
            {
                try
                {
                    do
                    {
                        len = VCI_Receive(DEVICETYPE, DeviceInd, CNDInd, ref frameinfos[0], 1, 0);
                        if (len <= 0)
                        {
                            VCI_ReadErrInfo(DEVICETYPE, DeviceInd, CNDInd, ref errinfo);
                        }
                        else
                        {
                            foreach (var frameinfo in frameinfos)
                            {
                                if (frameinfo.DataLen > 0 && frameinfo.Data != null)
                                {
                                    if (frameinfo.Data.Length < frameinfo.DataLen)
                                    {
                                        WriteLog("数据格式不正确！");
                                    }

                                    buffer.Add(0xFA);//加数据包头
                                    //buffer.Add(0xe1);
                                    //buffer.Add(0x08);
                                    //buffer.Add(0);
                                    //buffer.Add(0);

                                    for (int i = 1; i < frameinfo.DataLen + 1; i++)
                                    {
                                        buffer.Add(frameinfo.Data[i - 1]);
                                    }
                                    DataRecievedArgs args = new DataRecievedArgs();

                                    args.Buffer = buffer;

                                    //System.Diagnostics.Debug.WriteLine(frameinfo.Data[7]);
                                    //args.Buffer1 = new byte[frameinfo.DataLen];
                                    ////args.Buffer[0] = 0xe1;
                                    ////args.Buffer[1] = 0x08;
                                    ////args.Buffer[2] = 0;
                                    ////args.Buffer[3] = 0;
                                    //for (int i = 0; i < frameinfo.DataLen; i++)
                                    //{
                                    //    args.Buffer1[i] = frameinfo.Data[i];
                                    //}

                                    if (DataReceived != null) DataReceived(args);
                                }
                            }
                        }
                    }
                    while (len <= 0);
                }
                catch { }
                //System.Diagnostics.Debug.WriteLine("end:" + DateTime.Now.Millisecond);
                //VCI_ClearBuffer(VCI_USBCAN1, DeviceInd, CNDInd);
                Thread.Sleep(1);
            }
        }

        private void WriteLog(string log)
        {
            if (LogEvent != null)
            {
                LogEvent(log);
            }
        }

        #region CAN API

        [DllImport("ControlCAN.dll", EntryPoint = "VCI_OpenDevice", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        static extern uint VCI_OpenDevice(uint DeviceType, uint DeviceInd, uint Reserved);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_CloseDevice(uint DeviceType, uint DeviceInd);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_InitCAN(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_INIT_CONFIG pInitConfig);

        [DllImport("ControlCAN.dll")]
        static extern uint VCI_ReadBoardInfo(uint DeviceType, uint DeviceInd, ref VCI_BOARD_INFO pInfo);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_ReadErrInfo(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_ERR_INFO pErrInfo);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_ReadCANStatus(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_STATUS pCANStatus);

        [DllImport("ControlCAN.dll")]
        static extern uint VCI_GetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, IntPtr pData);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_SetReference(uint DeviceType, uint DeviceInd, uint CANInd, uint RefType, IntPtr pData);

        [DllImport("ControlCAN.dll")]
        static extern ulong VCI_GetReceiveNum(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_ClearBuffer(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll")]
        static extern uint VCI_StartCAN(uint DeviceType, uint DeviceInd, uint CANInd);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_ResetCAN(uint DeviceType, uint DeviceInd, uint CANInd);

        [DllImport("ControlCAN.dll")]
        static extern uint VCI_Transmit(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pSend, uint Len);
        [DllImport("ControlCAN.dll")]
        static extern uint VCI_Receive(uint DeviceType, uint DeviceInd, uint CANInd, ref VCI_CAN_OBJ pReceive, uint Len, int WaitTime);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VCI_INIT_CONFIG
        {
            public uint AccCode;
            public uint AccMask;
            public uint Reserved;
            public byte Filter;
            public byte Timing0;
            public byte Timing1;
            public byte Mode;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VCI_ERR_INFO
        {
            public uint ErrCode;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] Passive_ErrData;
            public byte ArLost_ErrData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VCI_CAN_STATUS
        {
            public byte ErrInterrupt;
            public byte regMode;
            public byte regStatus;
            public byte regALCapture;
            public byte regECCapture;
            public byte regEWLimit;
            public byte regRECounter;
            public byte regTECounter;
            public uint Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VCI_CAN_OBJ
        {
            public uint ID;
            public uint TimeStamp;
            public byte TimeFlag;
            public byte SendType;
            public byte RemoteFlag;//是否是远程帧
            public byte ExternFlag;//是否是扩展帧
            public byte DataLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
            public byte[] Data;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.I1)]
            public byte[] Reserved;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        struct VCI_BOARD_INFO
        {
            public ushort hw_Version;
            public ushort fw_Version;
            public ushort dr_Version;
            public ushort in_Version;
            public ushort irq_Num;
            public byte can_Num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public char[] str_Serial_Num;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public char[] str_hw_Type;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] Reserved;
        }

        #endregion

    }
}
