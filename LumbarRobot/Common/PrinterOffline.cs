using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
using System.Runtime.InteropServices;
using System.Management;

namespace LumbarRobot.Common
{
     class PrinterOffline
    {
        public static PrintQueue GetPrintQueue(string PrinterName)
        {
            LocalPrintServer pr = new LocalPrintServer();
            pr.Refresh();
            EnumeratedPrintQueueTypes[] enumerationFlags = {EnumeratedPrintQueueTypes.Local,
                                                            EnumeratedPrintQueueTypes.Connections,
                                                           };
            foreach (PrintQueue pq in pr.GetPrintQueues(enumerationFlags))
            {
                if (pq.Name == PrinterName)
                {
                    return pq;
                }
            }
            return null;
        }

        public static string GetPrintState(string PrinterName)
        {
            string printStateText = null;
            PrintQueue printQueue = GetPrintQueue(PrinterName);
            if (printQueue == null)
                return null;
            if (printQueue.IsBusy)
                printStateText = "打印机正忙";
            else if (printQueue.IsDoorOpened)
                printStateText = "打印机门被打开";
            else if (printQueue.IsInError)
                printStateText = "打印机出错";
            else if (printQueue.IsInitializing)
                printStateText = "打印机正在初始化";
            else if (printQueue.IsIOActive)
                printStateText = "打印机正在与打印服务器交换数据";
            else if (printQueue.IsManualFeedRequired)
                printStateText = "打印机出错";
            else if (printQueue.IsNotAvailable)
                printStateText = "打印机状态信息不可用";
            else if (printQueue.IsTonerLow)
                printStateText = "打印机墨粉用完";
            else if (printQueue.IsOffline)
                printStateText = "打印机脱机";
            else if (printQueue.IsOutOfMemory)
                printStateText = "打印机无可用内存";
            else if (printQueue.IsOutputBinFull)
                printStateText = "打印机输出纸盒已满";
            else if (printQueue.IsPaperJammed)
                printStateText = "打印机卡纸";
            else if (printQueue.IsOutOfPaper)
                printStateText = "打印机中没有或已用完当前打印作业所需的纸张类型";
            else if (printQueue.QueueStatus == PrintQueueStatus.PaperProblem)
                printStateText = "打印机中的纸张导致未指定的错误情况";
            else if (printQueue.IsPaused)
                printStateText = "打印队列已暂停";
            else if (printQueue.IsPendingDeletion)
                printStateText = "打印队列正在删除打印作业";
            else if (printQueue.IsPrinting)
                printStateText = "设备正在打印";
            else if (printQueue.IsProcessing)
                printStateText = "设备正在执行某种工作，如果此设备是集打印机、传真机和扫描仪于一体的多功能设备，则不需要打印.";
            else if (printQueue.IsServerUnknown)
                printStateText = "打印机处于错误状态";
            else
                printStateText = "ok";
            //else if (printQueue.IsWarmingUp)
            //     printStateText = "打印机正在预热";
            return printStateText;
        }


        //============================================================================================================================================================================================================

        /// <summary>
        /// 获取打印机的当前状态
        /// </summary>
        /// <param name="PrinterDevice">打印机设备名称</param>
        /// <returns>打印机状态</returns>
        public static PrinterStatus GetPrinterStat(string PrinterDevice)
        {
            PrinterStatus ret = 0;
            string path = @"win32_printer.DeviceId='" + PrinterDevice + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            ret = (PrinterStatus)Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
            return ret;
        }

