using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class ControlCommandPackage
    {
        public CommandType CommandType { get; set; }
        public CommandParam CommandParam { get; set; }
    }

    public enum CommandType
    {
        /// <summary>
        /// 主动
        /// </summary>
        Free2 = 0x09,

        /// <summary>
        /// 停止
        /// </summary>
        Pause = 0x12,

        /// <summary>
        /// 急停
        /// </summary>
        QuickStop = 0x11,

        /// <summary>
        /// 电机归零
        /// </summary>
        SetZero = 0x13,

        /// <summary>
        /// 主动模式
        /// </summary>
        Free = 0x14,

        /// <summary>
        /// 被动模式
        /// </summary>
        Guided = 0x15,

        /// <summary>
        /// 助力模式
        /// </summary>
        Assist = 0x16,

        /// <summary>
        /// 等长A
        /// </summary>
        IsotonicA = 0x17,

        /// <summary>
        /// 等长B
        /// </summary>
        IsotonicB = 0x18,

        /// <summary>
        /// 主动配重块
        /// </summary>
        FreeCounterWeight = 0x19,

        /// <summary>
        /// 主动恒阻力
        /// </summary>
        FreeConstantResistance = 0x1A,

        /// <summary>
        /// 等长A
        /// </summary>
        IsotonicA2 = 0x1B,

        /// <summary>
        /// 离心
        /// </summary>
        Lixin = 0x1c,

        /// <summary>
        /// 等速
        /// </summary>
        Dengsu = 0x1d,

        /// <summary>
        /// 传感器清零
        /// </summary>
        SensorInit = 0x21,

        /// <summary>
        /// 故障清零
        /// </summary>
        ErrorReset = 0x22,

        /// <summary>
        /// 电机复位
        /// </summary>
        Reset = 0x23,

        /// <summary>
        /// 读取版本号
        /// </summary>
        Version = -0x11,

        /// <summary>
        /// 推杆向上
        /// </summary>
        PushRodUp = 0x01,

        /// <summary>
        /// 推杆向下
        /// </summary>
        PushRodDown = 0x02,

        /// <summary>
        /// 推杆停止
        /// </summary>
        PushRodStop = 0x03
    }

    public enum CommandParam
    {
        /// <summary>
        /// 屈伸
        /// </summary>
        BendStretch = 0x11,

        /// <summary>
        /// 旋转
        /// </summary>
        Rotation = 0x12
    }

    public enum Gameenum
    {
        
    }
}
