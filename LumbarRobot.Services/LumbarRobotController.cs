using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Communication;
using LumbarRobot.Protocol;
using LumbarRobot.Common;
using LumbarRobot.Services.RobotActions;
using LumbarRobot.Enums;
using System.Threading;
using LumbarRobot.Common.Enums;

namespace LumbarRobot.Services
{
    public class LumbarRobotController
    {
        public static LumbarRobotController RobotController {get;set; }


        #region 变量
        IDataCommunication dataCommunication = null;

        ControlProtocol controlCommand = null;

        public ControlProtocol ControlCommand
        {
            get { return controlCommand; }
            set { controlCommand = value; }
        }

        public IRobotAction currentControl = null;

        public bool IsStop
        {
            get 
            {
                if (currentControl != null)
                {
                    return currentControl.IsStop;
                }
                return true;
            }

            set
            {
                if (value)
                {
                    LumbarRobotController.RobotController.controlCommand.PauseCmd();
                    if (currentControl != null)
                    {
                        currentControl.Stop();
                        currentControl = null;
                    }
                }
            }
        }

        public bool IsReady
        {
            get
            {
                if (currentControl != null)
                {
                    return currentControl.IsReady;
                }

                return false;
            }
        }

        private bool isPause = false;

        public bool IsPause 
        {
            get
            {
                return isPause;
            }
            set
            {
                if (value)
                {
                    controlCommand.PauseCmd();
                }
                isPause = value;
            }
        }

        System.Threading.Timer timer;

        long lastReceiveTime;

        private bool isConnected = false;

        public bool IsConnected
        {
            get { return isConnected; }
            set 
            {
                isConnected = value;
                if (value)
                {
                    if (Connected != null) Connected(null, null);
                }
                else
                {
                    if (Disconnected != null)
                    {
                        ThreadPool.QueueUserWorkItem((x) => { Disconnected(null, null); }, null);
                    }
                }
            }
        }

        public int CurrentTimes
        {
            get
            {
                if (currentControl != null)
                {
                    return currentControl.CurrentTimes;
                }

                return 0;
            }
        }

        public float HomingAngle { get; set; }

        private float pushRodAngle;

        public float PushRodAngle 
        {
            get
            {
                return pushRodAngle;
            }

            set
            {
                if (Math.Abs(value - pushRodAngle) >0.05)
                {
                    pushRodAngle = value;
                    if (PushRodChanged != null) PushRodChanged(null, null);
                }
            }
        }

        public float CurrentBendStretchAngle { get; set;}

        public float CurrentRotateAngle { get; set; }

        int limitSwitchValue = 0;

        public int LimitSwitchValue
        {
            get { return limitSwitchValue; }
            set 
            {
                if (value != limitSwitchValue)
                {
                    limitSwitchValue = value;
                    if (LimitSwitchChanged != null) LimitSwitchChanged(null, null);
                }
            }
        }

        public int[] LimitSwitch 
        {
            get
            {
                return GetLimitSwitch(LimitSwitchValue);
            }
        }

        public List<GroupRecord> GroupRecordList
        {
            get;
            set;
        }

        private int alarmCode = 0;

        public int AlarmCode
        {
            get { return alarmCode; }
            set 
            {
                if (alarmCode != value)
                {
                    alarmCode = value;
                    if (alarmCode == 0)
                    {
                        if (ReleaseAlarm != null)
                        {
                            ReleaseAlarm(null, null);
                        }
                    }
                    else
                    {
                        AlarmArgs args = new AlarmArgs();
                        args.AlarmCode = alarmCode;
                        if (Alarm != null)
                        {                  
                            Alarm(args);
                        }
                        LogManager.Alarm(args.ToString());
                    }
                }
            }
        }

        double[] targetLine;

        /// <summary>
        /// 目标线
        /// </summary>
        public double[] TargetLine
        {
            get { return targetLine; }
            set { targetLine = value; }
        }

        List<float> realLine;

        /// <summary>
        /// 实际训练线
        /// </summary>
        public List<float> RealLine
        {
            get { return realLine; }
            set { realLine = value; }
        }

        List<float> forceLine;

        /// <summary>
        /// 实时力曲线
        /// </summary>
        public List<float> ForceLine
        {
            get { return forceLine; }
            set { forceLine = value; }
        }


        long startTicks = 0;

        public long StartTicks
        {
            get { return startTicks; }
            set { startTicks = value; }
        }

        long endTicks = 0;

        public long EndTicks
        {
            get { return endTicks; }
            set { endTicks = value; }
        }

