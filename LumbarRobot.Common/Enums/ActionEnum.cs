using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Enums
{
    public enum ActionEnum
    {
        /// <summary>
        /// 旋转
        /// </summary>
        [EnumItemName("旋转")]
        Rotation = 1,
        /// <summary>
        /// 前屈
        /// </summary>
        [EnumItemName("前屈")]
        Protrusive = 2,
        /// <summary>
        /// 后伸
        /// </summary>
        [EnumItemName("后伸")]
        Bend = 3,
        /// <summary>
        /// 前伸后曲
        /// </summary>
        [EnumItemName("前屈后伸")]
        ProtrusiveOrBend = 4
    }
}
