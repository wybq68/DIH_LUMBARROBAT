using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Enums;

namespace LumbarRobot.Common.Enums
{
    public enum DiagnoseTypeEnum
    {
        /// <summary>
        /// 旋转
        /// </summary>
        [EnumItemName("腰背痛")]
        Lumbago = 1,
        /// <summary>
        /// 前屈
        /// </summary>
        [EnumItemName("其它")]
        Else = 2,
    }
}
