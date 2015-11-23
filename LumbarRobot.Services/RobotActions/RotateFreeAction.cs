using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;
using LumbarRobot.Enums;

namespace LumbarRobot.Services.RobotActions
{
    public class RotateFreeAction : BaseRobotAction
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

        int position = -1; //-1 最小、0 零位、1 最大

        long readyTime = 0;

        //public ModeEnum ModeEnum { get; set; } 

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
                if (responsePackage.Code != (int)ResponseCodes.RotationData) return;

                //if (DateTime.Now.Ticks - readyTime < 30000000) return;//小于3秒不执行

                #region 删除
                ////判断是否到目标位
                //if (Math.Abs(responsePackage.RotationAngle - target) < 1.1)
                //{
                //    if (position == -1)
                //    {
                //        position = 1;
                //        target = controller.MaxAngle;
                //    }
                //    else if (position == 0)
                //    {
                //        position = -1;
                //        target = controller.MinAngle;
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
                //        target = 0;
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

                switch (Mode)
                {
                    case ModeEnum.Free:
                        controller.ControlCommand.RotationFree(controller.Force, controller.MinAngle == 0 ? -1 : controller.MinAngle, controller.MinAngle, controller.MaxAngle);
                        break;
                    case ModeEnum.Lixin:
                        controller.ControlCommand.RotationLixin(controller.Force, controller.MinAngle == 0 ? -1 : controller.MinAngle, controller.MinAngle, controller.MaxAngle);
                        break;
                    case ModeEnum.Dengsu:
                        controller.ControlCommand.RotationDengsu(controller.Force, controller.MinAngle == 0 ? -1 : controller.MinAngle, controller.MinAngle, controller.MaxAngle);
                        break;
                    case ModeEnum.Sokoban:
                        controller.ControlCommand.RotationFree(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                        break;
                    case ModeEnum.FreeConstantResistance:
                        controller.ControlCommand.RotationFreeConstantResistance(controller.Force, target, controller.MinAngle, controller.MaxAngle);
                        break;
                    case ModeEnum.FreeCounterWeight:
                        //if (responsePackage.RotationAngle > 1)
                        //{
                        //    //controller.ControlCommand.RotationCounterWeight(controller.Force, -1, controller.MinAngle, controller.MaxAngle);
                        //}
                        //else if (responsePackage.RotationAngle < -1)
                        //{
                            controller.ControlCommand.RotationCounterWeight(controller.Force, 1, controller.MinAngle, controller.MaxAngle);
                        //}
                        //else
                        //{
                        //    //controller.ControlCommand.RotationCounterWeight(controller.Force, 0, controller.MinAngle, controller.MaxAngle);
                        //}
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
                            if (ControlResetRotate(responsePackage, LumbarRobotController.RobotController.Target))
                            {
                                isRotateReady = true;
                                readyTime = DateTime.Now.Ticks;
                                if (StartAction != null) StartAction(null, null);
                                target = controller.MaxAngle + controller.Target;
                                position = 1;
                            }
                        }
                    }
                }
            }
        }
    }
}
