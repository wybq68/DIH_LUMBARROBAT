using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Protocol;
using LumbarRobot.Common;
using LumbarRobot.Enums;
using LumbarRobot.Common.Enums;

namespace LumbarRobot.Services.RobotActions
{
    public class EvaluateAction : BaseRobotAction
    {
        public event SetForceHandler SetForce;

        public event EventHandler StartAction = null;

        public event EventHandler StopAction = null;

        private bool isBendReady = false;

        private bool isRotateReady = false;

        long readyTime = 0;

        float maxValue = 0;

        float lastValue = 0;

        int i = 0;

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


        public EvaluateModeEnum EvaluateMode { get; set; }

        public EvaluateActionEnum EvaluateActionValue { get; set; }

        public override void Start()
        {
            isBendReady = false;
            isRotateReady = false;
            isStop = false;
            dataNum = 0;
            currentTimes = 0;
            maxValue = 0;
           
            IsDrawLine = false;
            
            readyTime = 0;
            i = 0;

        }

        public override void Continue()
        {

            if (EvaluateActionValue == EvaluateActionEnum.RotationRangeLeft
                || EvaluateActionValue == EvaluateActionEnum.RotationRangeRight)
            {
                LumbarRobotController.RobotController.ControlCommand.Free2(Common.CommandParam.Rotation);
            }
            else if (EvaluateActionValue == EvaluateActionEnum.RangeBend
                || EvaluateActionValue == EvaluateActionEnum.RangeProtrusive)
            {
                LumbarRobotController.RobotController.ControlCommand.Free2(Common.CommandParam.BendStretch);
            }

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

                if ((EvaluateActionValue == EvaluateActionEnum.RangeBend || EvaluateActionValue == EvaluateActionEnum.RangeProtrusive
                    || EvaluateActionValue == EvaluateActionEnum.StrengthBend || EvaluateActionValue == EvaluateActionEnum.StrengthProtrusive )
                    && responsePackage.Code == (int)ResponseCodes.BendStretchData)
                {

                    if (dataNum >= Controller.TargetLine.Length)
                    {
                        Stop();
                        return;
                    }

                    dataNum++;

                    if (EvaluateMode == EvaluateModeEnum.Strength)
                    {

                        if (EvaluateActionValue == EvaluateActionEnum.StrengthProtrusive)
                        {
                            if (SetForce != null) SetForce(responsePackage.BendStretchForce);
                            if (responsePackage.BendStretchForce > 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                if (lastValue > Math.Abs(responsePackage.BendStretchForce) && i >100)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                if (maxValue < Math.Abs(responsePackage.BendStretchForce))
                                {
                                    maxValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                i++;
                            }
                            
                        }
                        else if (EvaluateActionValue == EvaluateActionEnum.StrengthBend)
                        {
                            if (SetForce != null) SetForce(responsePackage.BendStretchForce * -1);
                            if (responsePackage.BendStretchForce < 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                if (lastValue > Math.Abs(responsePackage.BendStretchForce) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                if (maxValue < Math.Abs(responsePackage.BendStretchForce))
                                {
                                    maxValue = Math.Abs(responsePackage.BendStretchForce);
                                }
                                i++;
                            }
                        }
                        
                    }
                    else
                    {
                        LumbarRobotController.RobotController.ControlCommand.Free2(Common.CommandParam.BendStretch);
                        if (EvaluateActionValue == EvaluateActionEnum.RangeProtrusive)
                        {
                            if (SetForce != null) SetForce(responsePackage.BendStretchAngle);
                            if (responsePackage.BendStretchAngle > 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                if (lastValue > Math.Abs(responsePackage.BendStretchAngle) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                if (maxValue < Math.Abs(responsePackage.BendStretchAngle))
                                {
                                    maxValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                i++;
                            }
                        }
                        else if (EvaluateActionValue == EvaluateActionEnum.RangeBend)
                        {
                            if (SetForce != null) SetForce(responsePackage.BendStretchAngle * -1);
                            if (responsePackage.BendStretchAngle < 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                if (lastValue > Math.Abs(responsePackage.BendStretchAngle) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                if (maxValue < Math.Abs(responsePackage.BendStretchAngle))
                                {
                                    maxValue = Math.Abs(responsePackage.BendStretchAngle);
                                }
                                i++;
                            }
                        }
                        
                    }

                }
                else if ((EvaluateActionValue == EvaluateActionEnum.RotationStrengthLeft || EvaluateActionValue == EvaluateActionEnum.RotationStrengthRigth
                    || EvaluateActionValue == EvaluateActionEnum.RotationRangeLeft || EvaluateActionValue == EvaluateActionEnum.RotationRangeRight)
                        && responsePackage.Code == (int)ResponseCodes.RotationData)
                {

                    if (dataNum >= Controller.TargetLine.Length)
                    {
                        Stop();
                        return;
                    }


                    dataNum++;


                    if (EvaluateMode == EvaluateModeEnum.Strength)
                    {

                        if (EvaluateActionValue == EvaluateActionEnum.RotationStrengthLeft)
                        {
                            if (SetForce != null) SetForce(responsePackage.RotationForce * -1);
                            if (responsePackage.RotationForce < 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationForce);
                                }
                                if (lastValue > Math.Abs(responsePackage.RotationForce) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationForce);
                                }
                                if (maxValue < Math.Abs(responsePackage.RotationForce))
                                {
                                    maxValue = Math.Abs(responsePackage.RotationForce);
                                }
                                i++;
                            }
                        }
                        else if (EvaluateActionValue == EvaluateActionEnum.RotationStrengthRigth)
                        {
                            if (SetForce != null) SetForce(responsePackage.RotationForce);
                            if (responsePackage.RotationForce > 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationForce);
                                }
                                if (lastValue > Math.Abs(responsePackage.RotationForce) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationForce);
                                }
                                if (maxValue < Math.Abs(responsePackage.RotationForce))
                                {
                                    maxValue = Math.Abs(responsePackage.RotationForce);
                                }
                                i++;
                            }
                        }
                    }
                    else
                    {
                        LumbarRobotController.RobotController.ControlCommand.Free2(Common.CommandParam.Rotation);
                        if (EvaluateActionValue == EvaluateActionEnum.RotationRangeLeft)
                        {
                            if (SetForce != null) SetForce(responsePackage.RotationAngle * -1);
                            if (responsePackage.RotationAngle < 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                if (lastValue > Math.Abs(responsePackage.RotationAngle) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                if (maxValue < Math.Abs(responsePackage.RotationAngle))
                                {
                                    maxValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                i++;
                            }
                        }
                        else if (EvaluateActionValue == EvaluateActionEnum.RotationRangeRight)
                        {
                            if (SetForce != null) SetForce(responsePackage.RotationAngle);
                            if (responsePackage.RotationAngle > 0)
                            {
                                if (i == 120)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                if (lastValue > Math.Abs(responsePackage.RotationAngle) && i > 100)
                                {
                                    lastValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                if (maxValue < Math.Abs(responsePackage.RotationAngle))
                                {
                                    maxValue = Math.Abs(responsePackage.RotationAngle);
                                }
                                i++;
                            }
                        }
                    }

                    
                }

                
            }
            #region 复位
            else
            {
                if (!isBendReady)
                {
                    if (responsePackage.Code == (int)ResponseCodes.BendStretchData)
                    {

                       var target = LumbarRobotController.RobotController.Target;

                       if (Action == ActionEnum.Rotation)
                       {
                           target = 0;
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
                            var target = LumbarRobotController.RobotController.Target;

                            if (Action != ActionEnum.Rotation)
                            {
                                target = 0;
                            }

                            if (ControlResetRotate(responsePackage, target))
                            {
                                isRotateReady = true;
                                readyTime = DateTime.Now.Ticks;
                                if (StartAction != null) StartAction(null, null);
                            }
                        }
                    }
                }
            }
            #endregion

        }

        int dataNum = 0;

        public override EvaluteResult GetEvaluteResult()
        {
            EvaluteResult result = new EvaluteResult();
            result.EvaluteAction = EvaluateActionValue;
            result.EvaluteMode = EvaluateMode;
            result.MaxValue = maxValue;
            result.LastValue = lastValue;

            return result;
        }
    }
}
