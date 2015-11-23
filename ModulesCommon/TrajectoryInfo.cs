using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ModulesCommon
{
    /// <summary>
    /// 路径数据包体
    /// </summary>
    public class TrajectoryInfo
    {
        
        private bool isOrigin;
        /// <summary>
        /// 是否为原点
        /// </summary>
        public bool IsOrigin
        {
            get { return isOrigin; }
            set { isOrigin = value; }
        }

        //private Point3DInfo _originPoint=new Point3DInfo();
        ///// <summary>
        ///// 原点坐标
        ///// </summary>
        //public Point3DInfo OriginPoint
        //{
        //    get { return _originPoint; }
        //    set { _originPoint = value; }
        //}

        /// <summary>
        /// 轨迹点集合
        /// </summary>
        //private List<Point3DInfo> _trajectoryPoints=new List<Point3DInfo>();
        ///// <summary>
        ///// 轨迹点集合
        ///// </summary>
        //public List<Point3DInfo> TrajectoryPoints
        //{
        //    get { return _trajectoryPoints; }
        //    set { _trajectoryPoints = value; }
        //}
        /// <summary>
        /// 是否创建力量箭头
        /// </summary>
        private bool _isCreateArrow=false;
        /// <summary>
        /// 是否创建力量箭头
        /// </summary>
        public bool IsCreateArrow
        {
            get { return _isCreateArrow; }
            set { _isCreateArrow = value; }
        }
        /// <summary>
        /// 是否清除
        /// </summary>
        private bool _isClear=false;
        /// <summary>
        /// 是否清除
        /// </summary>
        public bool IsClear
        {
            get { return _isClear; }
            set { _isClear = value; }
        }


        private bool _isCrate=false;
        /// <summary>
        /// 是否创建新轨迹
        /// </summary>
        public bool IsCrate
        {
            get { return _isCrate; }
            set { _isCrate = value; }
        }

        private int _trajectoryType;
        /// <summary>
        /// 0，普通轨迹，1圆形轨迹，3，螺旋
        /// </summary>
        public int TrajectoryType
        {
            get { return _trajectoryType; }
            set { _trajectoryType = value; }
        }

        //private Point3DInfo _centerPoint=new Point3DInfo();
        ///// <summary>
        ///// 圆心
        ///// </summary>
        //public Point3DInfo CenterPoint
        //{
        //    get { return _centerPoint; }
        //    set { _centerPoint = value; }
        //}

        private double radius;
        /// <summary>
        /// 半径
        /// </summary>
        public double Radius
        {
            get { return radius; }
            set { radius = value; }
        }


        private int displayType;
        /// <summary>
        /// 显示方式 2 为2D，3为3D
        /// </summary>
        public int DisplayType
        {
            get { return displayType; }
            set { displayType = value; }
        }

        /// <summary>
        /// 默认视角
        /// </summary>
        //private Point3DInfo _defaultPosition;

        //public Point3DInfo DefaultPosition
        //{
        //    get { return _defaultPosition; }
        //    set { _defaultPosition = value; }
        //}

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


        //List<Point3DInfo> fitPoints = new List<Point3DInfo>();

        ///// <summary>
        ///// Fit点
        ///// </summary>

        //public List<Point3DInfo> FitPoints
        //{
        //    get { return fitPoints; }
        //    set { fitPoints = value; }
        //}
    
        ///// <summary>
        ///// 摄像机位置
        ///// </summary>
        //private CameraPositionType cameraPosition;
        ///// <summary>
        ///// 摄像机位置
        ///// </summary>
        //public CameraPositionType CameraPosition
        //{
        //    get { return cameraPosition; }
        //    set { cameraPosition = value; }
        //}

        private int _handType = 0;

        public int HandType
        {
            get { return _handType; }
            set { _handType = value; }
        }


        /// <summary>
        /// 内环
        /// </summary>
        //private List<Point3DInfo> _innerCirclePoints = new List<Point3DInfo>();

        //public List<Point3DInfo> InnerCirclePoints
        //{
        //    get { return _innerCirclePoints; }
        //    set { _innerCirclePoints = value; }
        //}


        ///// <summary>
        ///// 外环
        ///// </summary>
        //private List<Point3DInfo> _outCirclePoints = new List<Point3DInfo>();

        //public List<Point3DInfo> OutCirclePoints
        //{
        //    get { return _outCirclePoints; }
        //    set { _outCirclePoints = value; }
        //}
      
    }
}
