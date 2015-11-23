using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Enums;

namespace LumbarRobot.Common
{
    public class PrintOutputInfo
    {
        private string _intervalValue;
        /// <summary>
        /// 训练时间
        /// </summary>
        public string IntervalValue
        {
            get { return _intervalValue; }
            set { _intervalValue = value; }
        }
        private ActionEnum _mode;
        /// <summary>
        /// 模式
        /// </summary>
        public ActionEnum Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        private string _fitTime;
        /// <summary>
        /// 评测时间
        /// </summary>
        public string FitTime
        {
            get { return _fitTime; }
            set { _fitTime = value; }
        }

        private string _explosiveForceFirst;
        /// <summary>
        /// 爆发力1
        /// </summary>
        public string ExplosiveForceFirst
        {
            get { return _explosiveForceFirst; }
            set { _explosiveForceFirst = value; }
        }

        private string _explosiveForceSecond;
        /// <summary>
        ///  爆发力2
        /// </summary>
        public string ExplosiveForceSecond
        {
            get { return _explosiveForceSecond; }
            set { _explosiveForceSecond = value; }
        }

        private string _explosiveForceThird;
        /// <summary>
        /// 爆发力3
        /// </summary>
        public string ExplosiveForceThird
        {
            get { return _explosiveForceThird; }
            set { _explosiveForceThird = value; }
        }

        private string _explosiveForceAVG;
        /// <summary>
        /// 爆发力平均值
        /// </summary>
        public string ExplosiveForceAVG
        {
            get { return _explosiveForceAVG; }
            set { _explosiveForceAVG = value; }
        }

        private string _explosiveForceSD;
        /// <summary>
        /// 爆发力标准差
        /// </summary>
        public string ExplosiveForceSD
        {
            get { return _explosiveForceSD; }
            set { _explosiveForceSD = value; }
        }

        private string _explosiveForceSV;
        /// <summary>
        /// 爆发力标准值（最大值-最小值）/最大值*100%
        /// </summary>
        public string ExplosiveForceSV
        {
            get { return _explosiveForceSV; }
            set { _explosiveForceSV = value; }
        }

        private string _explosiveForceDifference;
        /// <summary>
        /// 爆发力双侧差异
        /// </summary>
        public string ExplosiveForceDifference
        {
            get { return _explosiveForceDifference; }
            set { _explosiveForceDifference = value; }
        }

        private string _EnduranceFirst;
        /// <summary>
        /// 耐力1
        /// </summary>
        public string EnduranceFirst
        {
            get { return _EnduranceFirst; }
            set { _EnduranceFirst = value; }
        }
        private string _EnduranceSecond;
        /// <summary>
        /// 耐力2
        /// </summary>
        public string EnduranceSecond
        {
            get { return _EnduranceSecond; }
            set { _EnduranceSecond = value; }
        }
        private string _EnduranceThird;
        /// <summary>
        /// 耐力3
        /// </summary>
        public string EnduranceThird
        {
            get { return _EnduranceThird; }
            set { _EnduranceThird = value; }
        }
        private string _EnduranceAVG;
        /// <summary>
        /// 耐力平均值
        /// </summary>
        public string EnduranceAVG
        {
            get { return _EnduranceAVG; }
            set { _EnduranceAVG = value; }
        }
        private string _EnduranceSD;
        /// <summary>
        /// 耐力标准差
        /// </summary>
        public string EnduranceSD
        {
            get { return _EnduranceSD; }
            set { _EnduranceSD = value; }
        }
        private string _EnduranceSV;
        /// <summary>
        /// 耐力标准值 （最大值-最小值）/最大值*100%
        /// </summary>
        public string EnduranceSV
        {
            get { return _EnduranceSV; }
            set { _EnduranceSV = value; }
        }

        private string _EnduranceDifference;
        /// <summary>
        /// 耐力双侧差异
        /// </summary>
        public string EnduranceDifference
        {
            get { return _EnduranceDifference; }
            set { _EnduranceDifference = value; }
        }

        private string _laborIndexFirst;
        /// <summary>
        /// 指数1
        /// </summary>
        public string LaborIndexFirst
        {
            get { return _laborIndexFirst; }
            set { _laborIndexFirst = value; }
        }
        private string _laborIndexSecond;
        /// <summary>
        /// 指数2
        /// </summary>
        public string LaborIndexSecond
        {
            get { return _laborIndexSecond; }
            set { _laborIndexSecond = value; }
        }
        private string _laborIndexThird;
        /// <summary>
        /// 指数2
        /// </summary>
        public string LaborIndexThird
        {
            get { return _laborIndexThird; }
            set { _laborIndexThird = value; }
        }
        private string _laborIndexAVG;
        /// <summary>
        /// 指数平均值
        /// </summary>
        public string LaborIndexAVG
        {
            get { return _laborIndexAVG; }
            set { _laborIndexAVG = value; }
        }
        private string _laborIndexSD;
        /// <summary>
        /// 指数标准差
        /// </summary>
        public string LaborIndexSD
        {
            get { return _laborIndexSD; }
            set { _laborIndexSD = value; }
        }
        private string _laborIndexSV;
        /// <summary>
        /// 指数标准值 （最大值-最小值）/最大值*100%
        /// </summary>
        public string LaborIndexSV
        {
            get { return _laborIndexSV; }
            set { _laborIndexSV = value; }
        }

        private string _laborIndexDifference;
        /// <summary>
        /// 指数双侧差异
        /// </summary>
        public string LaborIndexDifference
        {
            get { return _laborIndexDifference; }
            set { _laborIndexDifference = value; }
        }
    }
}
