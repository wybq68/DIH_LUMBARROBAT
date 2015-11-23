using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Enums;

namespace LumbarRobot.Common.Enums
{
    public enum EvaluateModeEnum
    {
        /// <summary>
        /// 力量
        /// </summary>
        Strength,

        /// <summary>
        /// 范围
        /// </summary>
        Range
    }

    public enum EvaluateActionEnum
    {
        /// <summary>
        /// 范围旋转(左) 1
        /// </summary>
        [EnumItemName("旋转(左)范围")]
        RotationRangeLeft = 1,
        /// <summary>
        /// 范围旋转(左) 2
        /// </summary>
        [EnumItemName("旋转(右)范围")]
        RotationRangeRight = 2,
        ///<summary>
        /// 范围前屈 3
        /// </summary>
        [EnumItemName("前屈范围")]
        RangeProtrusive = 3,
        /// <summary>
        /// 范围后伸 4
        /// </summary>
        [EnumItemName("后伸范围")]
        RangeBend = 4,

        /// <summary>
        /// 力量旋转(左) 5
        /// </summary>
        [EnumItemName("旋转(左)力量")]
        RotationStrengthLeft = 5,
        /// <summary>
        /// 力量旋转(右) 6
        /// </summary>
        [EnumItemName("旋转(右)力量")]
        RotationStrengthRigth = 6,
        /// <summary>
        /// 力量前屈 7
        /// </summary>
        [EnumItemName("前屈力量")]
        StrengthProtrusive = 7,
        /// <summary>
        /// 力量后伸 8
        /// </summary>
        [EnumItemName("后伸力量")]
        StrengthBend = 8
    }
}
