using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Protocol
{
    public class RequestCodes
    {
        /// <summary>
        /// 屈伸上行数据编组号
        /// </summary>
        public const int BendStretchParam = 0x13;

        /// <summary>
        /// 旋转上行数据编组号
        /// </summary>
        public const int RotationParam = 0x14;

        /// <summary>
        /// 上行控制指令编组号
        /// </summary>
        public const int ControlCommand = 0x61;

        /// <summary>
        /// 上行版本号指令编组号
        /// </summary>
        public const int VersionCommand = 0x80;
    }
}
