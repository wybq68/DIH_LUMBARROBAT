using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;
using LumbarRobot.Enums;

namespace LumbarRobot.Services.RobotActions
{
    public class BendStretchFreeAction : BaseRobotAction
    {
        public event EventHandler NextTime = null;

        public event EventHandler StopAction = null;

        public event EventHandler StartAction = null;

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

        private bool isBendReady = false;

        private bool isRotateReady = false;

        private int dataNum = 0;

        int target = 0;

        long readyTime = 0;

        int position = -1; //-1 最小、0 零位、1 最大

        //public ModeEnum ModeEnum { get; set; }

        //public ActionEnum ActionEnum { get; set; }

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            target = 0;
            position = -1;
            currentTimes = 0;
            isStop = false;
            readyTime = 0;
            dataNum = 0;
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
                //if (DateTime.Now.Ticks - readyTime < 30000000) return;//小于3秒不执行

                if (responsePackage.Code != (int)ResponseCodes.BendStretchData) return;

                #region 删除
                ////判断是否到目标位
                //if (Math.Abs(responsePackage.BendStretchAngle - target) < 2)
                //{
                //    if (position == 1)
                //    {
                //        position = -1;
                //        target = controller.MinAngle - 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                //    }
                //    else if (position == 0)
                //    {
                //        position = 1;
                //        target = controller.MaxAngle + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                //        currentTimes++;
                //        if (currentTimes >= controller.Times)
                //        {
                //            Stop();
                            
                //            return;
                //        }

                //        if (NextTime != null) NextTime(null, null);
                //    }
                //    else
                //    {
                //        position = 0;
                //        target = (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                //    }
                //}
                #endregion

                dataNum++;

                if (dataNum >= Controller.TargetLine.Length)
                {
                    currentTimes++;
                    Stop();
                    return;
                }
                else
                {

                    //判断是否到目标位
                    if (Math.Abs(Controller.TargetLine[dataNum] - target) < 2)
                    {
                        if (position == 1)
                        {
                            position = -1;
                            target = controller.MinAngle + controller.Target;
                        }
                        else if (position == 0)
                        {
                            position = 1;
                            target = controller.MaxAngle + controller.Target;
                            
                            //if (currentTimes >= controller.Times)
                            //{
                            //    Stop();
                            //    return;
                            //}
                            if (currentTimes < controller.Times * controller.GroupNum-1)
                            {
                                currentTimes++;
                                if (NextTime != null) NextTime(null, null);
                            }
                        }
                        else
                        {
                            position = 0;
                            target = controller.Target;
                        }
                    }
                }

                int targetAngle = GetMechineAngle(controller.Target);
                int minAngle = GetMechineAngle(controller.MinAngle);
                int maxAngle = GetMechineAngle(controller.MaxAngle);

                switch (Mode)
                {
                    case ModeEnum.Free:
                        //controller.ControlCommand.BendStretchFree(controller.Force, GetMechineAngle(controller.MinAngle == 0 ? -1 : controller.MinAngle),minAngle, maxAngle);
                        controller.ControlCommand.BendStretchFree(controller.Force, targetAngle, minAngle, maxAngle);
                        break;
                    case ModeEnum.Lixin:
                        if (Action == Enums.ActionEnum.Bend)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchLixin(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else if (Action == Enums.ActionEnum.Protrusive)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchLixin(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else
                        {
                            controller.ControlCommand.BendStretchLixin(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        break;
                    case ModeEnum.Dengsu:
                        if (Action == Enums.ActionEnum.Bend)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchDensu(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else if (Action == Enums.ActionEnum.Protrusive)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchDensu(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else
                        {
                            controller.ControlCommand.BendStretchDensu(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        break;
                    case ModeEnum.Sokoban:
                        if (Action == Enums.ActionEnum.Bend)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchFree(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else if (Action == Enums.ActionEnum.Protrusive)
                        {
                            //controller.ControlCommand.BendStretchFree(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchFree(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else
                        {
                            //if (responsePackage.BendStretchAngle < -1)
                            //{
                            //    controller.ControlCommand.BendStretchFree(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                            //}
                            //else if (responsePackage.BendStretchAngle > 1)
                            //{
                            //    controller.ControlCommand.BendStretchFree(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            //}
                            //else
                            //{
                                //controller.ControlCommand.BendStretchFree(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchFree(controller.Force, targetAngle, minAngle, maxAngle);
                            //}
                        }

                        break;
                    case ModeEnum.FreeConstantResistance:
                        controller.ControlCommand.BendStretchFreeConstantResistance(controller.Force, targetAngle, minAngle, maxAngle);
                        break;
                    case ModeEnum.FreeCounterWeight:
                        if (Action == Enums.ActionEnum.Bend)
                        {
                            //controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else if (Action == Enums.ActionEnum.Protrusive)
                        {
                            //controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                            controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, targetAngle, minAngle, maxAngle);
                        }
                        else
                        {
                            if (responsePackage.BendStretchAngle < -1)
                            {
                                //controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                                controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, targetAngle, minAngle, maxAngle);
                            }
                            else if (responsePackage.BendStretchAngle > 1)
                            {
                                //controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                                controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, targetAngle, minAngle, maxAngle);
                            }
                            else
                            {
                                //controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, 0, controller.MinAngle, controller.MaxAngle);
                                controller.ControlCommand.BendStretchFreeCounterWeight(controller.Force, targetAngle, minAngle, maxAngle);
                            }
                        }
                        break;
                 }

            }
            else
            {
                if (!isBendReady)
                {
                    if (responsePackage.Code == (int)ResponseCodes.BendStretchData)
                    {
                        //复位
                        if (ControlResetBendStretch(responsePackage, LumbarRobotController.RobotController.HomingAngle, LumbarRobotController.RobotController.PushRodAngle, LumbarRobotController.RobotController.Target))
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
                                target = controller.MaxAngle + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle) + controller.Target;
                                position = 1;
                            }
                        }
                    }
                }
            }
        }

        private int GetMechineAngle(int angle)
        {
            return (int)(angle + (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle));
        }
    }
}