        float actionMinAngle = 0;

        float actionMaxAngle = 0;

        float groupActionMinAngle = 0;

        float groupActionMaxAngle = 0;

        float maxForce = 0;

        double sumForce = 0;

        int sumForceNum = 0;

        #endregion

        #region 设置参数
        /// <summary>
        /// 速度
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// 力量
        /// </summary>
        public int Force { get; set; }

        /// <summary>
        /// 次数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 最小角度
        /// </summary>
        public int MinAngle { get; set; }

        /// <summary>
        /// 最大角度
        /// </summary>
        public int MaxAngle { get; set; }

        /// <summary>
        /// 目标角度
        /// </summary>
        public int Target { get; set; }

        /// <summary>
        /// 分组数
        /// </summary>
        public int GroupNum { get; set; }
        #endregion

        #region 事件

        #region 参数
        public class PositionArgs : EventArgs
        {
            private ResponsePackage package;
            public ResponsePackage Package { get { return package; } }

            public PositionArgs(ResponsePackage package)
            {
                this.package = package;
            }
        }

        public delegate void MoveHandler(PositionArgs args);

        public class TrainEndEventArgs : EventArgs
        {

        }

        public delegate void TrainEndHandler(TrainEndEventArgs e);

        public class AlarmArgs : EventArgs
        {
            public int AlarmCode { get; set; }

            public override string ToString()
            {
                StringBuilder str = new StringBuilder();

                if ((AlarmCode & 0x01) == 0x01) //1
                {
                    str.Append("'屈伸电机故障'");
                }
                if ((AlarmCode & 0x02) == 0x02) //2
                {
                    str.Append("'旋转电机故障'");
                }
                if ((AlarmCode & 0x04) == 0x04) //3
                {
                    str.Append("'电机急停动作'");
                }
                if ((AlarmCode & 0x08) == 0x08)//4
                {
                    str.Append("'屈伸电机DA输出故障'");
                }
                if ((AlarmCode & 0x010) == 0x010)//5
                {
                    str.Append("'屈伸电机DA输出故障'");
                }
                if ((AlarmCode & 0x020) == 0x020)//6
                {
                    str.Append("'屈伸电机使能失效'");
                }
                if((AlarmCode & 0x040) == 0x040)//7
                {
                    str.Append("'旋转电机使能失效'");
                }
                if ((AlarmCode & 0x080) == 0x080)//8
                {
                    str.Append("'刹车动作'");
                }
                if ((AlarmCode & 0x1000) == 0x1000)//13
                {
                    str.Append("'前屈传感器过载报警'");
                }
                if ((AlarmCode & 0x2000) == 0x2000)//14
                {
                    str.Append("'后伸传感器过载报警'");
                }
                if ((AlarmCode & 0x4000) == 0x4000)//15
                {
                    str.Append("'旋转传感器过载报警'");
                }
                if ((AlarmCode & 0x10000) == 0x10000)//17
                {
                    str.Append("'屈伸电机过流故障'");
                }
                if ((AlarmCode & 0x20000) == 0x20000)//18
                {
                    str.Append("'旋转电机过流故障'");
                }
                if ((AlarmCode & 0x40000) == 0x40000)//19
                {
                    str.Append("'屈伸电机失速告警'");
                }
                if ((AlarmCode & 0x80000) == 0x80000)//20
                {
                    str.Append("'旋转电机失速告警'");
                }
                if ((AlarmCode & 0x100000) == 0x100000)//21
                {
                    str.Append("'屈伸电机失速故障'");
                }
                if ((AlarmCode & 0x200000) == 0x200000)//22
                {
                    str.Append("'旋转电机失速故障'");
                }
                //else if ((AlarmCode & 0x400000) == 0x400000)//23
                //{
                //    str.Append("'屈伸电机运动角度超限告警(主动)'");
                //}
                //else if ((AlarmCode & 0x800000) == 0x800000)//24
                //{
                //    str.Append("'旋转电机运动角度超限告警(主动)'");
                //}
                if ((AlarmCode & 0x1000000) == 0x1000000)//25
                {
                    str.Append("'屈伸电机角度超限故障(被动)'");
                }
                if ((AlarmCode & 0x2000000) == 0x2000000)//26
                {
                    str.Append("'旋转电机角度超限故障(被动)'");
                }
                if ((AlarmCode & 0x4000000) == 0x4000000)//27
                {
                    str.Append("'推杆角度超限告警'");
                }
                if ((AlarmCode & 0x8000000) == 0x8000000)//28
                {
                    str.Append("'推杆过载故障'");
                }
                

                return str.ToString();
            }
        }

