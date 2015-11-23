using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{
    #region DetailDemo
    public class ReportDetail
    {
        private string exItemId;
        /// <summary>
        /// 动作ID
        /// </summary>
        public string ExItemId
        {
            get { return exItemId; }
            set { exItemId = value; }
        }

        private string time;
        /// <summary>
        /// 训练时间
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        private string totalTime;
        /// <summary>
        /// 训练时长
        /// </summary>
        public string TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        private string _seeionId;
        /// <summary>
        /// 分组编号
        /// </summary>
        public string SeeionId
        {
            get { return _seeionId; }
            set { _seeionId = value; }
        }
        private string _patientId;
        /// <summary>
        /// 病人编号
        /// </summary>
        public string PatientId
        {
            get { return _patientId; }
            set { _patientId = value; }
        }
        private string _actionId;
        /// <summary>
        /// 动作ID
        /// </summary>
        public string ActionId
        {
            get { return _actionId; }
            set { _actionId = value; }
        }
        private string _modeId;
        /// <summary>
        /// 模式ID
        /// </summary>
        public string ModeId
        {
            get { return _modeId; }
            set { _modeId = value; }
        }
        private string _pushRodValue;
        /// <summary>
        /// 推杆高度
        /// </summary>
        public string PushRodValue
        {
            get { return _pushRodValue; }
            set { _pushRodValue = value; }
        }
        private string _isFit;
        /// <summary>
        /// 是否Fit
        /// </summary>
        public string IsFit
        {
            get { return _isFit; }
            set { _isFit = value; }
        }
        private string _exerciseDate;
        /// <summary>
        /// 日期
        /// </summary>
        public string ExerciseDate
        {
            get { return _exerciseDate; }
            set { _exerciseDate = value; }
        }
        private string _startTime;
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }
        private string _endTime;
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        private string _speed;
        /// <summary>
        /// 速度
        /// </summary>
        public string Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private string _robotForce;
        /// <summary>
        /// 力量
        /// </summary>
        public string RobotForce
        {
            get { return _robotForce; }
            set { _robotForce = value; }
        }
        private string _minAngle;
        /// <summary>
        /// 最小角度
        /// </summary>
        public string MinAngle
        {
            get { return _minAngle; }
            set { _minAngle = value; }
        }
        private string _maxAngle;
        /// <summary>
        /// 最大角度
        /// </summary>
        public string MaxAngle
        {
            get { return _maxAngle; }
            set { _maxAngle = value; }
        }
        private string _times;
        /// <summary>
        /// 应训次数
        /// </summary>
        public string Times
        {
            get { return _times; }
            set { _times = value; }
        }
        private string _factTimes;
        /// <summary>
        /// 实际次数
        /// </summary>
        public string FactTimes
        {
            get { return _factTimes; }
            set { _factTimes = value; }
        }
        private string _exMinutes;
        /// <summary>
        /// 训练时间
        /// </summary>
        public string ExMinutes
        {
            get { return _exMinutes; }
            set { _exMinutes = value; }
        }
        private string _maxforce;
        /// <summary>
        /// 最大力量
        /// </summary>
        public string Maxforce
        {
            get { return _maxforce; }
            set { _maxforce = value; }
        }
        private string _modeName;
        /// <summary>
        /// 模式名称
        /// </summary>
        public string ModeName
        {
            get { return _modeName; }
            set { _modeName = value; }
        }
        private string _actionName;
        /// <summary>
        /// 动作名称
        /// </summary>
        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; }
        }
    }
    #endregion
}
