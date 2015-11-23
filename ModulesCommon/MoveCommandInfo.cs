using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ModulesCommon
{
  public  class MoveCommandInfo
    {


        private bool _isStart;
        /// <summary>
        /// 开始
        /// </summary>
        public bool IsStart
        {
            get { return _isStart; }
            set { _isStart = value; }
        }

        private bool _isPasue;
       /// <summary>
       /// 暂停
       /// </summary>
        public bool IsPasue
        {
            get { return _isPasue; }
            set { _isPasue = value; }
        }

        private bool _isStop;
      /// <summary>
      /// 停止
      /// </summary>
        public bool IsStop
        {
            get { return _isStop; }
            set { _isStop = value; }
        }

      //private Point3DInfo _move1;
      ///// <summary>
      ///// 移动目标点1
      ///// </summary>
      //public Point3DInfo Move1
      //{
      //    get { return _move1; }
      //    set { _move1 = value; }
      //}

      //private Point3DInfo _move2;
      ///// <summary>
      ///// 移动目标点2
      ///// </summary>
      //public Point3DInfo Move2
      //{
      //    get { return _move2; }
      //    set { _move2 = value; }
      //}

      //private Point3DInfo _move3;
      ///// <summary>
      ///// 移动目标点3
      ///// </summary>
      //public Point3DInfo Move3
      //{
      //    get { return _move3; }
      //    set { _move3 = value; }
      //}


      private int forceFlag;

      public int ForceFlag
      {
          get { return forceFlag; }
          set { forceFlag = value; }
      }

     

    


    }

  public class NextInfo
  {
      private int snn = 0;

      public int Snn
      {
          get { return snn; }
          set { snn = value; }
      }
      private bool _isNextPoint;
      /// <summary>
      /// 是否有下一个点
      /// </summary>
      public bool IsNextPoint
      {
          get { return _isNextPoint; }
          set { _isNextPoint = value; }
      }

      //private Point3DInfo _nextPoint;
      ///// <summary>
      ///// 下一个目标点坐标
      ///// </summary>
      //public Point3DInfo NextPoint
      //{
      //    get { return _nextPoint; }
      //    set { _nextPoint = value; }
      //}
  }

  public class ArrivedInfo
  {
      private int snn = 0;

      public int Snn
      {
          get { return snn; }
          set { snn = value; }
      }

      private bool _isArrive;
      /// <summary>
      /// 是否到达
      /// </summary>
      public bool IsArrive
      {
          get { return _isArrive; }
          set { _isArrive = value; }
      }

      //private Point3DInfo _arrivePoint;
      ///// <summary>
      ///// 到达点
      ///// </summary>
      //public Point3DInfo ArrivePoint
      //{
      //    get { return _arrivePoint; }
      //    set { _arrivePoint = value; }
      //}
 
  }

}
