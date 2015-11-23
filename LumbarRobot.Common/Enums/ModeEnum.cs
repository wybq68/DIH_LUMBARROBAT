using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Enums
{
    public enum ModeEnum
    {
       
        /// <summary>
        /// 被动运动
        /// </summary>
        [EnumItemName("被动运动模式")]
        Guided = 1,
        /// <summary>
        /// 主动运动
        /// </summary>
        [EnumItemName("主动运动")]
        Free = 100,
        /// <summary>
        /// 助动运动
        /// </summary>
        [EnumItemName("助动运动")]
        Initiated = 101,

        /// <summary>
        /// 主动配重块
        /// </summary>
        [EnumItemName("向心、离心运动模式")]
        FreeCounterWeight = 2,

        /// <summary>
        /// 主动恒阻力
        /// </summary>
        [EnumItemName("等张运动模式")]
        FreeConstantResistance = 3,

        /// <summary>
        /// 等长
        /// </summary>
        [EnumItemName("等长运动模式")]
        IsotonicA = 4,

        /// <summary>
        /// 等长
        /// </summary>
        [EnumItemName("协调性训练模式")]
        IsotonicB = 5,

        /// <summary>
        /// 推箱子
        /// </summary>
        [EnumItemName("推箱子")]
        Sokoban = 6,

        /// <summary>
        /// 离心运动模式
        /// </summary>
        [EnumItemName("离心运动模式")]
        Lixin = 7,

        /// <summary>
        /// 等速运动模式
        /// </summary>
        [EnumItemName("等速运动模式")]
        Dengsu = 8,

        /// <summary>
        /// FIT
        /// </summary>
        [EnumItemName("FIT")]
        Fit = 10,

        /// <summary>
        /// 力量评测
        /// </summary>
        [EnumItemName("力量评测")]
        StrengthEvaluation = 11,

        /// <summary>
        /// 评测
        /// </summary>
        [EnumItemName("评测")]
        Evaluation = 12

    }
}
