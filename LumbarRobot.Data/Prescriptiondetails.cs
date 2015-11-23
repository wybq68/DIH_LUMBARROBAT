using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Data
{

    #region Prescription

    /// <summary>
    /// Prescriptiondetails object for NHibernate mapped table 'prescriptiondetails'.
    /// </summary>
    public class Prescriptiondetails
    {
        #region Member Variables
        private string _id;
        private int _actionId;
        private int _modeId;
        private int _speed;
        private string _prescriptionId;
        private int _pForce;
        private int _minAngle;
        private int _maxAngle;
        private int? _pGroup;
        private int _times;
        private int _lastLocation;
        private DateTime? _lastTime;
        #endregion

        #region Public Properties
        /// <summary>
        /// ID
        /// </summary>
        public virtual string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 动作
        /// </summary>
        public virtual int ActionId
        {
            get { return _actionId; }
            set { _actionId = value; }
        }
        /// <summary>
        /// 模式
        /// </summary>
        public virtual int ModeId
        {
            get { return _modeId; }
            set { _modeId = value; }
        }
        /// <summary>
        /// 疗程ID
        /// </summary>
        public virtual string PrescriptionId
        {
            get { return _prescriptionId; }
            set { _prescriptionId = value; }
        }
        /// <summary>
        /// 力量
        /// </summary>
        public virtual int PForce
        {
            get { return _pForce; }
            set { _pForce = value; }
        }
        /// <summary>
        /// 速度
        /// </summary>
        public virtual int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        /// <summary>
        /// 最小角度
        /// </summary>
        public virtual int MinAngle
        {
            get { return _minAngle; }
            set { _minAngle = value; }
        }
        /// <summary>
        /// 最大角度
        /// </summary>
        public virtual int MaxAngle
        {
            get { return _maxAngle; }
            set { _maxAngle = value; }
        }
        /// <summary>
        /// 分组
        /// </summary>
        public virtual int? PGroup
        {
            get { return _pGroup; }
            set { _pGroup = value; }
        }
        /// <summary>
        /// 次数
        /// </summary>
        public virtual int Times
        {
            get { return _times; }
            set { _times = value; }
        }
        /// <summary>
        /// 位置
        /// </summary>
        public virtual int LastLocation
        {
            get { return _lastLocation; }
            set { _lastLocation = value; }
        }

        /// <summary>
        /// 最后时间
        /// </summary>
        public virtual DateTime? LastTime
        {
            get { return _lastTime; }
            set { _lastTime = value; }
        }
        #endregion
    }
    #endregion 
}
