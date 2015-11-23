using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;
using LumbarRobot.Enums;

namespace LumbarRobot.Services.RobotActions
{
    public class IsotonicBAction : BaseRobotAction
    {
        public event SetForceHandler SetForce;

        public event EventHandler NextTime = null;

        public event EventHandler StartAction = null;

        public event EventHandler StopAction = null;

        private bool isBendReady = false;

        private bool isRotateReady = false;

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

        bool isControl = true;

        public int Type { get; set; }//0 屈伸，1 旋转

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            isStop = false;
            dataNum = 0;
            currentTimes = 0;
            IsDrawLine = false;
            readyTime = 0;
            isControl = true;
            
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

                if (Type == 0 && responsePackage.Code == (int)ResponseCodes.BendStretchData)
                {
                    //if (i != 0 && i % (GetNum(LumbarRobotController.RobotController.Speed)*2) == 0)
                    //{
                    //    currentTimes++;
                    //    if (currentTimes >= controller.Times)
                    //    {
                    //        Stop();
                            
                    //        return;
                    //    }
                    //    if (NextTime != null)
                    //    {
                    //        NextTime(null, null);
                    //    }
                    //}
                    //int f = GetForce(Action);

                    
                    if (dataNum >= Controller.TargetLine.Length)
                    {
                        Stop();
                        return;
                    }

                    if (dataNum > 50 && (dataNum - 50) % ((Controller.TargetLine.Length - 50) / (controller.GroupNum * controller.Times)) == 0)
                    {
                        if (NextTime != null)
                        {
                            currentTimes++;
                            NextTime(null, null);
                        }
                    }
                    int f = GetForce(dataNum);

                    dataNum++;

                    controller.ControlCommand.BendStretchIsotonicB(Math.Abs(f), f < 0 ? -1 : 1, 0, 0, isControl);

                    isControl = false;

                    if (SetForce != null) SetForce(f);

                }
                else if (Type == 1 && responsePackage.Code == (int)ResponseCodes.RotationData)
                {
                    //if (i != 0 && i % (GetNum(LumbarRobotController.RobotController.Speed) * 2) == 0)
                    //{
                    //    currentTimes++;
                    //    if (currentTimes >= controller.Times)
                    //    {
                    //        Stop();

                    //        return;
                    //    }
                    //    if (NextTime != null)
                    //    {
                    //        NextTime(null, null);
                    //    }
                    //}
                    //int f = GetForce(Action);

                    if (dataNum >= Controller.TargetLine.Length)
                    {
                        Stop();
                        return;
                    }

                    if (dataNum > 50 && (dataNum - 50) % ((Controller.TargetLine.Length - 50) / (controller.GroupNum * controller.Times)) == 0)
                    {
                        if (NextTime != null)
                        {
                            currentTimes++;
                            NextTime(null, null);
                        }
                    }
                    int f = GetForce(dataNum);

                    dataNum++;

                    controller.ControlCommand.RotationIsotonicB(Math.Abs(f), f < 0 ? -1 : 1, 0, 0, isControl);

                    isControl = false;

                    if (SetForce != null) SetForce(f);
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
                        if (ControlResetBendStretch(responsePackage, LumbarRobotController.RobotController.HomingAngle, LumbarRobotController.RobotController.PushRodAngle,target))
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
                                readyTime = DateTime.Now.Ticks;
                                if (StartAction != null) StartAction(null, null);
                            }
                        }
                    }
                }
            }

        }

        int dataNum = 0;

        private int GetForce(int i)
        {
            if (i < LumbarRobotController.RobotController.TargetLine.Count())
            {
                var result = (int)LumbarRobotController.RobotController.TargetLine[i];
                i++;
                return result;
            }
            else
            {
                return 0;
            }
        }

        #region 删除
        //int k = -1;

        //private int GetForce(ActionEnum action)
        //{
        //    int result = 0;
        //    if (action == ActionEnum.ProtrusiveOrBend || action == ActionEnum.Rotation)
        //    {
        //        int num = GetNum(LumbarRobotController.RobotController.Speed);
        //        result = (int)(Math.Sin((i / (float)num) * 3.1416) * controller.Force / 2);
        //        if ((i%num) == num / 2)
        //        {
        //            if (k > num / 2)
        //            {
        //                k = -1;
        //                i++;
        //            }
        //            else
        //            {
        //                k++;
        //            }
        //        }
        //        else
        //        {
        //        //System.Diagnostics.Debug.WriteLine(result);
        //            i++;     
        //        }
        //    }
        //    else if (action == ActionEnum.Protrusive)
        //    {
        //        int num = GetNum(LumbarRobotController.RobotController.Speed);
        //        result = (int)(Math.Sin((i / (float)num) * 3.1416) * controller.Force / 2);
        //        //System.Diagnostics.Debug.WriteLine(result);

        //        if (((i % num) == num / 2))
        //        {
        //            if (k > num / 2)
        //            {
        //                k = -1;
        //                i++;
        //            }
        //            else
        //            {
        //                k++;
        //            }
        //        }
        //        else
        //        {
        //            i++;
        //        }

        //        if (result < 0) result = 0;
        //    }
        //    else if (action == ActionEnum.Bend)
        //    {
        //        int num = GetNum(LumbarRobotController.RobotController.Speed);
        //        result = (int)(Math.Sin((i / (float)num) * 3.1416) * controller.Force / 2);

        //        if (((i % num) == num / 2))
        //        {
        //            if (k > num / 2)
        //            {
        //                k = -1;
        //                i++;
        //            }
        //            else
        //            {
        //                k++;
        //            }
        //        }
        //        else
        //        {
        //            i++;
        //        }

        //        if (result > 0) result = 0;
        //    }

        //    return result;
        //}

        //private int GetNum(int speed)
        //{
        //    return 4 * (110 - speed);
        //}
        #endregion
    }
}