        public delegate void AlarmHandler(AlarmArgs e);
        #endregion

        /// <summary>
        /// 移动事件
        /// </summary>
        public event MoveHandler Move;

        /// <summary>
        /// 训练结束事件
        /// </summary>
        public event TrainEndHandler TrainingEnd;

        /// <summary>
        /// 训练到达事件
        /// </summary>
        public event EventHandler TrainingArrived;

        public event EventHandler NextTime;

        public event EventHandler StopAction;

        public event EventHandler LimitSwitchChanged;

        public event SetForceHandler SetForce;

        public event EventHandler StartAction;

        public event AlarmHandler Alarm;

        public event EventHandler ReleaseAlarm;

        public event EventHandler PushRodChanged;

        /// <summary>
        /// 连接事件
        /// </summary>
        public event EventHandler Connected;

        /// <summary>
        /// 断开连接事件
        /// </summary>
        public event EventHandler Disconnected;


        #endregion

        #region RobotAction

        private BendStretchFreeAction bendStretchFreeAction = null;

        private BendStretchGuidedAction bendStretchGuidedAction = null;

        private BendStretchAssistedAction bendStretchAssistedAction = null;

        private RotateFreeAction rotateFreeAction = null;

        private RotateGuidedAction rotateGuidedAction = null;

        private RotateAssistedAction rotateAssistedAction = null;

        private BendStretchFitAction bendStretchFitAction = null;

        private BendStrechStrengthEvaluationAction bendStrechStrengthEvaluationAction = null;

        private RotateStrengthEvaluationAction rotateStrengthEvaluationAction = null;

        private RotateFitAction rotateFitAction = null;

        private IsotonicBAction isotonicBAction = null;

        private IsotonicAAction isotonicAAction = null;

        private EvaluateAction evaluateAction = null;

        #endregion

        #region 功能函数

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            actionMaxAngle = 0;
            actionMinAngle = 0;
            groupActionMinAngle = float.MaxValue;
            groupActionMaxAngle = float.MinValue;
            sumForce = 0;
            sumForceNum = 0;
            maxForce = 0;

            realLine = new List<float>();
            forceLine = new List<float>();

            controlCommand.PauseCmd();
            
        }

        /// <summary>
        /// 获取FIT结果
        /// </summary>
        /// <returns></returns>
        public FitResult GetFitResult()
        {
            if (currentControl != null)
            {
                return currentControl.GetFitResult();
            }

            return null;
        }

        public EvaluteResult GetEvaluteResult()
        {
            if (currentControl != null)
            {
                var result =  currentControl.GetEvaluteResult();

                result.TargetLine = targetLine;

                if (result.EvaluteMode == EvaluateModeEnum.Range)
                {
                    if (RealLine != null)
                    {
                        result.RealLine = RealLine.ToArray();
                    }
                }
                else
                {
                    if (ForceLine != null)
                    {
                        result.RealLine = ForceLine.ToArray();
                        if (result.EvaluteAction == EvaluateActionEnum.StrengthProtrusive
                            || result.EvaluteAction == EvaluateActionEnum.RotationStrengthLeft)
                        {
                            for (int i = 0; i < result.RealLine.Count(); i++)
                            {
                                result.RealLine[i] = result.RealLine[i] * -1;
                            }
                        }
                    }
                }

                return result;
            }

            return null;
        }

