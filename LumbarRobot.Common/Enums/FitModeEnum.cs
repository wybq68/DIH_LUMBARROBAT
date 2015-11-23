using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Enums;

namespace LumbarRobot.Common.Enums
{
    public enum FitModeEnum
    {
        /// <summary>
        /// 前屈后伸Fit
        /// </summary>
        [EnumItemName("前屈后伸Fit")]
        ProtrusiveOrBendFit = 1,

        /// <summary>
        /// 旋转Fit
        /// </summary>
        [EnumItemName("旋转Fit")]
        RotationFit = 2,

        /// <summary>
        /// 前屈后伸力量评测
        /// </summary>
        [EnumItemName("前屈后伸力量评测")]
        ProtrusiveOrBendStrengthEvaluation = 3,

        /// <summary>
        /// 旋转力量评测
        /// </summary>
        [EnumItemName("旋转力量评测")]
        RotationStrengthEvaluation = 4
    }
}
