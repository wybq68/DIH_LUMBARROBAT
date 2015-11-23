using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;

namespace LumbarRobot.Services.RobotActions
{
    /// <summary>
    /// 屈伸被动
    /// </summary>
    public class BendStretchGuidedAction : BaseRobotAction
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

        long readyTime = 0;

        int target = 0;

        int position = -1; //-1 最小、0 零位、1 最大

        long startWaitTick = 0;

        float lastAngle = 0;

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            target = 0;
            position = -1;
            currentTimes = 0;
            isStop = false;
            readyTime = 0;
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
                if (DateTime.Now.Ticks - readyTime < 30000000) return;//小于3秒不执行

                if (responsePackage.Code != (int)ResponseCodes.BendStretchData) return;
                int actionTarget = target;
                //判断是否到目标位
                System.Diagnostics.Debug.WriteLine(position + ":" + lastAngle + "," + responsePackage.BendStretchAngle);
                if (position == 2)
                {
                    lock (this)
                    {

                        if (lastAngle < 0 && (responsePackage.BendStretchAngle - (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle)) >= 0)
                        {
                            position = 1;
                            target = controller.MaxAngle + 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                            actionTarget = target;
                            currentTimes++;

                            if (NextTime != null) NextTime(null, null);

                        }
                    }
                }
                else
                {
                    if (Math.Abs(responsePackage.BendStretchAngle - target) < 1.1)
                    {
                        if (position == 1)
                        {
                            position = -1;
                            target = controller.MinAngle - 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                            actionTarget = target;
                        }
                        else if (position == 0)
                        {
                            lock (this)
                            {
                                position = 1;
                                target = controller.MaxAngle + 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                                actionTarget = target;
                                currentTimes++;
                                if (currentTimes >= controller.Times * controller.GroupNum)
                                {
                                    Stop();

                                    return;
                                }

                                if (NextTime != null) NextTime(null, null);

                                if (currentTimes % controller.Times == 0)
                                {
                                    LumbarRobotController.RobotController.ControlCommand.PauseCmd();
                                    //等待10秒,做下一个分组
                                    startWaitTick = DateTime.Now.Ticks;
                                }
                            }
                        }
                        else
                        {
                            if ((currentTimes + 1) % controller.Times == 0 && (currentTimes + 1) > 0)
                            {
                                position = 0;
                                target = (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                            }
                            else
                            {
                                position = 2;
                                target = controller.MaxAngle + 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                            }
                            //actionTarget = target + controller.MaxAngle;
                            //target = controller.MaxAngle + 1 + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                        }
                    }
                }
                //System.Diagnostics.Debug.WriteLine(target);
                if (DateTime.Now.Ticks - startWaitTick > 100000000)
                {
                    controller.ControlCommand.BendStretchGuided(controller.Speed, actionTarget, controller.MinAngle, controller.MaxAngle);
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
                                target = controller.MaxAngle + (int)(LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
                                position = 1;
                            }
                        }
                    }
                }
            }

            lastAngle = responsePackage.BendStretchAngle - (LumbarRobotController.RobotController.HomingAngle - LumbarRobotController.RobotController.PushRodAngle);
        }
    }
}
