using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using LumbarRobot.Enums;
using LumbarRobot.Common.Enums;

namespace LumbarRobot.Services
{
    public interface IRobotAction
    {
        /// <summary>
        /// 开始
        /// </summary>
        void Start();

        /// <summary>
        /// 过程控制
        /// </summary>
        /// <param name="responsePackage"></param>
        void Control(ResponsePackage responsePackage);

        /// <summary>
        /// 暂停后继续执行
        /// </summary>
        void Continue();

        /// <summary>
        /// 停止
        /// </summary>
        void Stop();

        /// <summary>
        /// 暂停
        /// </summary>
        void Pause();

        /// <summary>
        /// 获取FIT结果
        /// </summary>
        /// <returns></returns>
        FitResult GetFitResult();

        /// <summary>
        /// 获取评测结果
        /// </summary>
        /// <returns></returns>
        EvaluteResult GetEvaluteResult();

        /// <summary>
        /// 控制器
        /// </summary>
        LumbarRobotController Controller { get; set; }

        /// <summary>
        /// 是否已经停止
        /// </summary>
        bool IsStop { get; }

        bool IsReady { get; }

        /// <summary>
        /// 当前动作正在执行的次数
        /// </summary>
        int CurrentTimes { get; }

        bool IsDrawLine { get; set; }

        /// <summary>
        /// 模式
        /// </summary>
        ModeEnum Mode { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        ActionEnum Action { get; set; }
    }

    /// <summary>
    /// Fit结果
    /// </summary>
    public class FitResult
    {
        public float? BendMinAngle { get; set; }

        public float? BendMaxAngle { get; set; }

        public float? RotateMinAngle { get; set; }

        public float? RotateMaxAngle { get; set; }

        public float? BendMin { get; set; }

        public float? BendMax { get; set; }

        public float? RotateMin { get; set; }

        public float? RotateMax { get; set; }

        public FitModeEnum FitMode { get; set; }
    }

    public class EvaluteResult
    {
        public EvaluateModeEnum EvaluteMode { get; set; }

        public EvaluateActionEnum EvaluteAction { get; set; }

        public float? MaxValue { get; set; }

        public float? LastValue { get; set; }

        public double[] TargetLine
        {
            get;
            set;
        }

        public float[] RealLine
        {
            get;
            set;
        }
    }
}


