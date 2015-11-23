using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ViewModels
{
    #region 训练动作


    public class ItemDemo
    {
        private string _id;
        /// <summary>
        /// 主键
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _speed;
        /// <summary>
        /// 速度
        /// </summary>
        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        private int _force;
        /// <summary>
        /// 力量
        /// </summary>
        public int Force
        {
            get { return _force; }
            set { _force = value; }
        }

        private int _groupTimes;
        /// <summary>
        /// 分组
        /// </summary>
        public int GroupTimes
        {
            get { return _groupTimes; }
            set { _groupTimes = value; }
        }

        private int _times;
        /// <summary>
        /// 次数
        /// </summary>
        public int Times
        {
            get { return _times; }
            set { _times = value; }
        }
        private int _minAngle;
        /// <summary>
        /// 最小角度
        /// </summary>
        public int MinAngle
        {
            get { return _minAngle; }
            set { _minAngle = value; }
        }
        private int _maxAngle;
        /// <summary>
        /// 最大角度
        /// </summary>
        public int MaxAngle
        {
            get { return _maxAngle; }
            set { _maxAngle = value; }
        }
        private int _mode;
        /// <summary>
        /// 模式ID
        /// </summary>
        public int Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        private int _actionId;
        /// <summary>
        /// 动作ID
        /// </summary>
        public int ActionId
        {
            get { return _actionId; }
            set { _actionId = value; }
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

        private string _modeName;
        /// <summary>
        /// 模式名称
        /// </summary>
        public string ModeName
        {
            get { return _modeName; }
            set { _modeName = value; }
        }

        private string _picturePath;
        /// <summary>
        /// 图片路劲
        /// </summary>
        public string PicturePath
        {
            get { return _picturePath; }
            set { _picturePath = value; }
        }

        private int _position;
        /// <summary>
        /// 初始位置
        /// </summary>
        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }
    }
    #endregion

    #region 日期报告
    public class DayDemo
    {
        private string dayIndex;

        public string DayIndex
        {
            get { return dayIndex; }
            set { dayIndex = value; }
        }

        private string dayTime;

        public string DayTime
        {
            get { return dayTime; }
            set { dayTime = value; }
        }

        private string dayTotalTime;

        public string DayTotalTime
        {
            get { return dayTotalTime; }
            set { dayTotalTime = value; }
        }
    }
    #endregion

    #region 模式报告
    public class ModeDemo
    {
        private string modeID;

        public string ModeID
        {
            get { return modeID; }
            set { modeID = value; }
        }
        private string modeName;

        public string ModeName
        {
            get { return modeName; }
            set { modeName = value; }
        }

        private string totalTime;

        public string TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }
    }
    #endregion

    #region 动作
    public class ExItemDemo
    {
        private string _exItemId;
        /// <summary>
        /// ID
        /// </summary>
        public string ExItemId
        {
            get { return _exItemId; }
            set { _exItemId = value; }
        }
        private string _exItemName;
        /// <summary>
        /// 名称
        /// </summary>
        public string ExItemName
        {
            get { return _exItemName; }
            set { _exItemName = value; }
        }
    }
    #endregion

    #region 旋转Fit结果
    public class FitResultX
    {
        private string dayIndex;
        /// <summary>
        /// 编号
        /// </summary>
        public string DayIndex
        {
            get { return dayIndex; }
            set { dayIndex = value; }
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

        private string dayTime;
        /// <summary>
        /// 日期
        /// </summary>
        public string DayTime
        {
            get { return dayTime; }
            set { dayTime = value; }
        }

        private string _leftValue;
        /// <summary>
        /// 左
        /// </summary>
        public string LeftValue
        {
            get { return _leftValue; }
            set { _leftValue = value; }
        }
        private string _rightValue;
        /// <summary>
        /// 右
        /// </summary>
        public string RightValue
        {
            get { return _rightValue; }
            set { _rightValue = value; }
        }
    }
    #endregion

    #region 曲伸Fit结果
    public class FitResultQ
    {
        private string dayIndex;
        /// <summary>
        /// 编号
        /// </summary>
        public string DayIndex
        {
            get { return dayIndex; }
            set { dayIndex = value; }
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

        private string dayTime;
        /// <summary>
        /// 日期
        /// </summary>
        public string DayTime
        {
            get { return dayTime; }
            set { dayTime = value; }
        }

        private string _maxValue;
        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        private string _minValue;
        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }
    }
    #endregion

    #region 评测结果
    public class MyFitResult
    {
        private string dayIndex;
        /// <summary>
        /// 编号
        /// </summary>
        public string DayIndex
        {
            get { return dayIndex; }
            set { dayIndex = value; }
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

        private string dayTime;
        /// <summary>
        /// 日期
        /// </summary>
        public string DayTime
        {
            get { return dayTime; }
            set { dayTime = value; }
        }

        private string _maxValue;
        /// <summary>
        /// 旋转最大值
        /// </summary>
        public string MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }
        private string _minValue;
        /// <summary>
        /// 旋转最小值
        /// </summary>
        public string MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }


        private string _frontValue;
        /// <summary>
        /// 屈伸前
        /// </summary>
        public string FrontValue
        {
            get { return _frontValue; }
            set { _frontValue = value; }
        }

        private string _behindValue;
        /// <summary>
        /// 屈伸后
        /// </summary>
        public string BehindValue
        {
            get { return _behindValue; }
            set { _behindValue = value; }
        }

        private string _frontPowerValue;
        /// <summary>
        /// 屈伸前力量
        /// </summary>
        public string FrontPowerValue
        {
            get { return _frontPowerValue; }
            set { _frontPowerValue = value; }
        }
        private string _behindPowerValue;
        /// <summary>
        /// 屈伸后力量
        /// </summary>
        public string BehindPowerValue
        {
            get { return _behindPowerValue; }
            set { _behindPowerValue = value; }
        }
        private string _leftValue;
        /// <summary>
        /// 旋转左
        /// </summary>
        public string LeftValue
        {
            get { return _leftValue; }
            set { _leftValue = value; }
        }
        private string _rightValue;
        /// <summary>
        /// 旋转右
        /// </summary>
        public string RightValue
        {
            get { return _rightValue; }
            set { _rightValue = value; }
        }
    }
    #endregion

    #region 测试结果
    public class FitResultCount
    {
        private string _fitResultName;
        /// <summary>
        /// 评测项目
        /// </summary>
        public string FitResultName
        {
            get { return _fitResultName; }
            set { _fitResultName = value; }
        }

        private string _resultCount;
        /// <summary>
        /// 测试次数
        /// </summary>
        public string ResultCount
        {
            get { return _resultCount; }
            set { _resultCount = value; }
        }
    }
    #endregion

    #region 类


    public class FitResultDemo
    {
        private string _evaluteDetailId;

        public string EvaluteDetailId
        {
            get { return _evaluteDetailId; }
            set { _evaluteDetailId = value; }
        }

        private string _actionName;

        public string ActionName
        {
            get { return _actionName; }
            set { _actionName = value; }
        }
        private string _maxV;

        public string MaxV
        {
            get { return _maxV; }
            set { _maxV = value; }
        }
        private string _lastValue;

        public string LastValue
        {
            get { return _lastValue; }
            set { _lastValue = value; }
        }
        private string _fatigueIndex;

        public string FatigueIndex
        {
            get { return _fatigueIndex; }
            set { _fatigueIndex = value; }
        }
        private string _evaluteDetailDate;

        public string EvaluteDetailDate
        {
            get { return _evaluteDetailDate; }
            set { _evaluteDetailDate = value; }
        }
    }

    public class FitDemo
    {
        private bool isChecked;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; }
        }

        private string dayIndex;
        /// <summary>
        /// 编号
        /// </summary>
        public string DayIndex
        {
            get { return dayIndex; }
            set { dayIndex = value; }
        }

        private string dayTime;
        /// <summary>
        /// 日期
        /// </summary>
        public string DayTime
        {
            get { return dayTime; }
            set { dayTime = value; }
        }
    }
    #endregion
}
