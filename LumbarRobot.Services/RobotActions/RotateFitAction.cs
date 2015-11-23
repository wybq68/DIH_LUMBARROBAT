using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;

namespace LumbarRobot.Services.RobotActions
{
    public class RotateFitAction : BaseRobotAction
    {
        public event EventHandler StartAction = null;

        private bool isBendReady = false;

        private bool isRotateReady = false;

        public float? BendMaxAngle = null;
        public float? BendMinAngle = null;
        public float? RotateMaxAngle = null;
        public float? RotateMinAngle = null;

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
            RotateMaxAngle = 0;
            RotateMinAngle = 0;
            BendMaxAngle = null;
            BendMinAngle = null;
            readyTime = 0;
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
                    if (responsePackage.RotationAngle < RotateMinAngle)
                    {
                        RotateMinAngle = responsePackage.RotationAngle;
                    }
                    else if (responsePackage.RotationAngle > RotateMaxAngle)
                    {
                        RotateMaxAngle = responsePackage.RotationAngle;
                    }
                }

                //LumbarRobotController.RobotController.ControlCommand.QuickStopCmd();
                LumbarRobotController.RobotController.ControlCommand.Free2(Common.CommandParam.Rotation);
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
            result.RotateMinAngle = RotateMinAngle;
            result.RotateMaxAngle = RotateMaxAngle;
            result.FitMode = Common.Enums.FitModeEnum.RotationFit;

            return result;
        }
    }
}
