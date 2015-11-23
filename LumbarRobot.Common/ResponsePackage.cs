using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class ResponsePackage
    {
        public int Code { get; set; }

        /// <summary>
        /// 屈伸角度
        /// </summary>
        public float BendStretchAngle { get; set; }

        /// <summary>
        /// 屈伸实时力
        /// </summary>
        public float BendStretchForce { get; set; }

        /// <summary>
        /// 屈伸角速度
        /// </summary>
        public float BendStretchSpeed { get; set; }

        /// <summary>
        /// 屈伸电压
        /// </summary>
        public float BendStretchVoltage { get; set; }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public float RotationAngle { get; set; }

        /// <summary>
        /// 旋转实时力
        /// </summary>
        public float RotationForce { get; set; }

        /// <summary>
        /// 旋转角速度
        /// </summary>
        public float RotationSpeed { get; set; }

        /// <summary>
        /// 旋转电压
        /// </summary>
        public float RotationVoltage { get; set; }

        /// <summary>
        /// 推杆角度
        /// </summary>
        public float PushRodAngle { get; set; }

        /// <summary>
        /// Homing结束后的推杆角度
        /// </summary>
        public float HomingAngle { get; set; }

        /// <summary>
        /// 限位开关
        /// </summary>
        public int LimitSwitch { get; set; }

        /// <summary>
        /// 版本号（新增功能升级）
        /// </summary>
        public int Version1 { get; set; }

        /// <summary>
        /// 版本号（现有功能升级）
        /// </summary>
        public int Version2 { get; set; }

        /// <summary>
        /// 版本号（bug修改升级）
        /// </summary>
        public int Version3 { get; set; }

        /// <summary>
        /// 报警
        /// </summary>
        public int Alarm { get; set; }
    }
}
