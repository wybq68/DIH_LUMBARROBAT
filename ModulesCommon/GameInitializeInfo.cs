using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ModulesCommon
{
    public class GameInitializeInfo
    {
        public GameInitializeInfo()
        {
            CustomFloatData = new List<float>();
            CustomStringData = new List<string>();
        }

        public float Position_X_Min { get; set; }
        public float Position_X_Max { get; set; }
        public float Position_Y_Min { get; set; }
        public float Position_Y_Max { get; set; }
        public float Position_Z_Min { get; set; }
        public float Position_Z_Max { get; set; }

        public float EulerAngles_X_Min { get; set; }
        public float EulerAngles_X_Max { get; set; }
        public float EulerAngles_Y_Min { get; set; }
        public float EulerAngles_Y_Max { get; set; }
        public float EulerAngles_Z_Min { get; set; }
        public float EulerAngles_Z_Max { get; set; }

        public List<float> CustomFloatData { get; set; }
        public List<string> CustomStringData { get; set; }

        public Difficulty Difficulty { get; set; }
    }

    /// <summary>
    /// 难度等级
    /// </summary>
    public enum Difficulty
    {
        Level1 = 1,
        Level2,
        Level3,
        Level4,
        Level5,
    }
}
