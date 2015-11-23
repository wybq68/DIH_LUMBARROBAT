using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    public class ControlParamPackage
    {
        /// <summary>
        /// 屈伸目标力
        /// </summary>
        public int BendStretchForceOrSpeed { get; set; }

        /// <summary>
        /// 目标角度
        /// </summary>
        public int BendStretchTarget { get; set; }

        /// <summary>
        /// 前屈角度限制（L 前屈 H后伸）
        /// </summary>
        public int BendAngleLimit { get; set; }

        /// <summary>
        /// 后伸角度限制（L 前屈 H后伸）
        /// </summary>
        public int StretchAngleLimit { get; set; }

        ///// <summary>
        ///// 屈伸角速度
        ///// </summary>
        //public int BendStretchSpeed { get; set; }

        /// <summary>
        /// 旋转目标力
        /// </summary>
        public int RotationaForceOrSpeed { get; set; }

        /// <summary>
        /// 旋转目标角度
        /// </summary>
        public int RotationTarget { get; set; }

        /// <summary>
        /// 旋转角度限制（L 左旋转 H 右旋转）
        /// </summary>
        public int LeftRotationAngleLimit { get; set; }

        /// <summary>
        /// 旋转角度限制（L 左旋转 H 右旋转）
        /// </summary>
        public int RightRotationAngleLimit { get; set; }

        ///// <summary>
        ///// 旋转角速度
        ///// </summary>
        //public int RotationSpeed { get; set; }
    }

    public enum ControlParamType
    {
        /// <summary>
        /// 屈伸
        /// </summary>
        BendStretch,

        /// <summary>
        /// 旋转
        /// </summary>
        Rotationa 
    }
}
