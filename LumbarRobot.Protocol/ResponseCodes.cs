using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Protocol
{
    /// <summary>
    /// 下行数据编组号
    /// </summary>
    public class ResponseCodes
    {
        /// <summary>
        /// 屈伸下行数据编组号
        /// </summary>
        public const int BendStretchData = 0x10;

        /// <summary>
        /// 旋转下行数据编组号
        /// </summary>
        public const int RotationData = 0x11;

        /// <summary>
        /// 其他数据
        /// </summary>
        public const int OtherData = 0x12;

        /// <summary>
        /// 报警
        /// </summary>
        public const int Alarm = 0x60;

        /// <summary>
        /// 版本
        /// </summary>
        public const int Version = 0x81;
    }
}
