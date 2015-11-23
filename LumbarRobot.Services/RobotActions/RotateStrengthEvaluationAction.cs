using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;

namespace LumbarRobot.Services.RobotActions
{
    public class RotateStrengthEvaluationAction : BaseRobotAction
    {
        public event EventHandler StartAction = null;

        public event SetForceHandler SetForce;

        private bool isBendReady = false;

        private bool isRotateReady = false;

        public float? BendMax = null;
        public float? BendMin = null;
        public float? RotateMax = null;
        public float? RotateMin = null;

        long readyTime = 0;

        public override bool IsReady
        {
            get
            {
                return isReady;
            }
        }

        private bool isReady
        {
            get
            {
                if (isBendReady && isRotateReady)
                {
                    return true;
                }
                return false;
            }
        }

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            isStop = false;
            RotateMax = 0;
            RotateMin = 0;
            BendMax = null;
            BendMin = null;
            readyTime = 0;
            IsDrawLine = false;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            if (!isStop)
            {
                base.Stop();
            }
        }

        public override void Control(Common.ResponsePackage responsePackage)
        {
            if (IsStop) return;

            //开始执行
            if (isReady)
            {
                //if (DateTime.Now.Ticks - readyTime < 30000000) return;//小于3秒不执行

                if (responsePackage.Code == (int)ResponseCodes.RotationData)
                {
                    var currentForce = responsePackage.RotationForce;
                    if (currentForce < RotateMin)
                    {
                        RotateMin = currentForce;
                    }
                    else if (currentForce > RotateMax)
                    {
                        RotateMax = currentForce;
                        RotateMin = currentForce;
                    }

                    if (SetForce != null) SetForce(responsePackage.RotationForce);
                }
            }
            else
            {
                if (!isBendReady)
                {
                    if (responsePackage.Code == (int)ResponseCodes.BendStretchData)
                    {
                        //复位
                        if (ControlResetBendStretch(responsePackage, LumbarRobotController.RobotController.HomingAngle, LumbarRobotController.RobotController.PushRodAngle))
                        {
                            isBendReady = true;
                        }
                    }
                }
                else
                {
                    if (!isRotateReady)
                    {
                        if (responsePackage.Code == (int)ResponseCodes.RotationData)
                        {
                            if (ControlResetRotate(responsePackage))
                            {
                                isRotateReady = true;
                                readyTime = DateTime.Now.Ticks;
                                LumbarRobotController.RobotController.ControlCommand.RotationIsotonicA();
                                if (StartAction != null) StartAction(null, null);
                            }
                        }
                    }
                }
            }
        }

        public override FitResult GetFitResult()
        {
            FitResult result = new FitResult();
            result.RotateMin = RotateMin;
            result.RotateMax = RotateMax;
            result.FitMode = Common.Enums.FitModeEnum.RotationStrengthEvaluation;

            return result;
        }
    }
}