        public static string GetPrinterStatus(string PrinterName)
        {
            int intValue = GetPrinterStatusInt(PrinterName);
            string strRet = string.Empty;
            switch (intValue)
            {
                case 0:
                    strRet = "准备就绪（Ready）";
                    break;
                case 0x00000200:
                    strRet = "忙(Busy）";
                    break;
                case 0x00400000:
                    strRet = "被打开（Printer Door Open）";
                    break;
                case 0x00000002:
                    strRet = "错误(Printer Error）";
                    break;
                case 0x0008000:
                    strRet = "初始化(Initializing）";
                    break;
                case 0x00000100:
                    strRet = "正在输入,输出（I/O Active）";
                    break;
                case 0x00000020:
                    strRet = "手工送纸（Manual Feed）";
                    break;
                case 0x00040000:
                    strRet = "无墨粉（No Toner）";
                    break;
                case 0x00001000:
                    strRet = "不可用（Not Available）";
                    break;
                case 0x00000080:
                    strRet = "脱机（Off Line）";
                    break;
                case 0x00200000:
                    strRet = "内存溢出（Out of Memory）";
                    break;
                case 0x00000800:
                    strRet = "输出口已满（Output Bin Full）";
                    break;
                case 0x00080000:
                    strRet = "当前页无法打印（Page Punt）";
                    break;
                case 0x00000008:
                    strRet = "塞纸（Paper Jam）";
                    break;
                case 0x00000010:
                    strRet = "打印纸用完（Paper Out）";
                    break;
                case 0x00000040:
                    strRet = "纸张问题（Page Problem）";
                    break;
                case 0x00000001:
                    strRet = "暂停（Paused）";
                    break;
                case 0x00000004:
                    strRet = "正在删除（Pending Deletion）";
                    break;
                case 0x00000400:
                    strRet = "正在打印（Printing）";
                    break;
                case 0x00004000:
                    strRet = "正在处理（Processing）";
                    break;
                case 0x00020000:
                    strRet = "墨粉不足（Toner Low）";
                    break;
                case 0x00100000:
                    strRet = "需要用户干预（User Intervention）";
                    break;
                case 0x20000000:
                    strRet = "等待（Waiting）";
                    break;
                case 0x00010000:
                    strRet = "热机中（Warming Up）";
                    break;
                default:
                    strRet = "未知状态（Unknown Status）";
                    break;
            }
            return strRet;
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool OpenPrinter(string printer, out IntPtr handle, ref structPrinterDefaults pDefault);
        [DllImport("winspool.drv")]
        public static extern bool ClosePrinter(IntPtr handle);
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool GetPrinter(IntPtr handle, Int32 level, IntPtr buffer, Int32 size, out Int32 sizeNeeded);

        internal static int GetPrinterStatusInt(string PrinterName)
        {
            int intRet = 0x00000080;
            // int intRet = 0;
            IntPtr hPrinter;
            structPrinterDefaults defaults = new structPrinterDefaults();
            if (OpenPrinter(PrinterName, out hPrinter, ref defaults))
            {
                int cbNeeded = 0;
                bool bolRet = GetPrinter(hPrinter, 2, IntPtr.Zero, 0, out cbNeeded);
                if (cbNeeded > 0)
                {
                    IntPtr pAddr = Marshal.AllocHGlobal((int)cbNeeded);
                    bolRet = GetPrinter(hPrinter, 2, pAddr, cbNeeded, out cbNeeded);
                    if (bolRet)
                    {
                        PRINTER_INFO_2 Info2 = new PRINTER_INFO_2();

                        Info2 = (PRINTER_INFO_2)Marshal.PtrToStructure(pAddr, typeof(PRINTER_INFO_2));

                        intRet = System.Convert.ToInt32(Info2.Status);
                    }
                    Marshal.FreeHGlobal(pAddr);
                }
                ClosePrinter(hPrinter);
            }

            return intRet;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct structPrinterDefaults
        {
            [MarshalAs(UnmanagedType.LPTStr)]
            public String pDatatype;
            public IntPtr pDevMode;
            [MarshalAs(UnmanagedType.I4)]
            public int DesiredAccess;
        };

        //状态枚举
        [FlagsAttribute]
        internal enum PrinterStatus
        {
            PRINTER_STATUS_BUSY = 0x00000200,
            PRINTER_STATUS_DOOR_OPEN = 0x00400000,
            PRINTER_STATUS_ERROR = 0x00000002,
            PRINTER_STATUS_INITIALIZING = 0x00008000,
            PRINTER_STATUS_IO_ACTIVE = 0x00000100,
            PRINTER_STATUS_MANUAL_FEED = 0x00000020,
            PRINTER_STATUS_NO_TONER = 0x00040000,
            PRINTER_STATUS_NOT_AVAILABLE = 0x00001000,
            PRINTER_STATUS_OFFLINE = 0x00000080,
            PRINTER_STATUS_OUT_OF_MEMORY = 0x00200000,
            PRINTER_STATUS_OUTPUT_BIN_FULL = 0x00000800,
            PRINTER_STATUS_PAGE_PUNT = 0x00080000,
            PRINTER_STATUS_PAPER_JAM = 0x00000008,
            PRINTER_STATUS_PAPER_OUT = 0x00000010,
            PRINTER_STATUS_PAPER_PROBLEM = 0x00000040,
            PRINTER_STATUS_PAUSED = 0x00000001,
            PRINTER_STATUS_PENDING_DELETION = 0x00000004,
            PRINTER_STATUS_PRINTING = 0x00000400,
            PRINTER_STATUS_PROCESSING = 0x00004000,
            PRINTER_STATUS_TONER_LOW = 0x00020000,
            PRINTER_STATUS_USER_INTERVENTION = 0x00100000,
            PRINTER_STATUS_WAITING = 0x20000000,
            PRINTER_STATUS_WARMING_UP = 0x00010000
        }

        public struct PRINTER_INFO_2
        {
            public string pServerName;
            public string pPrinterName;
            public string pShareName;
            public string pPortName;
            public string pDriverName;
            public string pComment;
            public string pLocation;
            public IntPtr pDevMode;
            public string pSepFile;
            public string pPrintProcessor;
            public string pDatatype;
            public string pParameters;
            public IntPtr pSecurityDescriptor;
            public UInt32 Attributes;
            public UInt32 Priority;
            public UInt32 DefaultPriority;
            public UInt32 StartTime;
            public UInt32 UntilTime;
            public UInt32 Status;
            public UInt32 cJobs;
            public UInt32 AveragePPM;
        }

        /// <summary>
        /// 清除打印对列
        /// </summary>
        /// <param name="printJobID"></param>
        /// <returns></returns>
        public static bool CancelPrintJob(int printJobID)
        {
            // Variable declarations.            
            bool isActionPerformed = false;
            string searchQuery;
            String jobName;
            char[] splitArr;
            int prntJobID;
            ManagementObjectSearcher searchPrintJobs;
            ManagementObjectCollection prntJobCollection;
            try
            {
                // Query to get all the queued printer jobs.                
                searchQuery = "SELECT * FROM Win32_PrintJob";
                // Create an object using the above query.                
                searchPrintJobs = new ManagementObjectSearcher(searchQuery);
                // Fire the query to get the collection of the printer jobs.                
                prntJobCollection = searchPrintJobs.Get();
                // Look for the job you want to delete/cancel.                
                foreach (ManagementObject prntJob in prntJobCollection)
                {
                    jobName = prntJob.Properties["Name"].Value.ToString();
                    // Job name would be of the format [Printer name], [Job ID]                    
                    splitArr = new char[1];
                    splitArr[0] = Convert.ToChar(",");
                    // Get the job ID.                    
                    prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                    // If the Job Id equals the input job Id, then cancel the job.                    
                    //if (prntJobID == printJobID)
                    //{
                    // Performs a action similar to the cancel                        
                    // operation of windows print console                        
                    prntJob.Delete();
                    isActionPerformed = true;
                    //    break;
                    //}
                }
                return isActionPerformed;
            }
            catch (Exception sysException)
            {
                // Log the exception.                
                return false;
            }
        }
    }
}