        public bool IsDrawLine()
        {
            if (currentControl != null && currentControl.IsDrawLine)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="serialPortManager"></param>
        /// <param name="readCom"></param>
        /// <param name="writeCom"></param>
        public void Init(IDataCommunication dataCommunication)
        {
            this.dataCommunication = dataCommunication;
            //dataCommunication.LogEvent += new LogEventHandler(dataCommunication_LogEvent);
            dataCommunication.Open();

            controlCommand = new ControlProtocol(dataCommunication);

            lastReceiveTime = DateTime.Now.Ticks;
            controlCommand.DataPackRecieved += new DataPackReceivedHandler(controlCommand_DataPackRecieved);

            controlCommand.PauseCmd();

            //创建定时器
            timer = new System.Threading.Timer(TimeCallBackFunc, null, 1000, 1000);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            timer.Dispose();
            timer = null;
            this.dataCommunication.Close();
        }

        public void Continue()
        {
            IsPause = false;
            if (currentControl != null)
            {
                currentControl.Continue();
            }
        }
        #endregion

        #region 控制

        void controlCommand_DataPackRecieved(DataPackReceivedEventArgs args)
        {
            if (args.ResponsePackageData != null)
            {
                lastReceiveTime = DateTime.Now.Ticks;
                IsConnected = true;

                if (args.ResponsePackageData.Code == ResponseCodes.BendStretchData)
                {
                    CurrentBendStretchAngle = args.ResponsePackageData.BendStretchAngle;
                    if (Move != null && currentControl != null)
                    {
                        //if (currentControl.IsReady)
                        //{
                            
                            if (!LumbarRobotController.RobotController.IsStop
                                && !LumbarRobotController.RobotController.IsPause
                                && RealLine != null)
                            {
                                Move(new PositionArgs(args.ResponsePackageData));

                                if (LumbarRobotController.RobotController.currentControl != null
                                    && (LumbarRobotController.RobotController.currentControl.Action == ActionEnum.Bend
                                    || LumbarRobotController.RobotController.currentControl.Action == ActionEnum.Protrusive
                                    || LumbarRobotController.RobotController.currentControl.Action == ActionEnum.ProtrusiveOrBend))
                                {
                                    realLine.Add(args.ResponsePackageData.BendStretchAngle);

                                    if (args.ResponsePackageData.BendStretchAngle < actionMinAngle)
                                    {
                                        actionMinAngle = args.ResponsePackageData.BendStretchAngle;
                                    }
                                    else if (args.ResponsePackageData.BendStretchAngle > actionMaxAngle)
                                    {
                                        actionMaxAngle = args.ResponsePackageData.BendStretchAngle;
                                    }

                                    if (args.ResponsePackageData.BendStretchAngle < groupActionMinAngle)
                                    {
                                        groupActionMinAngle = args.ResponsePackageData.BendStretchAngle;
                                    }
                                    else if (args.ResponsePackageData.BendStretchAngle > groupActionMaxAngle)
                                    {
                                        groupActionMaxAngle = args.ResponsePackageData.BendStretchAngle;
                                    }

                                    if (args.ResponsePackageData.BendStretchForce > maxForce)
                                    {
                                        maxForce = args.ResponsePackageData.BendStretchForce;
                                    }

                                    sumForce += args.ResponsePackageData.BendStretchForce;
                                    sumForceNum++;

                                    forceLine.Add(args.ResponsePackageData.BendStretchForce * -1);
                                }
                            }
                        //}
                    }
                }
                else if (args.ResponsePackageData.Code == ResponseCodes.RotationData)
                {
                    CurrentRotateAngle = args.ResponsePackageData.RotationAngle;
                    if (Move != null && currentControl != null) 
                    {
                        //if (currentControl.IsReady)
                        //{
                            
                            

                            if (LumbarRobotController.RobotController.currentControl != null
                                        && LumbarRobotController.RobotController.currentControl.Action == ActionEnum.Rotation)
                            {

                                if (!LumbarRobotController.RobotController.IsStop
                                    && !LumbarRobotController.RobotController.IsPause
                                    && RealLine != null)
                                {

                                    Move(new PositionArgs(args.ResponsePackageData));

                                    realLine.Add(args.ResponsePackageData.RotationAngle);

                                    if (args.ResponsePackageData.RotationAngle < actionMinAngle)
                                    {
                                        actionMinAngle = args.ResponsePackageData.RotationAngle;
                                    }
                                    else if (args.ResponsePackageData.RotationAngle > actionMaxAngle)
                                    {
                                        actionMaxAngle = args.ResponsePackageData.RotationAngle;
                                    }

                                    if (args.ResponsePackageData.RotationAngle < groupActionMinAngle)
                                    {
                                        groupActionMinAngle = args.ResponsePackageData.RotationAngle;
                                    }
                                    else if (args.ResponsePackageData.RotationAngle > groupActionMaxAngle)
                                    {
                                        groupActionMaxAngle = args.ResponsePackageData.RotationAngle;
                                    }

                                    if (args.ResponsePackageData.RotationForce > maxForce)
                                    {
                                        maxForce = args.ResponsePackageData.RotationForce;
                                    }

                                    sumForce += args.ResponsePackageData.RotationForce;
                                    sumForceNum++;

                                    forceLine.Add(args.ResponsePackageData.RotationForce);
                                }
                            }

                        //}
                    }
                }
                else if (args.ResponsePackageData.Code == ResponseCodes.OtherData)
                {
                    HomingAngle = args.ResponsePackageData.HomingAngle;
                    PushRodAngle = args.ResponsePackageData.PushRodAngle;
                    LimitSwitchValue = args.ResponsePackageData.LimitSwitch;

                }
                else if (args.ResponsePackageData.Code == ResponseCodes.Alarm)
                {
                    //报警
                    AlarmCode = args.ResponsePackageData.Alarm;

                }
                else if (args.ResponsePackageData.Code == ResponseCodes.Version)
                {
                }

                if (currentControl != null && !IsStop && !IsPause)
                {
                    currentControl.Control(args.ResponsePackageData);
                }
            }
        }

        private int[] GetLimitSwitch(int limitSwitch)
        {
            List<int> result = new List<int>();
            for (int i = 1; i <= 24; i++)
            {
                if (((limitSwitch << i) & 0x1000000) != 0x1000000)
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }
        #endregion

        #region DoAction

        public void DoEvaluateAction(EvaluateActionEnum evaluateActionEnum, EvaluateModeEnum evaluateMode)
        {
            Reset();
            startTicks = DateTime.Now.Ticks;

            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            Thread.Sleep(10);
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            Thread.Sleep(10);

            if (evaluateAction == null)
            {
                evaluateAction = new EvaluateAction();
                evaluateAction.SetForce += new SetForceHandler(action_SetForce);
                evaluateAction.StartAction += new EventHandler(action_StartAction);
                evaluateAction.StopAction += new EventHandler(action_StopAction);
            }

            evaluateAction.Mode = ModeEnum.Evaluation;
            if (evaluateActionEnum == EvaluateActionEnum.RotationRangeLeft || evaluateActionEnum == EvaluateActionEnum.RotationRangeRight
                || evaluateActionEnum == EvaluateActionEnum.RotationStrengthLeft || evaluateActionEnum == EvaluateActionEnum.RotationStrengthRigth)
            {

                evaluateAction.Action = ActionEnum.Rotation;
            }
            else
            {
                evaluateAction.Action = ActionEnum.ProtrusiveOrBend;
            }
            currentControl = evaluateAction;

            if (currentControl != null)
            {
                evaluateAction.EvaluateActionValue = evaluateActionEnum;
                evaluateAction.EvaluateMode = evaluateMode;
                evaluateAction.Controller = this;
                evaluateAction.Start();
            }
        }


        /// <summary>
        /// 执行动作
        /// </summary>
        public void DoAction(ModeEnum mode, ActionEnum action)
        {
            Reset();
            startTicks = DateTime.Now.Ticks;

            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            Thread.Sleep(10);
            LumbarRobotController.RobotController.ControlCommand.PauseCmd();
            Thread.Sleep(10);
            if (mode == ModeEnum.Fit)
            {
                if (action == ActionEnum.Bend || action == ActionEnum.Protrusive || action == ActionEnum.ProtrusiveOrBend)
                {
                    if (bendStretchFitAction == null)
                    {
                        bendStretchFitAction = new BendStretchFitAction();
                        bendStretchFitAction.StartAction += new EventHandler(action_StartAction);
                    }
                    bendStretchFitAction.Mode = mode;
                    bendStretchFitAction.Action = action;
                    currentControl = bendStretchFitAction;
                }
                else if(action == ActionEnum.Rotation)
                {
                    if (rotateFitAction == null)
                    {
                        rotateFitAction = new RotateFitAction();
                        rotateFitAction.StartAction += new EventHandler(action_StartAction);
                    }
                    rotateFitAction.Mode = mode;
                    rotateFitAction.Action = action;
                    currentControl = rotateFitAction;
                }
            }
            else if (mode == ModeEnum.StrengthEvaluation)
            {
                if (action == ActionEnum.Bend || action == ActionEnum.Protrusive || action == ActionEnum.ProtrusiveOrBend)
                {
                    if (bendStrechStrengthEvaluationAction == null)
                    {
                        bendStrechStrengthEvaluationAction = new BendStrechStrengthEvaluationAction();
                        bendStrechStrengthEvaluationAction.StartAction += new EventHandler(action_StartAction);
                        bendStrechStrengthEvaluationAction.SetForce += new SetForceHandler(action_SetForce);
                    }
                    bendStrechStrengthEvaluationAction.Mode = mode;
                    bendStrechStrengthEvaluationAction.Action = action;
                    currentControl = bendStrechStrengthEvaluationAction;
                }
                else if (action == ActionEnum.Rotation)
                {
                    if (rotateStrengthEvaluationAction == null)
                    {
                        rotateStrengthEvaluationAction = new RotateStrengthEvaluationAction();
                        rotateStrengthEvaluationAction.StartAction += new EventHandler(action_StartAction);
                        rotateStrengthEvaluationAction.SetForce += new SetForceHandler(action_SetForce);
                    }
                    rotateStrengthEvaluationAction.Mode = mode;
                    rotateStrengthEvaluationAction.Action = action;
                    currentControl = rotateStrengthEvaluationAction;
                }
            }
            else
            {

                if (action == ActionEnum.Bend)
                {
                    if (mode == ModeEnum.Sokoban || mode == ModeEnum.Free || mode == ModeEnum.FreeConstantResistance || mode == ModeEnum.FreeCounterWeight || mode == ModeEnum.Dengsu || mode == ModeEnum.Lixin)
                    {
                        if (bendStretchFreeAction == null)
                        {
                            bendStretchFreeAction = new BendStretchFreeAction();
                            bendStretchFreeAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchFreeAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchFreeAction.StartAction += new EventHandler(action_StartAction);
                        }
                        //bendStretchFreeAction.ModeEnum = mode;
                        //bendStretchFreeAction.ActionEnum = action;
                        currentControl = bendStretchFreeAction;
                    }
                    else if (mode == ModeEnum.Guided)
                    {
                        if (bendStretchGuidedAction == null)
                        {
                            bendStretchGuidedAction = new BendStretchGuidedAction();
                            bendStretchGuidedAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchGuidedAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchGuidedAction.StartAction += new EventHandler(action_StartAction);
                        }
                        currentControl = bendStretchGuidedAction;
                    }
                    else if (mode == ModeEnum.Initiated)
                    {
                        if (bendStretchAssistedAction == null)
                        {
                            bendStretchAssistedAction = new BendStretchAssistedAction();
                        }
                        currentControl = bendStretchAssistedAction;
                    }
                    else if (mode == ModeEnum.IsotonicA)
                    {
                        if (isotonicAAction == null)
                        {
                            isotonicAAction = new IsotonicAAction();
                            isotonicAAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicAAction.NextTime += new EventHandler(action_NextTime);
                            isotonicAAction.StartAction += new EventHandler(action_StartAction);
                            isotonicAAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicAAction.Type = 0;
                        currentControl = isotonicAAction;
                    }
                    else if (mode == ModeEnum.IsotonicB)
                    {
                        if (isotonicBAction == null)
                        {
                            isotonicBAction = new IsotonicBAction();
                            isotonicBAction.NextTime += new EventHandler(action_NextTime);
                            isotonicBAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicBAction.StartAction += new EventHandler(action_StartAction);
                            isotonicBAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicBAction.Type = 0;
                        currentControl = isotonicBAction;
                    }
                }
                else if (action == ActionEnum.Protrusive)
                {
                    if (mode == ModeEnum.Sokoban || mode == ModeEnum.Free || mode == ModeEnum.FreeConstantResistance || mode == ModeEnum.FreeCounterWeight || mode == ModeEnum.Dengsu || mode == ModeEnum.Lixin)
                    {
                        if (bendStretchFreeAction == null)
                        {
                            bendStretchFreeAction = new BendStretchFreeAction();
                            bendStretchFreeAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchFreeAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchFreeAction.StartAction += new EventHandler(action_StartAction);
                        }
                        //bendStretchFreeAction.ModeEnum = mode;
                        //bendStretchFreeAction.ActionEnum = action;
                        currentControl = bendStretchFreeAction;
                    }
                    else if (mode == ModeEnum.Guided)
                    {
                        if (bendStretchGuidedAction == null)
                        {
                            bendStretchGuidedAction = new BendStretchGuidedAction();
                            bendStretchGuidedAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchGuidedAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchGuidedAction.StartAction += new EventHandler(action_StartAction);
                        }
                        currentControl = bendStretchGuidedAction;
                    }
                    else if (mode == ModeEnum.Initiated)
                    {
                        if (bendStretchAssistedAction == null)
                        {
                            bendStretchAssistedAction = new BendStretchAssistedAction();
                        }
                        currentControl = bendStretchAssistedAction;
                    }
                    else if (mode == ModeEnum.IsotonicA)
                    {
                        if (isotonicAAction == null)
                        {
                            isotonicAAction = new IsotonicAAction();
                            isotonicAAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicAAction.NextTime += new EventHandler(action_NextTime);
                            isotonicAAction.StartAction += new EventHandler(action_StartAction);
                            isotonicAAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicAAction.Type = 0;
                        currentControl = isotonicAAction;
                    }
                    else if (mode == ModeEnum.IsotonicB)
                    {
                        if (isotonicBAction == null)
                        {
                            isotonicBAction = new IsotonicBAction();
                            isotonicBAction.NextTime += new EventHandler(action_NextTime);
                            isotonicBAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicBAction.StartAction += new EventHandler(action_StartAction);
                            isotonicBAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicBAction.Type = 0;
                        currentControl = isotonicBAction;
                    }
                }
                else if (action == ActionEnum.ProtrusiveOrBend)
                {
                    if (mode == ModeEnum.Sokoban || mode == ModeEnum.Free || mode == ModeEnum.FreeConstantResistance || mode == ModeEnum.FreeCounterWeight || mode == ModeEnum.Dengsu || mode == ModeEnum.Lixin)
                    {
                        if (bendStretchFreeAction == null)
                        {
                            bendStretchFreeAction = new BendStretchFreeAction();
                            bendStretchFreeAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchFreeAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchFreeAction.StartAction += new EventHandler(action_StartAction);
                        }
                        //bendStretchFreeAction.ModeEnum = mode;
                        //bendStretchFreeAction.ActionEnum = action;
                        currentControl = bendStretchFreeAction;
                    }
                    else if (mode == ModeEnum.Guided)
                    {
                        if (bendStretchGuidedAction == null)
                        {
                            bendStretchGuidedAction = new BendStretchGuidedAction();
                            bendStretchGuidedAction.NextTime += new EventHandler(action_NextTime);
                            bendStretchGuidedAction.StopAction += new EventHandler(action_StopAction);
                            bendStretchGuidedAction.StartAction += new EventHandler(action_StartAction);
                        }
                        currentControl = bendStretchGuidedAction;
                    }
                    else if (mode == ModeEnum.Initiated)
                    {
                        if (bendStretchAssistedAction == null)
                        {
                            bendStretchAssistedAction = new BendStretchAssistedAction();
                        }
                        currentControl = bendStretchAssistedAction;
                    }
                    else if (mode == ModeEnum.IsotonicA)
                    {
                        if (isotonicAAction == null)
                        {
                            isotonicAAction = new IsotonicAAction();
                            isotonicAAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicAAction.NextTime += new EventHandler(action_NextTime);
                            isotonicAAction.StartAction += new EventHandler(action_StartAction);
                            isotonicAAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicAAction.Type = 0;
                        currentControl = isotonicAAction;
                    }
                    else if (mode == ModeEnum.IsotonicB)
                    {
                        if (isotonicBAction == null)
                        {
                            isotonicBAction = new IsotonicBAction();
                            isotonicBAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicBAction.NextTime += new EventHandler(action_NextTime);
                            isotonicBAction.StartAction += new EventHandler(action_StartAction);
                            isotonicBAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicBAction.Type = 0;
                        currentControl = isotonicBAction;
                    }
                }
                else if (action == ActionEnum.Rotation)
                {
                    if (mode == ModeEnum.Sokoban || mode == ModeEnum.Free || mode == ModeEnum.FreeConstantResistance || mode == ModeEnum.FreeCounterWeight || mode == ModeEnum.Dengsu || mode == ModeEnum.Lixin)
                    {
                        if (rotateFreeAction == null)
                        {
                            rotateFreeAction = new RotateFreeAction();
                            rotateFreeAction.NextTime += new EventHandler(action_NextTime);
                            rotateFreeAction.StopAction += new EventHandler(action_StopAction);
                            rotateFreeAction.StartAction += new EventHandler(action_StartAction);
                        }
                        //rotateFreeAction.ModeEnum = mode;
                        currentControl = rotateFreeAction;
                    }
                    else if (mode == ModeEnum.Guided)
                    {
                        if (rotateGuidedAction == null)
                        {
                            rotateGuidedAction = new RotateGuidedAction();
                            rotateGuidedAction.NextTime += new EventHandler(action_NextTime);
                            rotateGuidedAction.StopAction += new EventHandler(action_StopAction);
                            rotateGuidedAction.StartAction += new EventHandler(action_StartAction);
                        }
                        currentControl = rotateGuidedAction;

                    }
                    else if (mode == ModeEnum.Initiated)
                    {
                        if (rotateAssistedAction == null)
                        {
                            rotateAssistedAction = new RotateAssistedAction();
                        }
                        currentControl = rotateAssistedAction;
                    }
                    else if (mode == ModeEnum.IsotonicA)
                    {
                        if (isotonicAAction == null)
                        {
                            isotonicAAction = new IsotonicAAction();
                            isotonicAAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicAAction.NextTime += new EventHandler(action_NextTime);
                            isotonicAAction.StartAction += new EventHandler(action_StartAction);
                            isotonicAAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicAAction.Type = 1;
                        currentControl = isotonicAAction;
                    }
                    else if (mode == ModeEnum.IsotonicB)
                    {
                        if (isotonicBAction == null)
                        {
                            isotonicBAction = new IsotonicBAction();
                            isotonicBAction.SetForce += new SetForceHandler(action_SetForce);
                            isotonicBAction.NextTime += new EventHandler(action_NextTime);
                            isotonicBAction.StartAction += new EventHandler(action_StartAction);
                            isotonicBAction.StopAction += new EventHandler(action_StopAction);
                        }
                        isotonicBAction.Type = 1;
                        currentControl = isotonicBAction;
                    }
                }
            }

            if (currentControl != null)
            {
                currentControl.Mode = mode;
                currentControl.Action = action;
                currentControl.Controller = this;
                currentControl.Start();
            }
        }

        void action_StartAction(object sender, EventArgs e)
        {
            if (StartAction != null) StartAction(null, null);
        }

        void action_SetForce(float force)
        {
            if (SetForce != null) SetForce(force);
        }

        void action_StopAction(object sender, EventArgs e)
        {
            if (Times != 0)
            {
                //保存最后一组的训练数据
                if (GroupRecordList == null)
                {
                    GroupRecordList = new List<GroupRecord>();
                }

                GroupRecord gr = new GroupRecord();
                gr.GroupNum = CurrentTimes / Times;
                gr.Min = groupActionMinAngle;
                gr.Max = groupActionMaxAngle;
                GroupRecordList.Add(gr);
            }

            endTicks = DateTime.Now.Ticks;
            IsPause = false;
            if (StopAction != null)
            {
                StopAction(null, null);
            }
        }

        void action_NextTime(object sender, EventArgs e)
        {
            //判断是否到下一组
            if (CurrentTimes % Times == 0 && Times > 0)
            {
                if (GroupRecordList == null)
                {
                    GroupRecordList = new List<GroupRecord>();
                }

                GroupRecord gr = new GroupRecord();
                gr.GroupNum = CurrentTimes / Times;
                gr.Min = groupActionMinAngle;
                gr.Max = groupActionMaxAngle;

                groupActionMinAngle = float.MaxValue;
                groupActionMaxAngle = float.MinValue;

                GroupRecordList.Add(gr);
            }

            if (NextTime != null)
            {
                NextTime(null, null);
            }
        }
        #endregion

        #region  判断连接是否中断
        /// <summary>`
        /// 时间触发
        /// </summary>
        /// <param name="obj"></param>
        void TimeCallBackFunc(object obj)
        {
            if (DateTime.Now.Ticks - lastReceiveTime > 30000000)
            {
                if (this.isConnected)
                {
                    //如果大于3秒钟还未接收到数据，则认为连接中断
                    this.IsConnected = false;
                }
                else
                {
                    //ThreadPool.QueueUserWorkItem((x) => { dataCommunication.Open(); }, null);
                }
            }
        }
        #endregion

        #region GetTrainRecord
        public TrainRecord GetTrainRecord()
        {
            if (currentControl != null)
            {
                endTicks = DateTime.Now.Ticks;
                TrainRecord record = new TrainRecord();
                record.MaxAngle = MaxAngle;
                record.MinAngle = MinAngle;
                record.PushRodValue = PushRodAngle;
                record.Force = Force;
                record.Speed = Speed;
                record.Times = Times * GroupNum;
                record.ModeId = (int)currentControl.Mode;
                record.ActionId = (int)currentControl.Action;
                record.FactTimes = currentControl.CurrentTimes;
                record.IsFit = 0;
                record.StartTime = new DateTime(startTicks);
                record.EndTime = new DateTime(endTicks);
                record.ExMinutes = (int)((endTicks - startTicks) / (10000 * 1000));
                record.ExerciseDate = record.StartTime.Date;
                record.TargetLine = targetLine;
                record.MaxForce = maxForce;
                record.RealMinAngle = actionMinAngle;
                record.RealMaxAngle = actionMaxAngle;
                record.GroupNum = GroupNum;
                if (GroupRecordList != null)
                {
                    record.GroupRecords = GroupRecordList.ToArray();
                }

                if (sumForceNum > 0)
                {
                    record.AvgForce = (float)(sumForce / sumForceNum);
                }

                if (RealLine != null)
                {
                    record.RealLine = RealLine.ToArray();
                }
                if (ForceLine != null)
                {
                    record.ForceLine = ForceLine.ToArray();
                }

                return record;
            }

            return null;
        }
        #endregion
    }
}
