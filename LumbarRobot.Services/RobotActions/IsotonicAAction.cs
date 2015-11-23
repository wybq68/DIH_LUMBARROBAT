using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;

namespace LumbarRobot.Services.RobotActions
{
    public class IsotonicAAction : BaseRobotAction
    {
        public event SetForceHandler SetForce;

        public event EventHandler NextTime = null;

        public event EventHandler StartAction = null;

        public event EventHandler StopAction = null;

        private bool isBendReady = false;

        private bool isRotateReady = false;

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

        private int dataNum = 0;

        public int Type { get; set; }//0 屈伸，1 旋转

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            isStop = false;
            IsDrawLine = false;
            dataNum = 0;
            currentTimes = 0;
        }

        public override void Continue()
        {
            
        }

        public override void Stop()
        {
            if (!isStop)
            {
                base.Stop();
                if (StopAction != null) StopAction(null, null);
            }
        }

        public override void Control(Common.ResponsePackage responsePackage)
        {
            if (IsStop) return;

            //开始执行
            if (isReady)
            {

                if (responsePackage.Code == (int)ResponseCodes.BendStretchData)
                {
                    
                    if (Type == 0)
                    {
                        dataNum++;
                        if (dataNum >= Controller.TargetLine.Length)
                        {
                            Stop();
                            return;
                        }

                        if (SetForce != null) SetForce(responsePackage.BendStretchForce);

                        if (dataNum > 50 && (dataNum - 50) % ((Controller.TargetLine.Length - 50) / (controller.GroupNum * controller.Times)) == 0)
                        {
                            if (currentTimes < LumbarRobotController.RobotController.Times * LumbarRobotController.RobotController.GroupNum)
                            {
                                if (NextTime != null)
                                {
                                    currentTimes++;
                                    NextTime(null, null);
                                }
                            }
                        }
                    }
                    
                }
                else if (responsePackage.Code == (int)ResponseCodes.RotationData)
                {
                   
                    if (Type == 1)
                    {
                        dataNum++;
                        if (dataNum >= Controller.TargetLine.Length)
                        {
                            Stop();
                            return;
                        }

                        if (SetForce != null) SetForce(responsePackage.RotationForce);

                        if (dataNum > 50 && (dataNum - 50) % ((Controller.TargetLine.Length - 50) / (controller.GroupNum * controller.Times)) == 0)
                        {
                            if (NextTime != null)
                            {
                                currentTimes++;
                                NextTime(null, null);
                            }
                        }
                    }
                }

                

            }
            else
            {
                if (!isBendReady)
                {
                    if (responsePackage.Code == (int)ResponseCodes.BendStretchData)
                    {
                        int target = 0;
                        if (Type == 0)
                        {
                            target = LumbarRobotController.RobotController.Target;
                        }
                        //复位
                        if (ControlResetBendStretch(responsePackage, LumbarRobotController.RobotController.HomingAngle, LumbarRobotController.RobotController.PushRodAngle, target))
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
                            int target = 0;
                            if (Type == 1)
                            {
                                target = LumbarRobotController.RobotController.Target;
                            }
                            if (ControlResetRotate(responsePackage,target))
                            {
                                isRotateReady = true;
                                if (Type == 0)
                                {
                                    LumbarRobotController.RobotController.ControlCommand.BendStretchIsotonicA();
                                }
                                else
                                {
                                    LumbarRobotController.RobotController.ControlCommand.RotationIsotonicA();
                                }
                                if (StartAction != null) StartAction(null, null);
                            }
                        }
                    }
                }
            }

        }

    }
}
