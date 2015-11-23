using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LumbarRobot.ModulesCommon
{
   public class GameResultInfo
    {
        private int _score;
         /// <summary>
         ///得分
         /// </summary>
        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        /// <summary>
        /// 完成时间（按秒计算）
        /// </summary>
        private int duration;
        /// <summary>
        /// 完成时间（按秒计算）
        /// </summary>
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }
        

        /// <summary>
        /// 完成百分比
        /// </summary>
        private int completeness;
        /// <summary>
        /// 完成百分比
        /// </summary>
        public int Completeness 
        {
            get { return completeness; }
            set { completeness = value; }
        }

        /// <summary>
        /// 正确完成次数
        /// </summary>
        private int accuracy;
        /// <summary>
        /// 正确完成次数
        /// </summary>
        public int Accuracy 
        {
            get { return accuracy; }
            set { accuracy = value; }
        }
     
        
       
    }
}
