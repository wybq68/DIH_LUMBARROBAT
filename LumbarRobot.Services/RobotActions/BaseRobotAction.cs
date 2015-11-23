using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using LumbarRobot.Protocol;
using LumbarRobot.Enums;

namespace LumbarRobot.Services.RobotActions
{
    public abstract class BaseRobotAction : IRobotAction
    {
       
        /// <summary>
        /// 开始
        /// </summary>
        public virtual void Start() { }

        /// <summary>
        /// 过程控制
        /// </summary>
        /// <param name="responsePackage"></param>
        public virtual void Control(ResponsePackage responsePackage) { }

        /// <summary>
        /// 暂停后继续执行
        /// </summary>
        public virtual void Continue() { }

        /// <summary>
        /// 停止
        /// </summary>
        public virtual void Stop() 
        {
            if (!isStop)
            {
                isStop = true;
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause() { }

        public virtual FitResult GetFitResult()
        {
            return null;
        }

        public virtual EvaluteResult GetEvaluteResult()
        {
            return null;
        }

        protected LumbarRobotController controller;

        /// <summary>
        /// 控制器
        /// </summary>
        public LumbarRobotController Controller { get { return controller; } set { controller = value; } }

        protected bool isStop;

        /// <summary>
        /// 是否已经停止
        /// </summary>
        public bool IsStop { get { return isStop; } }

        private bool isDrawLine = true;

        public bool IsDrawLine
        {
            get { return isDrawLine; }
            set { isDrawLine = value; }
        }

        /// <summary>
        /// 模式
        /// </summary>
        public ModeEnum Mode { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public ActionEnum Action { get; set; }

        public virtual bool IsReady
        {
            get;
            set;
        }

        protected int currentTimes = 1;

        /// <summary>
        /// 当前动作正在执行的次数
        /// </summary>
        public int CurrentTimes { get { return currentTimes; } }

        protected bool ControlResetBendStretch(Common.ResponsePackage responsePackage,float homingAngle,float pushAngle,int target =0)
        {
            float targetAngle = 0;

            targetAngle = homingAngle - pushAngle + target;

            if (Math.Abs(responsePackage.BendStretchAngle - targetAngle) <= 1)
            {
                return true;
            }
            else
            {
                LumbarRobotController.RobotController.ControlCommand.BendStretchGuided(10, (int)targetAngle, -200, 200);
            }
            return false;
        }

        protected bool ControlResetRotate(Common.ResponsePackage responsePackage,int target=0)
        {
            if (Math.Abs(responsePackage.RotationAngle - target) <= 1.5)
            {
                LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                return true;
            }
            else
            {
                LumbarRobotController.RobotController.ControlCommand.RotationGuided(10, target, -200, 200);
            }
            return false;
        }
    } 
}
