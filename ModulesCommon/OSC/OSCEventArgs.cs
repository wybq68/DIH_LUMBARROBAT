using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.ModulesCommon
{

    public class CarmaEventArgs : EventArgs
    {

        private int comnandType;
        /// <summary>
        /// 类型
        /// </summary>
        public int ComnandType
        {
            get { return comnandType; }
            set { comnandType = value; }
        }


        //private CameraInfo _info;
        ///// <summary>
        ///// 视角类型
        ///// </summary>
        //public CameraInfo Info
        //{
        //    get { return _info; }
        //    set { _info = value; }
        //}
    }


    public class GameReturnEventArgs : EventArgs
    {

        private int comnandType;
        /// <summary>
        /// 类型
        /// </summary>
        public int ComnandType
        {
            get { return comnandType; }
            set { comnandType = value; }
        }


        //private ULTROBOT.Game.Model.GameResultInfo _info;
        ///// <summary>
        ///// 视角类型
        ///// </summary>
        //public  ULTROBOT.Game.Model.GameResultInfo Info
        //{
        //    get { return _info; }
        //    set { _info = value; }
        //}
    }

    /// <summary>
    /// 
    /// </summary>
    public class EyeMoveInfonEventArgs : EventArgs
    {
        private int comnandType;

        public int ComnandType
        {
            get { return comnandType; }
            set { comnandType = value; }
        }

        //private MoveCommandInfo _info;

        //public MoveCommandInfo Info
        //{
        //    get { return _info; }
        //    set { _info = value; }
        //}




    }


    public class StartInitEventArgs : EventArgs
    {
        private int _InitType;

        public int InitType
        {
            get { return _InitType; }
            set { _InitType = value; }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public delegate void CameraMoveEventHandler(CarmaEventArgs e);


    /// <summary>
    /// 游戏返回事件
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public delegate void GameReturnEventHandler(GameReturnEventArgs e);


    /// <summary>
    /// 眼动事件
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public delegate void EyeMoveInfonEventHandler(EyeMoveInfonEventArgs e);

    /// <summary>
    /// 启动初始化
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">EventArgs.</param>
    public  delegate void StartInitEventHandler(StartInitEventArgs e);

  


}
