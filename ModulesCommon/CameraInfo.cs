using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ModulesCommon
{
  public  class CameraInfo
    {

       
        /// <summary>
        /// 视角移动类型
        /// </summary>
        private CameraMoveCommandType cameraMoveCommand;

        /// <summary>
        /// 视角移动类型
        /// </summary>
        public CameraMoveCommandType CameraMoveCommand
        {
            get { return cameraMoveCommand; }
            set { cameraMoveCommand = value; }
        }

        /// <summary>
        /// 摄像机位置
        /// </summary>
        private CameraPositionType cameraPosition;
        /// <summary>
        /// 摄像机位置
        /// </summary>
        public CameraPositionType CameraPosition
        {
            get { return cameraPosition; }
            set { cameraPosition = value; }
        }

        /// <summary>
        /// 0,移动视角  1移动位置
        /// </summary>
        private int cameraType;

        public int CameraType
        {
            get { return cameraType; }
            set { cameraType = value; }
        }
        //private string cameraPoint;
        ///// <summary>
        ///// 摄像机位置
        ///// </summary>
        //public string CameraPoint
        //{
        //    get { return cameraPoint; }
        //    set { cameraPoint = value; }
        //}


        private int _scale=200;

        public int Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }


        //private Point3DInfo camera3DView;

        //public Point3DInfo Camera3DView
        //{
        //    get { return camera3DView; }
        //    set { camera3DView = value; }
        //}

        //private Point3DInfo camera2DView;

        //public Point3DInfo Camera2DView
        //{
        //    get { return camera2DView; }
        //    set { camera2DView = value; }
        //}

        //private Point3DInfo cameraPoint;

        //public Point3DInfo CameraPoint
        //{
        //    get { return cameraPoint; }
        //    set { cameraPoint = value; }
        //}

        private bool isStartMove;
        /// <summary>
        /// 是否移动
        /// </summary>
        public bool IsStartMove
        {
            get { return isStartMove; }
            set { isStartMove = value; }
        }
     
    }


  public enum CameraMoveCommandType
  {
      /// <summary>
      /// 左
      /// </summary>
      LEFT = 1,
      /// <summary>
      /// 右
      /// </summary>
      RIGHT = 2,
      /// <summary>
      /// 上
      /// </summary>
      TOP = 3,
      /// <summary>
      /// 下
      /// </summary>
      DOWN = 4,
      /// <summary>
      /// 放大
      /// </summary>
      AMPLIFY = 5,
      /// <summary>
      /// 缩小
      /// </summary>
      REDUCE = 6,

      LEFT_ROTATE=7,

      RIGHT_ROTATE=8




  }

  /// <summary>
  /// 视角类型
  /// </summary>
  public enum CameraPositionType
  {
      /// <summary>
      /// 正面
      /// </summary>
      FRONT,
      /// <summary>
      /// 背面
      /// </summary>
      BACK,
      /// <summary>
      /// 上面
      /// </summary>
      TOP,
      /// <summary>
      /// 底部
      /// </summary>
      BOTTOM,
      /// <summary>
      /// 左侧
      /// </summary>
      LEFT,
      /// <summary>
      /// 右侧
      /// </summary>
      RIGHT,
      /// <summary>
      /// 正面俯视45度
      /// </summary>
      FRONT_OVERLOOK


  }
}
