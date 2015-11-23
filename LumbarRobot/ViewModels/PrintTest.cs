using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using System.Windows.Documents;
using System.Xml;
using System.Windows;
using System.Windows.Markup;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Serialization;
using System.Windows.Xps.Packaging;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using LumbarRobot.Commands;
using LumbarRobot.DAL;
using Remotion.Linq.Collections;
using System.Data;
using LumbarRobot.Data;
using LumbarRobot.Common.Enums;
using Visifire.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using LumbarRobot.Enums;
using Microsoft.Research.DynamicDataDisplay;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using LumbarRobot.ActionUserControl;



///**********************************
///打印模块  测试
///**********************************
namespace LumbarRobot.ViewModels
{
    public class PrintTest : ViewModelBase
    {
        #region 变量
        private int nColWidth = 120;
        private int nRowHeight = 40;
        Chart chart = new Chart();
        Dispatcher dispatcher { get; set; }
        /// <summary>
        /// 旋转的Fit集合
        /// </summary>
        ObservableCollection<FitResultX> FitResultXList = null;
        /// <summary>
        /// 曲伸的Fit集合
        /// </summary>
        ObservableCollection<FitResultQ> FitResultQList = null;

        /// <summary>
        /// 旋转力量的Fit集合
        /// </summary>
        ObservableCollection<FitResultX> FitResultXPowerList = null;
        /// <summary>
        /// 曲伸力量的Fit集合
        /// </summary>
        ObservableCollection<FitResultQ> FitResultQPowerList = null;

        /// <summary>
        /// 显示集合
        /// </summary>
        ObservableCollection<MyFitResult> FitResultList = null;
        /// <summary>
        /// 前屈
        /// </summary>
        PrintOutputInfo Protrusive = new PrintOutputInfo();
        /// <summary>
        /// 后伸
        /// </summary>
        PrintOutputInfo Bend = new PrintOutputInfo();
        /// <summary>
        /// 旋转显示集合 左
        /// </summary>
        PrintOutputInfo RotationLeft = new PrintOutputInfo();
        /// <summary>
        /// 旋转显示集合 右
        /// </summary>
        PrintOutputInfo RotationRigth = new PrintOutputInfo();

        /// <summary>
        /// 前屈折线图数据
        /// </summary>
        ObservableCollection<EvaluteDetail> ProtrusiveList = null;

        /// <summary>
        /// 后伸折线图数据
        /// </summary>
        ObservableCollection<EvaluteDetail> BendList = null;

        /// <summary>
        /// 旋转左折线图数据
        /// </summary>
        ObservableCollection<EvaluteDetail> RotationLeftList = null;

        /// <summary>
        /// 旋转右折线图数据
        /// </summary>
        ObservableCollection<EvaluteDetail> RotationRigthList = null;

        decimal? MaxProtrusive;
        decimal? MaxBend ;
        decimal? MaxRotationLeft ;
        decimal? MaxRotationRight;

        #endregion

        #region 声明属性
        private IDocumentPaginatorSource _mydocument;
        /// <summary>
        /// 报表内容 绑定到界面显示
        /// </summary>
        public IDocumentPaginatorSource mydocument
        {
            get { return _mydocument; }
            set
            {
                _mydocument = value;
                RaisePropertyChanged("mydocument");
            }
        }
        private ReportPrintControl myPrintWin;

        public ReportPrintControl MyPrintWin
        {
            get { return myPrintWin; }
            set
            { 
                myPrintWin = value;
                this.RaisePropertyChanged("MyPrintWin");
            }
        }

        private Grid _myGrid;

        public Grid MyGrid
        {
            get { return _myGrid; }
            set 
            { 
                _myGrid = value;
                this.RaisePropertyChanged("MyGrid");
            }
        }
        #endregion

        #region ICommand
        public ICommand PrintBtnCommand { get; private set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="win"></param>
        /// <param name="sTempletName"></param>
        /// <param name="listUser">手工录入信息列表</param>
        public PrintTest(ReportPrintControl myWin,Grid gd)
        {
            FitResultXList = new ObservableCollection<FitResultX>();
            FitResultQList = new ObservableCollection<FitResultQ>();
            FitResultXPowerList = new ObservableCollection<FitResultX>();
            FitResultQPowerList = new ObservableCollection<FitResultQ>();
            FitResultList = new ObservableCollection<MyFitResult>();
            PrintBtnCommand = new PrintCommand(this);
            MyPrintWin = myWin;
            MyGrid = gd;
            BindInfomation(ModuleConstant.PatientId, ModuleConstant.FitDate, EvaluateActionEnum.StrengthProtrusive, Protrusive);
            BindInfomation(ModuleConstant.PatientId, ModuleConstant.FitDate, EvaluateActionEnum.StrengthBend, Bend);
            BindInfomation(ModuleConstant.PatientId, ModuleConstant.FitDate, EvaluateActionEnum.RotationStrengthLeft, RotationLeft);
            BindInfomation(ModuleConstant.PatientId, ModuleConstant.FitDate, EvaluateActionEnum.RotationStrengthRigth, RotationRigth);
            GetValue(ModuleConstant.PatientId, ModuleConstant.FitDate);
            ShowPrintContent();
        }
        #endregion

        #region 私有方法

        #region 绑定旋转Fit记录
        /// <summary>
        /// 绑定旋转Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultX(string patientId)
        {
            FitResultXList = new ObservableCollection<FitResultX>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));    //行号 0
            tbl.Columns.Add("Time", typeof(string));      //训练时间  1
            tbl.Columns.Add("LeftValue", typeof(string));     // 左 2 
            tbl.Columns.Add("RightValue", typeof(string));    // 右 3 
            tbl.Columns.Add("ModeId", typeof(string));      //模式名称 4
            int count = 0;

            var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.ModeID == Convert.ToInt32(FitModeEnum.RotationFit)
                        select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Max(b => b.CreateTime),
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };

            foreach (var item in temp)
            {
                count++;
                tbl.Rows.Add(count.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy-MM-dd"), item.MaxAngle, item.MinAngle, item.ModeID);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {
                FitResultXList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultX item = new FitResultX();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.LeftValue = dr[2].ToString();
                        item.RightValue = dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultXList.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 绑定曲伸Fit记录
        /// <summary>
        /// 绑定曲伸Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultQ(string patientId)
        {
            FitResultQList = new ObservableCollection<FitResultQ>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));        //行号 0
            tbl.Columns.Add("Time", typeof(string));         //训练时间  1
            tbl.Columns.Add("MaxtValue", typeof(string));     //最大值 2 
            tbl.Columns.Add("MinValue", typeof(string));      //最小 3 
            tbl.Columns.Add("ModeId", typeof(string));        //模式名称 4
            int count = 0;

            var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.ModeID == Convert.ToInt32(FitModeEnum.ProtrusiveOrBendFit)
                        select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Max(b => b.CreateTime),
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };

            foreach (var item in temp)
            {
                count++;
                tbl.Rows.Add(count.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy-MM-dd"), item.MaxAngle, item.MinAngle, item.ModeID);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {

                FitResultQList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultQ item = new FitResultQ();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.MaxValue = dr[2].ToString();
                        item.MinValue = dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultQList.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 绑定旋转力量Fit记录
        /// <summary>
        /// 绑定旋转Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultXPower(string patientId)
        {
            FitResultXList = new ObservableCollection<FitResultX>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));    //行号 0
            tbl.Columns.Add("Time", typeof(string));      //训练时间  1
            tbl.Columns.Add("LeftValue", typeof(string));     // 左 2 
            tbl.Columns.Add("RightValue", typeof(string));    // 右 3 
            tbl.Columns.Add("ModeId", typeof(string));      //模式名称 4
            int count = 0;

            var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.ModeID == Convert.ToInt32(FitModeEnum.RotationStrengthEvaluation)
                        select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Max(b => b.CreateTime),
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };

            foreach (var item in temp)
            {
                count++;
                tbl.Rows.Add(count.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy-MM-dd"), item.MaxAngle, item.MinAngle, item.ModeID);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {
                FitResultXList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultX item = new FitResultX();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.LeftValue = dr[2].ToString();
                        item.RightValue = dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultXList.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 绑定曲伸力量Fit记录
        /// <summary>
        /// 绑定曲伸Fit记录
        /// </summary>
        /// <param name="patientId"></param>
        public void BindFitResultQPower(string patientId)
        {
            FitResultQList = new ObservableCollection<FitResultQ>();
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));        //行号 0
            tbl.Columns.Add("Time", typeof(string));         //训练时间  1
            tbl.Columns.Add("MaxtValue", typeof(string));     //最大值 2 
            tbl.Columns.Add("MinValue", typeof(string));      //最小 3 
            tbl.Columns.Add("ModeId", typeof(string));        //模式名称 4
            int count = 0;

            var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId && od.ModeID == Convert.ToInt32(FitModeEnum.ProtrusiveOrBendStrengthEvaluation)
                        select od;
            var temp = from a in listX
                       group a by new
                       {
                           a.PatientID,
                           a.CreateTime,
                           a.MaxAngle,
                           a.MinAngle,
                           a.ModeID
                       } into p
                       select new
                       {
                           pid = p.Key.PatientID,
                           exerciseDate = p.Max(b => b.CreateTime),
                           p.Key.MaxAngle,
                           p.Key.MinAngle,
                           p.Key.ModeID
                       };

            foreach (var item in temp)
            {
                count++;
                tbl.Rows.Add(count.ToString(), Convert.ToDateTime(item.exerciseDate).ToString("yyyy-MM-dd"), item.MaxAngle, item.MinAngle, item.ModeID);
            }
            //按照日期查询数据库

            if (tbl.Rows.Count > 0)
            {

                FitResultQList.Clear();
                if (tbl.Rows.Count > 0)
                {
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        DataRow dr = tbl.Rows[i];
                        FitResultQ item = new FitResultQ();
                        item.DayIndex = dr[0].ToString();
                        item.DayTime = dr[1].ToString();
                        item.MaxValue = dr[2].ToString();
                        item.MinValue = dr[3].ToString();
                        item.ModeId = dr[4].ToString();
                        FitResultQList.Add(item);
                    }
                }
            }
        }
        #endregion

        #region 整合数据
        public void BindData(string patientId)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Index", typeof(string));    //行号 0
            tbl.Columns.Add("Time", typeof(string));      //训练时间  1
            tbl.Columns.Add("LeftValue", typeof(string));     // 左 2 
            tbl.Columns.Add("RightValue", typeof(string));    // 右 3 
            tbl.Columns.Add("MaxValue", typeof(string));     // 左 2 
            tbl.Columns.Add("MinValue", typeof(string));    // 右 3 
            tbl.Columns.Add("FrontValue", typeof(string));     // 左 2 
            tbl.Columns.Add("BehindValue", typeof(string));    // 右 3 
            tbl.Columns.Add("FrontPowerValue", typeof(string));     // 左 2 
            tbl.Columns.Add("BehindPowerValue", typeof(string));    // 右 3 
            tbl.Columns.Add("ModeId", typeof(string));      //模式名称 4

             var listX = from od in MySession.Query<FitRecord>()
                        where od.PatientID == patientId
                        select od;
             var maxTime = (from a in listX
                            group a by new
                            {
                                a.PatientID,
                                a.ExerciseDate

                            } into p
                            select new
                            {
                                Pid = p.Key.PatientID,
                                MaxDate =p.Max(b => b.CreateTime)
                            }).OrderBy(x => x.MaxDate);

            MyFitResult FitItem = null;

            foreach (var item in maxTime)
            {
               FitItem = new MyFitResult();
               FitItem.DayTime = item.MaxDate.ToString("yyyy-MM-dd");
               FitResultList.Add(FitItem);
            }

            foreach (MyFitResult item in FitResultList)
            {
                foreach (FitResultX itemX in FitResultXList)
                {
                    if (item.DayTime == itemX.DayTime)
                    {
                        item.LeftValue = itemX.LeftValue;
                        item.RightValue = itemX.RightValue;
                    }
                }
            }

            foreach (MyFitResult item in FitResultList)
            {
                foreach (FitResultQ itemQ in FitResultQList)
                {
                    if (item.DayTime == itemQ.DayTime)
                    {
                        item.FrontValue = itemQ.MinValue;
                        item.BehindValue = itemQ.MaxValue;
                    }
                }
            }

            foreach (MyFitResult item in FitResultList)
            {
                foreach (FitResultX itemXQ in FitResultXPowerList)
                {
                    if (item.DayTime == itemXQ.DayTime)
                    {
                        item.MinValue = itemXQ.LeftValue;
                        item.MaxValue = itemXQ.RightValue;
                    }
                }
            }

            foreach (MyFitResult item in FitResultList)
            {
                foreach (FitResultQ itemPQ in FitResultQPowerList)
                {
                    if (item.DayTime == itemPQ.DayTime)
                    {
                        item.FrontPowerValue = itemPQ.MinValue;
                        item.BehindPowerValue = itemPQ.MaxValue;
                    }
                }
            }
        }
        #endregion

        #region 获得范围评测的值
        public void GetValue(string patientId, DateTime Date)
        {
            var obj = (from od in MySession.Query<EvaluteDetail>()
                       where od.PatientID == patientId && od.EvaluteDetailDate > Date && od.EvaluteDetailDate < Date.AddDays(1)
                       select od).OrderByDescending(x => x.EvaluteDetailDate).ToList();

            if (obj != null && obj.Count() > 0)
            {
                var ProtrusiveValue = (from od in obj
                             where od.ActionId == Convert.ToInt32(EvaluateActionEnum.StrengthProtrusive)
                             select od).OrderByDescending(x => x.EvaluteDetailDate).Take(3).ToList();


                ProtrusiveList = new ObservableCollection<EvaluteDetail>();
                foreach (EvaluteDetail item in ProtrusiveValue)
                {
                    ProtrusiveList.Add(item);
                }

                var BendValue = (from od in obj
                                       where od.ActionId == Convert.ToInt32(EvaluateActionEnum.StrengthBend)
                                       select od).OrderByDescending(x => x.EvaluteDetailDate).Take(3).ToList();


                BendList = new ObservableCollection<EvaluteDetail>();
                foreach (EvaluteDetail item in BendValue)
                {
                    BendList.Add(item);
                }

                var RotationStrengthLeftValue = (from od in obj
                                       where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RotationStrengthLeft)
                                       select od).OrderByDescending(x => x.EvaluteDetailDate).Take(3).ToList();


                RotationLeftList = new ObservableCollection<EvaluteDetail>();
                foreach (EvaluteDetail item in RotationStrengthLeftValue)
                {
                    RotationLeftList.Add(item);
                }

                var RotationStrengthRigthValue = (from od in obj
                                       where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RotationStrengthRigth)
                                       select od).OrderByDescending(x => x.EvaluteDetailDate).Take(3).ToList();


                RotationRigthList = new ObservableCollection<EvaluteDetail>();
                foreach (EvaluteDetail item in RotationStrengthRigthValue)
                {
                    RotationRigthList.Add(item);
                }

                var value1 = (from od in obj
                              where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RangeProtrusive)
                              select od).OrderByDescending(x => x.EvaluteDetailDate).Take(1).ToList();
                if (value1 != null && value1.Count() > 0)
                {
                    EvaluteDetail item = value1.ToList()[0] as EvaluteDetail;
                    MaxProtrusive = Convert.ToDecimal(item.MaxV);
                }

                var value2 = (from od in obj
                              where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RangeBend)
                              select od).OrderByDescending(x => x.EvaluteDetailDate).Take(1).ToList();
                if (value2 != null && value2.Count() > 0)
                {
                    EvaluteDetail item1 = value2.ToList()[0] as EvaluteDetail;
                    MaxBend = Convert.ToDecimal(item1.MaxV);
                }

                var value3 = (from od in obj
                              where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RotationRangeLeft)
                              select od).OrderByDescending(x => x.EvaluteDetailDate).Take(1).ToList();
                if (value3 != null && value3.Count() > 0)
                {
                    EvaluteDetail item2 = value3.ToList()[0] as EvaluteDetail;
                    MaxRotationLeft = Convert.ToDecimal(item2.MaxV);
                }

                var value4 = (from od in obj
                              where od.ActionId == Convert.ToInt32(EvaluateActionEnum.RotationRangeRight)
                              select od).OrderByDescending(x => x.EvaluteDetailDate).Take(1).ToList();
                if (value4 != null && value4.Count() > 0)
                {
                    EvaluteDetail item3 = value4.ToList()[0] as EvaluteDetail;
                    MaxRotationRight = Convert.ToDecimal(item3.MaxV);
                }
            }
        }
        #endregion

        #region 绑定报表数据
        public void BindInfomation(string patientId, DateTime Date, EvaluateActionEnum ActionEnum, PrintOutputInfo Info)
        {
            var list = ((from od in MySession.Query<EvaluteDetail>()
                        where od.PatientID == patientId && od.ActionId == Convert.ToInt32(ActionEnum) && od.EvaluteDetailDate > Date && od.EvaluteDetailDate < Date.AddDays(1)
                        select od).OrderByDescending(x=>x.EvaluteDetailDate).Take(3).ToList());

            //Protrusive.
            int count = 0;
            float ExplosiveForceAVGSUM = 0;
            float EnduranceAVGSUM = 0;
            float LaborIndexAVGSUM = 0;

            if (list != null && list.Count() > 0)
            {
                count = list.Count();

                double[] ExplosiveForceNums = new double[count];
                double[] EnduranceNums = new double[count];
                double[] LaborIndexNums = new double[count];

                for (int i = 0; i < count; i++)
                {
                    if (i == 0)
                    {
                        Info.ExplosiveForceFirst = list.ToList()[i].MaxV.ToString(("#0.00"));
                        Info.EnduranceFirst = list.ToList()[i].LastValue.ToString(("#0.00"));
                        Info.LaborIndexFirst = ((list.ToList()[i].FatigueIndex) * 100).ToString(("#0.00"));
                        Info.IntervalValue = list.ToList()[i].Interval.ToString();

                    }
                    else if (i == 1)
                    {
                        Info.ExplosiveForceSecond = list.ToList()[i].MaxV.ToString(("#0.00"));
                        Info.EnduranceSecond = list.ToList()[i].LastValue.ToString(("#0.00"));
                        Info.LaborIndexSecond = ((list.ToList()[i].FatigueIndex) * 100).ToString(("#0.00"));
                    }
                    else if(i==2)
                    {
                        Info.ExplosiveForceThird = list.ToList()[i].MaxV.ToString(("#0.00"));
                        Info.EnduranceThird = list.ToList()[i].LastValue.ToString(("#0.00"));
                        Info.LaborIndexThird = ((list.ToList()[i].FatigueIndex) * 100).ToString(("#0.00"));
                    }

                    ExplosiveForceAVGSUM += list.ToList()[i].MaxV;
                    EnduranceAVGSUM += list.ToList()[i].LastValue;
                    LaborIndexAVGSUM += (list.ToList()[i].FatigueIndex) * 100;

                    ExplosiveForceNums[i] = list.ToList()[i].MaxV;
                    EnduranceNums[i] = list.ToList()[i].LastValue;
                    LaborIndexNums[i] = (list.ToList()[i].FatigueIndex) * 100;
                }

                #region 平均值

                Info.ExplosiveForceAVG = Convert.ToDecimal(ExplosiveForceAVGSUM / count).ToString(("#0.00"));
                Info.EnduranceAVG = Convert.ToDecimal(EnduranceAVGSUM / count).ToString(("#0.00"));
                Info.LaborIndexAVG = Convert.ToDecimal(LaborIndexAVGSUM / count).ToString(("#0.00"));

                #endregion

                #region 标准差
                Info.ExplosiveForceSD = GetSD(ExplosiveForceNums, Info.ExplosiveForceAVG, count).ToString();
                Info.EnduranceSD = GetSD(EnduranceNums, Info.EnduranceAVG, count).ToString();
                Info.LaborIndexSD = GetSD(LaborIndexNums, Info.LaborIndexAVG, count).ToString();
                #endregion

                #region 标准值
                
                #endregion
               

                #region 时间
                Info.FitTime = Convert.ToDateTime(list.ToList()[0].EvaluteDetailDate).ToString("yyyy-MM-dd");
                #endregion

            }

        }
        #endregion

        #endregion

        #region 计算标准差
        double GetSD(double[] nums,string avg,int count)
        {
            double variance = 0;
            for (int i = 0; i < count; i++)
            {
                variance += Math.Pow(nums[i] - Convert.ToDouble(avg), 2);
            }//求方差
            double sd = Math.Pow(variance, 0.5);//求标准差

            return Math.Round(sd,2);
        }
        #endregion

        #region 报告

        #region 加载打印
        public IDocumentPaginatorSource LoadPrint(System.Collections.Generic.List<FixedPage> pages, Size vsize)
        {
            FixedDocument fd = new FixedDocument();
            fd.DocumentPaginator.PageSize = vsize;
            foreach (var item in pages)
            {
                PageContent pc = new PageContent();
                ((IAddChild)pc).AddChild(item);
                pc.Height = vsize.Height;
                pc.Width = vsize.Width;
                fd.Pages.Add(pc);
            }
            MemoryStream ms = new MemoryStream();
            Package pkg = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
            string pack = "pack://report.xps";
            PackageStore.RemovePackage(new Uri(pack));
            PackageStore.AddPackage(new Uri(pack), pkg);
            XpsDocument doc = new XpsDocument(pkg, CompressionOption.NotCompressed, pack);
            XpsSerializationManager rsm = new XpsSerializationManager(new XpsPackagingPolicy(doc), false);
            rsm.SaveAsXaml(fd);
            return doc.GetFixedDocumentSequence() as IDocumentPaginatorSource;
        }
        #endregion

        #region Title赋值
        /// <summary>
        /// Title赋值
        /// </summary>
        /// <param name="c"></param>
        /// <param name="titleMsg"></param>
        private static void SetTitle(Chart c, string titleMsg)
        {
            // Create a new instance of Title
            Title title = new Title();

            // Set title property
            title.Text = titleMsg;

            // Add title to Titles collection
            c.Titles.Add(title);
        }
        #endregion

        #region  隐藏图表控件广告

        void chart_Rendered(object sender, EventArgs e)
        {
            var c = sender as Chart;
            var legend = c.Legends[0];
            var root = legend.Parent as Grid;

            root.Children.RemoveAt(8);
            root.Children.RemoveAt(7);
        }  

        void c_Rendered(object sender, EventArgs e)
        {
            var legend = (sender as Chart).Legends[0];
            var root = legend.Parent as Grid;
            if (root != null)
            {
                root.Children.RemoveAt((sender as Chart).Series.Count + 8);
                root.Children.RemoveAt((sender as Chart).Series.Count + 8);
                (sender as Chart).Legends[0].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                (sender as Chart).Legends[0].VerticalAlignment = System.Windows.VerticalAlignment.Center;
            }
        }
        #endregion

        #region 显示打印内容
        /// <summary>
        /// 显示打印内容
        /// </summary>
        private void ShowPrintContent()
        {
            int TotalPageCount = 2; //总页数
            int TotalCount = 0;     //记录总条数

           
            #region 加载页面内容
            

            #region 页面参数

            List<FixedPage> vpages = new List<FixedPage>();
            FixedPage curPage = null;

            Size pageSize = new Size();
            Thickness pageMargin = new Thickness();

            pageSize.Width = Objtoobj.objectToDouble("823");
            pageSize.Height = Objtoobj.objectToDouble("1120");
            pageMargin = Objtoobj.strToThicknewss("10");
            #endregion

            #region 页面
            for (int i = 0; i < TotalPageCount; i++)
            {
                FixedPage vpage = new FixedPage();
                vpage.RenderSize = pageSize;
                vpage.Measure(pageSize);
                vpage.Width = pageSize.Width;
                vpage.Height = pageSize.Height;
                vpages.Add(vpage);
                //curPage = vpage;
            }

            curPage=vpages[0];
            #endregion

            #region 拼接
          
            #region 标题
            TextBlock tbTitle = new TextBlock();
            tbTitle.Text = "北京市积水潭医院";

            tbTitle.Margin = new Thickness(320,10,0,0);
            tbTitle.FontSize = 20;
            tbTitle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            tbTitle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            tbTitle.TextAlignment = System.Windows.TextAlignment.Center;
            curPage.Children.Add(tbTitle);
            #endregion

            TextBlock vt = new TextBlock();
            vt.FontFamily = new FontFamily("宋体");
            Line line = new Line();
            int selectIndex = 0;
            foreach (FixedPage item in vpages)
            {
                selectIndex++;
                curPage = item;

                #region 页码
                vt = new TextBlock();
                vt.Text =  selectIndex + "/";
                vt.Margin = new Thickness(680, 1080, 0, 0);
                vt.FontSize = 12;
                vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                curPage.Children.Add(vt);

                vt = new TextBlock();
                vt.Text =  TotalPageCount.ToString() ;
                vt.Margin = new Thickness(695, 1080, 0, 0);
                vt.FontSize = 12;
                vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                curPage.Children.Add(vt);
                #endregion

                if (selectIndex == 1)
                {

                    #region 标题
                    vt = new TextBlock();
                    vt.Text = "康复医学科";
                    vt.Margin = new Thickness(350, 40, 0, 0);
                    vt.FontSize = 20;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region 患者信息

                    vt = new TextBlock();
                    vt.Text = "姓   名: " + ModuleConstant.Syspatient.UserName;
                    vt.Margin = new Thickness(45, 80, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (string.IsNullOrEmpty(ModuleConstant.Syspatient.Sex))
                    {
                        vt.Text = "性  别: 未输入";
                    }
                    else
                    {
                       string sex= ModuleConstant.Syspatient.Sex == "1" ? "男" : "女";
                       vt.Text = "性  别: " + sex;
                    }
                    vt.Margin = new Thickness(220, 80, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "出生日期: " + Convert.ToDateTime(ModuleConstant.Syspatient.BirthDay).ToString("yyyy-MM-dd");
                    vt.Margin = new Thickness(400, 80, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "病历号: " + ModuleConstant.Syspatient.PatientCarNo;
                    vt.Margin = new Thickness(45, 110, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "年  龄: "+(DateTime.Now.Year-Convert.ToDateTime( ModuleConstant.Syspatient.BirthDay).Year);
                    vt.Margin = new Thickness(220, 110, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "初步诊断: " +EnumHelper.GetEnumItemName(Convert.ToInt32(ModuleConstant.Syspatient.DiagnoseTypeId),typeof( DiagnoseTypeEnum));
                    vt.Margin = new Thickness(400, 115, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    if (ModuleConstant.Syspatient.BodyHeight.HasValue)
                    {
                        vt.Text = "身   高: " + ModuleConstant.Syspatient.BodyHeight;
                    }
                    else
                    {
                        vt.Text = "身   高: ";
                    }
                    vt.Margin = new Thickness(45, 140, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (ModuleConstant.Syspatient.Weight.HasValue)
                    {
                        vt.Text = "体  重: " + ModuleConstant.Syspatient.Weight;
                    }
                    else
                    {
                        vt.Text = "体  重: ";
                    }
                    vt.Margin = new Thickness(220, 140, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = "打印日期：" + DateTime.Now.ToString() ;
                    vt.Margin = new Thickness(580, 140, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    #endregion

                    #region 分割线
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 70;
                    line.Y2 = 70;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 1.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);
                    #endregion

                    #region 力量评定部分

                    vt = new TextBlock();
                    vt.Text = "一、力量评测部分";
                    vt.Margin = new Thickness(45, 180, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #region 表格一 前屈
                   
                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 230;
                    line.Y2 = 230;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 230;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 230;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 430;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 255;
                    line.Y2 = 255;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 280;
                    line.Y2 = 280;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 305;
                    line.Y2 = 305;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 330;
                    line.Y2 = 330;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 355;
                    line.Y2 = 355;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 380;
                    line.Y2 = 380;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                  
                    line = new Line();
                    line.X1 = 130;
                    line.X2 = 130;
                    line.Y1 = 230;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                  
                    line = new Line();
                    line.X1 = 280;
                    line.X2 = 280;
                    line.Y1 = 230;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 430;
                    line.X2 = 430;
                    line.Y1 = 230;
                    line.Y2 = 430;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 标题
                    vt = new TextBlock();
                    vt.Text = "前屈";
                    vt.Margin = new Thickness(45, 235, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(45, 260, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(45, 285, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(45, 310, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "平均值";
                    vt.Margin = new Thickness(45, 335, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准差";
                    vt.Margin = new Thickness(45, 360, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准值";
                    vt.Margin = new Thickness(45, 385, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "爆发力（最大值）";
                    vt.Margin = new Thickness(145, 235, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "耐力（最小值）";
                    vt.Margin = new Thickness(295, 235, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "疲劳指数/[（最大值-最小值）/最大值*100%]";
                    vt.Margin = new Thickness(440, 235, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion
                  
                    #region 数据
                    vt = new TextBlock();
                    vt.Text = "测试时间：" + Protrusive.FitTime;
                    vt.Margin = new Thickness(45, 205, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceFirst;
                    vt.Margin = new Thickness(145, 260, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceSecond;
                    vt.Margin = new Thickness(145, 285, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceThird;
                    vt.Margin = new Thickness(145, 310, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceAVG;
                    vt.Margin = new Thickness(145, 335, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceSD;
                    vt.Margin = new Thickness(145, 360, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.ExplosiveForceSV;
                    vt.Margin = new Thickness(145, 385, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceFirst;
                    vt.Margin = new Thickness(295, 260, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceSecond;
                    vt.Margin = new Thickness(295, 285, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceThird;
                    vt.Margin = new Thickness(295, 310, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceAVG;
                    vt.Margin = new Thickness(295, 335, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceSD;
                    vt.Margin = new Thickness(295, 360, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.EnduranceSV;
                    vt.Margin = new Thickness(295, 385, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexFirst;
                    vt.Margin = new Thickness(440, 260, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexSecond;
                    vt.Margin = new Thickness(440, 285, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexThird;
                    vt.Margin = new Thickness(440, 310, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexAVG;
                    vt.Margin = new Thickness(440, 335, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexSD;
                    vt.Margin = new Thickness(440, 360, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Protrusive.LaborIndexSV;
                    vt.Margin = new Thickness(440, 385, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #endregion

                    #region 表格二 后伸

                 
                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 650;
                    line.Y2 = 650;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 650;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 650;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 850;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 675;
                    line.Y2 = 675;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 700;
                    line.Y2 = 700;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 725;
                    line.Y2 = 725;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 750;
                    line.Y2 = 750;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 775;
                    line.Y2 = 775;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 800;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 130;
                    line.X2 = 130;
                    line.Y1 = 650;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 280;
                    line.X2 = 280;
                    line.Y1 = 650;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 430;
                    line.X2 = 430;
                    line.Y1 = 650;
                    line.Y2 = 850;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 标题
                    vt = new TextBlock();
                    vt.Text = "后伸";
                    vt.Margin = new Thickness(45, 655, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(45, 680, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(45, 705, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(45, 730, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "平均值";
                    vt.Margin = new Thickness(45, 755, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准差";
                    vt.Margin = new Thickness(45, 780, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准值";
                    vt.Margin = new Thickness(45, 805, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "爆发力（最大值）";
                    vt.Margin = new Thickness(145, 655, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "耐力（最小值）";
                    vt.Margin = new Thickness(295, 655, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "疲劳指数/[（最大值-最小值）/最大值*100%]";
                    vt.Margin = new Thickness(440, 655, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region 数据

                    vt = new TextBlock();
                    vt.Text = "测试时间：" + Bend.FitTime;
                    vt.Margin = new Thickness(45, 625, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceFirst;
                    vt.Margin = new Thickness(145, 680, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceSecond;
                    vt.Margin = new Thickness(145, 705, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceThird;
                    vt.Margin = new Thickness(145, 730, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceAVG;
                    vt.Margin = new Thickness(145, 755, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceSD;
                    vt.Margin = new Thickness(145, 780, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.ExplosiveForceSV;
                    vt.Margin = new Thickness(145, 805, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceFirst;
                    vt.Margin = new Thickness(295, 680, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceSecond;
                    vt.Margin = new Thickness(295, 705, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceThird;
                    vt.Margin = new Thickness(295, 730, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceAVG;
                    vt.Margin = new Thickness(295, 755, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceSD;
                    vt.Margin = new Thickness(295, 780, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.EnduranceSV;
                    vt.Margin = new Thickness(295, 805, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexFirst;
                    vt.Margin = new Thickness(440, 680, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexSecond;
                    vt.Margin = new Thickness(440, 705, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexThird;
                    vt.Margin = new Thickness(440, 730, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexAVG;
                    vt.Margin = new Thickness(440, 755, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexSD;
                    vt.Margin = new Thickness(440, 780, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = Bend.LaborIndexSV;
                    vt.Margin = new Thickness(440, 805, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #endregion

                    #region 图表

                    #region chart1
                    CustomChars chart1 = new CustomChars();
                    //chart1.Maxarc = 100;
                    chart1.Background = new SolidColorBrush(Colors.White);
                    chart1.ScaleColor = new SolidColorBrush(Colors.LightGray);
                    chart1.LineColor1 = ChartColor.brush[0];
                    chart1.LineColor2 = ChartColor.brush[1];
                    chart1.LineColor3 = ChartColor.brush[2];
                    chart1.Width = 630;
                    chart1.Height = 170;
                    chart1.Margin = new Thickness(120, 440, 0, 0);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 490;
                    line.Y2 = 490;
                    line.Stroke = ChartColor.brush[0];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "前屈";
                    vt.Margin = new Thickness(50, 465, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Inlines.Add(new Italic(new Run(Protrusive.IntervalValue + "s")));
                    vt.Margin = new Thickness(400, 608, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(750, 608, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("T")));  
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(105, 440, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("N")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(82, 482, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 510;
                    line.Y2 = 510;
                    line.Stroke = ChartColor.brush[1];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(82, 502, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 530;
                    line.Y2 = 530;
                    line.Stroke = ChartColor.brush[2];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(82, 522, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region chart2
                    CustomChars chart2 = new CustomChars();
                    //chart2.Maxarc = 500;
                    chart2.Background = new SolidColorBrush(Colors.White);
                    chart2.ScaleColor = new SolidColorBrush(Colors.LightGray);
                    chart2.LineColor1 = ChartColor.brush[0];
                    chart2.LineColor2 = ChartColor.brush[1];
                    chart2.LineColor3 = ChartColor.brush[2];
                    chart2.Width = 630;
                    chart2.Height = 170;
                    chart2.Margin = new Thickness(120, 860, 0, 0);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 910;
                    line.Y2 = 910;
                    line.Stroke = ChartColor.brush[0];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "后伸";
                    vt.Margin = new Thickness(50, 885, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Inlines.Add(new Italic(new Run(Bend.IntervalValue + "s")));
                    vt.Margin = new Thickness(400, 1028, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(750, 1028, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("T")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(105, 860, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("N")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(82, 902, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 930;
                    line.Y2 = 930;
                    line.Stroke = ChartColor.brush[1];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(82, 922, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 950;
                    line.Y2 = 950;
                    line.Stroke = ChartColor.brush[2];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(82, 942, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    #endregion

                    #endregion

                    #region 添加折线图

                    #region 前屈

                    if (ProtrusiveList.Count > 0)
                    {
                         List<double> point=null;
                         List<double> point2=null;
                         List<double> point3=null;
                         double maxValue = 0;
                        for (int c = 0; c < ProtrusiveList.Count; c++)
                        {
                            EvaluteDetail myLine = ProtrusiveList[c];
                            string[] ypoint = myLine.Record2.Split('|');
                            for (int i = 0; i < ypoint.Count(); i++)
                            {
                                maxValue = MavValue(decimal.Parse(ypoint[i]));
                            }
                            if (c == 0)
                            {
                                point = SetPint(point, ypoint);
                            }
                            else if (c == 1)
                            {
                                point2 = SetPint(point2, ypoint);
                            }
                            else
                            {
                                point3 = SetPint(point3, ypoint);
                            }
                        }
                        chart1.Maxarc = maxValue;
                        chart1.Point1 = point;
                        chart1.Point2 = point2;
                        chart1.Point3 = point3;
                        curPage.Children.Add(chart1);
                    }
                    #endregion

                    #region 后伸
                    if (BendList.Count > 0)
                    {
                        List<double> point = null;
                        List<double> point2 = null;
                        List<double> point3 = null;
                        double maxValue = 0;
                        for (int c = 0; c < BendList.Count; c++)
                        {
                            EvaluteDetail myLine = BendList[c];
                            string[] ypoint = myLine.Record2.Split('|');
                            for (int i = 0; i < ypoint.Count(); i++)
                            {
                                maxValue = MavValue(decimal.Parse(ypoint[i]));
                            }
                            if (c == 0)
                            {
                                point = SetPint(point, ypoint);
                            }
                            else if (c == 1)
                            {
                                point2 = SetPint(point2, ypoint);
                               
                            }
                            else
                            {
                                point3 = SetPint(point3, ypoint);
                            }
                        }
                        chart2.Maxarc = maxValue;
                        chart2.Point1 = point;
                        chart2.Point2 = point2;
                        chart2.Point3 = point3;
                        curPage.Children.Add(chart2);
                    }
                    #endregion

                    #endregion

                    #endregion

                }
                #region 其它

                else
                {
                    #region 表格三

                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 70;
                    line.Y2 = 70;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 70;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 70;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 270;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 95;
                    line.Y2 = 95;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 120;
                    line.Y2 = 120;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 145;
                    line.Y2 = 145;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 170;
                    line.Y2 = 170;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 195;
                    line.Y2 = 195;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 220;
                    line.Y2 = 220;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 245;
                    line.Y2 = 245;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 130;
                    line.X2 = 130;
                    line.Y1 = 70;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 280;
                    line.X2 = 280;
                    line.Y1 = 70;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 430;
                    line.X2 = 430;
                    line.Y1 = 70;
                    line.Y2 = 270;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 205;
                    line.X2 = 205;
                    line.Y1 = 95;
                    line.Y2 = 220;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 355;
                    line.X2 = 355;
                    line.Y1 = 95;
                    line.Y2 = 220;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 600;
                    line.X2 = 600;
                    line.Y1 = 95;
                    line.Y2 = 220;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 标题
                    vt = new TextBlock();
                    vt.Text = "旋转（左/右）";
                    vt.Margin = new Thickness(45, 75, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(45, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(45, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(45, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "平均值";
                    vt.Margin = new Thickness(45, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准差";
                    vt.Margin = new Thickness(45, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准值";
                    vt.Margin = new Thickness(45, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "双侧差异(%)";
                    vt.Margin = new Thickness(45, 250, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "爆发力（最大值）";
                    vt.Margin = new Thickness(145, 75, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "耐力（最小值）";
                    vt.Margin = new Thickness(295, 75, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "疲劳指数/[（最大值-最小值）/最大值*100%]";
                    vt.Margin = new Thickness(440, 75, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region 数据

                    #region 左
                  
                    vt = new TextBlock();
                    vt.Text = "测试时间："+RotationLeft.FitTime;
                    vt.Margin = new Thickness(45, 45, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceFirst;
                    vt.Margin = new Thickness(145, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceSecond;
                    vt.Margin = new Thickness(145, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceThird;
                    vt.Margin = new Thickness(145, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceAVG;
                    vt.Margin = new Thickness(145, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceSD;
                    vt.Margin = new Thickness(145, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.ExplosiveForceSV;
                    vt.Margin = new Thickness(145, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (Convert.ToDouble(RotationLeft.ExplosiveForceAVG) < Convert.ToDouble(RotationRigth.ExplosiveForceAVG))
                    {
                        vt.Text = (Convert.ToDouble(RotationLeft.ExplosiveForceAVG) / Convert.ToDouble(RotationRigth.ExplosiveForceAVG)*100).ToString("#0.00") ;
                    }
                    else
                    {
                        vt.Text = (Convert.ToDouble(RotationRigth.ExplosiveForceAVG) / Convert.ToDouble(RotationLeft.ExplosiveForceAVG)*100).ToString("#0.00") ;
                    }
                    vt.Margin = new Thickness(145, 250, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceFirst;
                    vt.Margin = new Thickness(295, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceSecond;
                    vt.Margin = new Thickness(295, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceThird;
                    vt.Margin = new Thickness(295, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceAVG;
                    vt.Margin = new Thickness(295, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceSD;
                    vt.Margin = new Thickness(295, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.EnduranceSV;
                    vt.Margin = new Thickness(295, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (Convert.ToDouble(RotationLeft.EnduranceAVG) < Convert.ToDouble(RotationRigth.EnduranceAVG))
                    {
                        vt.Text = (Convert.ToDouble(RotationLeft.EnduranceAVG) / Convert.ToDouble(RotationRigth.EnduranceAVG)*100).ToString("#0.00");
                    }
                    else
                    {
                        vt.Text = (Convert.ToDouble(RotationRigth.EnduranceAVG) / Convert.ToDouble(RotationLeft.EnduranceAVG)*100).ToString("#0.00");
                    }
                   
                    vt.Margin = new Thickness(295, 250, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexFirst;
                    vt.Margin = new Thickness(440, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexSecond;
                    vt.Margin = new Thickness(440, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexThird;
                    vt.Margin = new Thickness(440, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexAVG;
                    vt.Margin = new Thickness(440, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexSD;
                    vt.Margin = new Thickness(440, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationLeft.LaborIndexSV;
                    vt.Margin = new Thickness(440, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                 
                    #endregion

                    #region 右
                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceFirst;
                    vt.Margin = new Thickness(220, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceSecond;
                    vt.Margin = new Thickness(220, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceThird;
                    vt.Margin = new Thickness(220, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceAVG;
                    vt.Margin = new Thickness(220, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceSD;
                    vt.Margin = new Thickness(220, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.ExplosiveForceSV;
                    vt.Margin = new Thickness(220, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                  


                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceFirst;
                    vt.Margin = new Thickness(370, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceSecond;
                    vt.Margin = new Thickness(370, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceThird;
                    vt.Margin = new Thickness(370, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceAVG;
                    vt.Margin = new Thickness(370, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceSD;
                    vt.Margin = new Thickness(370, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.EnduranceSV;
                    vt.Margin = new Thickness(370, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                  

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexFirst;
                    vt.Margin = new Thickness(615, 100, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexSecond;
                    vt.Margin = new Thickness(615, 125, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexThird;
                    vt.Margin = new Thickness(615, 150, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexAVG;
                    vt.Margin = new Thickness(615, 175, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexSD;
                    vt.Margin = new Thickness(615, 200, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = RotationRigth.LaborIndexSV;
                    vt.Margin = new Thickness(615, 225, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                   
                    #endregion
                    #endregion

                    #region 图表

                    #region chart3
                   
                    CustomChars chart3 = new CustomChars();
                    //chart3.Maxarc = 500;
                    chart3.Background = new SolidColorBrush(Colors.White);
                    chart3.ScaleColor = new SolidColorBrush(Colors.LightGray);
                    chart3.LineColor1 = ChartColor.brush[0];
                    chart3.LineColor2 = ChartColor.brush[1];
                    chart3.LineColor3 = ChartColor.brush[2];
                    chart3.Width = 630;
                    chart3.Height = 130;
                    chart3.Margin = new Thickness(120, 280, 0, 0);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 330;
                    line.Y2 = 330;
                    line.Stroke = ChartColor.brush[0];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "旋转左";
                    vt.Margin = new Thickness(50, 305, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Inlines.Add(new Italic(new Run(RotationLeft.IntervalValue + "s")));
                    vt.Margin = new Thickness(400, 408, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(750, 408, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("T")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(105, 280, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("N")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(82, 322, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 350;
                    line.Y2 = 350;
                    line.Stroke = ChartColor.brush[1];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(82, 342, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 370;
                    line.Y2 = 370;
                    line.Stroke = ChartColor.brush[2];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(82, 362, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region chart4

                    CustomChars chart4 = new CustomChars();
                    //chart4.Maxarc = 500;
                    chart4.Background = new SolidColorBrush(Colors.White);
                    chart4.ScaleColor = new SolidColorBrush(Colors.LightGray);
                    chart4.LineColor1 = ChartColor.brush[0];
                    chart4.LineColor2 = ChartColor.brush[1];
                    chart4.LineColor3 = ChartColor.brush[2];
                    chart4.Width = 630;
                    chart4.Height = 130;
                    chart4.Margin = new Thickness(120, 430, 0, 0);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 480;
                    line.Y2 = 480;
                    line.Stroke = ChartColor.brush[0];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "旋转右";
                    vt.Margin = new Thickness(50, 455, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                     vt = new TextBlock();
                    vt.Inlines.Add(new Italic(new Run(RotationLeft.IntervalValue + "s")));
                    vt.Margin = new Thickness(400, 558, 0, 0);
                    vt.FontSize = 14;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(750, 558, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("T")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Margin = new Thickness(105, 430, 0, 0);
                    vt.FontSize = 14;
                    vt.Inlines.Add(new Italic(new Run("N")));
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "测试1";
                    vt.Margin = new Thickness(82, 472, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 500;
                    line.Y2 = 500;
                    line.Stroke = ChartColor.brush[1];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试2";
                    vt.Margin = new Thickness(82, 492, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    line = new Line();
                    line.X1 = 35;
                    line.X2 = 80;
                    line.Y1 = 520;
                    line.Y2 = 520;
                    line.Stroke = ChartColor.brush[2];
                    line.StrokeThickness = 2.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    vt = new TextBlock();
                    vt.Text = "测试3";
                    vt.Margin = new Thickness(82, 512, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion
                    #endregion

                    #region 添加折线图

                    #region 旋转力量左
                    if (RotationLeftList.Count > 0)
                    {
                        List<double> point = null;
                        List<double> point2 = null;
                        List<double> point3 = null;
                        double maxValue=0;

                        for (int c = 0; c < RotationLeftList.Count; c++)
                        {
                            EvaluteDetail myLine = RotationLeftList[c];
                            string[] ypoint = myLine.Record2.Split('|');

                            
                            for (int i = 0; i < ypoint.Count(); i++)
                            {
                                maxValue = MavValue(decimal.Parse(ypoint[i]));
                            }
                           
                            if (c == 0)
                            {
                                point = SetPint(point, ypoint);
                            }
                            else if (c == 1)
                            {
                                point2 = SetPint(point2, ypoint);
                            }
                            else
                            {
                                point3 = SetPint(point3, ypoint);
                            }
                        }
                        chart3.Maxarc = maxValue;
                        chart3.Point1 = point;
                        chart3.Point2 = point2;
                        chart3.Point3 = point3;
                        curPage.Children.Add(chart3);
                    }
                    #endregion

                    #region 旋转力量右
                    if (RotationRigthList.Count > 0)
                    {
                        List<double> point = null;
                        List<double> point2 = null;
                        List<double> point3 = null;
                        double maxValue = 0;
                        for (int c = 0; c < RotationRigthList.Count; c++)
                        {
                            EvaluteDetail myLine = RotationRigthList[c];
                            string[] ypoint = myLine.Record2.Split('|');
                            for (int i = 0; i < ypoint.Count(); i++)
                            {
                                maxValue = MavValue(decimal.Parse(ypoint[i]));
                            }
                            if (c == 0)
                            {
                                point = SetPint(point, ypoint);

                            }
                            else if (c == 1)
                            {
                                point2 = SetPint(point2, ypoint);

                            }
                            else
                            {
                                point3 = SetPint(point3, ypoint);
                            }
                        }
                        chart4.Maxarc = maxValue;
                        chart4.Point1 = point;
                        chart4.Point2 = point2;
                        chart4.Point3 = point3;
                        curPage.Children.Add(chart4);
                    }
                    #endregion

                    #endregion

                    #endregion

                    #region 关节活动度
                    vt = new TextBlock();
                    vt.Text = "二、关节活动度";
                    vt.Margin = new Thickness(45, 580, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    #region 表格四

                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 600;
                    line.Y2 = 600;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 600;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 600;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 800;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 450;
                    line.Y1 = 650;
                    line.Y2 = 650;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 450;
                    line.Y1 = 700;
                    line.Y2 = 700;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 170;
                    line.X2 = 170;
                    line.Y1 = 600;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 310;
                    line.X2 = 310;
                    line.Y1 = 600;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 450;
                    line.X2 = 450;
                    line.Y1 = 600;
                    line.Y2 = 800;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 标题
                    vt = new TextBlock();
                    vt.Text = "最大活动范围";
                    vt.Margin = new Thickness(45, 625, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = "实际值";
                    vt.Margin = new Thickness(45, 665, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准值";
                    vt.Margin = new Thickness(45, 715, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = "前屈";
                    vt.Margin = new Thickness(220, 625, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "后伸";
                    vt.Margin = new Thickness(360, 625, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    #endregion

                    #region 图表

                    MyChart chart5 = new MyChart();
                    chart5.DataPointWidth = 12;
                    chart5.AnimationEnabled = false;
                    //chart5 = GetPng(chart5, RenderAs.Column);
                    chart5.Width = 300;
                    chart5.Height = 180;
                   
                    Axis yAxis = new Axis();
                    yAxis.Title = "";
                    chart5.AxesY.Add(yAxis);

                    Axis xaxis5 = new Axis();
                    AxisLabels xal5 = new AxisLabels
                    {
                        Enabled = true,
                        Angle = -45
                    };
                    xaxis5.AxisLabels = xal5;
                    //xaxis5.Enabled = false;
                    chart5.AxesX.Add(xaxis5);
                    SetTitle(chart5, "");

                    DataSeries dataSeries5 = new DataSeries();
                    dataSeries5.RenderAs = RenderAs.Column;
                    if (MaxProtrusive.HasValue)
                    {
                        dataSeries5.DataPoints.Add(new DataPoint
                        {
                            AxisXLabel = "前屈",
                            YValue = Convert.ToDouble(MaxProtrusive.Value)
                        });
                    }
                    if (MaxBend.HasValue)
                    {
                        dataSeries5.DataPoints.Add(new DataPoint
                        {
                            AxisXLabel = "后伸",
                            YValue = Convert.ToDouble(MaxBend.Value)
                        });
                    }

                    chart5.Rendered += new EventHandler(c_Rendered);
                    chart5.Margin = new Thickness(460, 610, 0, 0);
                    chart5.Series.Add(dataSeries5);
                    curPage.Children.Add(chart5);
                    #endregion

                    #region 数据

                    vt = new TextBlock();
                    if (MaxProtrusive.HasValue)
                    {
                        vt.Text = MaxProtrusive.Value.ToString();
                    }
                    vt.Margin = new Thickness(220, 665, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "";//标准值
                    vt.Margin = new Thickness(220, 715, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    if (MaxBend.HasValue)
                    {
                        vt.Text = MaxBend.Value.ToString();
                    }
                    vt.Margin = new Thickness(360, 665, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "";
                    vt.Margin = new Thickness(360, 715, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #endregion

                    #region 表格五

                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 820;
                    line.Y2 = 820;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 820;
                    line.Y2 = 1070;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 820;
                    line.Y2 = 1070;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 1070;
                    line.Y2 = 1070;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 450;
                    line.Y1 = 900;
                    line.Y2 = 900;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 450;
                    line.Y1 = 935;
                    line.Y2 = 935;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 450;
                    line.Y1 = 1035;
                    line.Y2 = 1035;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 170;
                    line.X2 = 450;
                    line.Y1 = 860;
                    line.Y2 = 860;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    line = new Line();
                    line.X1 = 170;
                    line.X2 = 170;
                    line.Y1 = 820;
                    line.Y2 = 1070;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 310;
                    line.X2 = 310;
                    line.Y1 = 860;
                    line.Y2 = 1035;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    line = new Line();
                    line.X1 = 450;
                    line.X2 = 450;
                    line.Y1 = 820;
                    line.Y2 = 1070;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);


                    #endregion

                    #region 标题

                    vt = new TextBlock();
                    vt.Text = "最大活动范围";
                    vt.Margin = new Thickness(45, 855, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "实际值";
                    vt.Margin = new Thickness(45, 915, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "标准值";
                    vt.Margin = new Thickness(45, 945, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "双侧对比（%）";
                    vt.Margin = new Thickness(45, 1045, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                    vt.Text = "旋转";
                    vt.Margin = new Thickness(300, 835, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "左";
                    vt.Margin = new Thickness(230, 870, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "右";
                    vt.Margin = new Thickness(370, 870, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #region 图表

                    MyChart chart6 = new MyChart();
                    //chart6 = GetPng(chart6, RenderAs.Column);
                    chart6.DataPointWidth = 12;
                    chart6.AnimationEnabled = false;
                    chart6.Width = 300;
                    chart6.Height = 210;
                    Axis xaxis6 = new Axis();
                    AxisLabels xal6 = new AxisLabels
                    {
                        Enabled = true,
                        Angle = -45
                    };
                    xaxis6.AxisLabels = xal6;
                    //xaxis6.Enabled = false;
                    chart6.AxesX.Add(xaxis6);
                    SetTitle(chart6, "");

                    DataSeries dataSeries = new DataSeries();
                    dataSeries.RenderAs = RenderAs.Column;
                    if (MaxRotationLeft.HasValue)
                    {
                        dataSeries.DataPoints.Add(new DataPoint
                        {
                            AxisXLabel = "左",
                            YValue =Convert.ToDouble(MaxRotationLeft.Value)
                        });
                    }
                    if (MaxRotationRight.HasValue)
                    {
                        dataSeries.DataPoints.Add(new DataPoint
                        {
                            AxisXLabel = "右",
                            YValue = Convert.ToDouble(MaxRotationRight.Value)
                        });
                    }

                    chart6.Rendered += new EventHandler(c_Rendered);
                    chart6.Margin =new Thickness(460, 840, 0, 0);
                    chart6.Series.Add(dataSeries);
                    curPage.Children.Add(chart6);
                
                    #endregion

                    #region 数据
                    vt = new TextBlock();
                    if (MaxRotationLeft.HasValue)
                    {
                        vt.Text = MaxRotationLeft.Value.ToString();
                    }
                    vt.Margin = new Thickness(230, 915, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (MaxRotationRight.HasValue)
                    {
                        vt.Text = MaxRotationRight.Value.ToString();
                    }
                    vt.Margin = new Thickness(370, 915, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);


                    vt = new TextBlock();
                   
                    vt.Margin = new Thickness(230, 1045, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if (!MaxRotationRight.HasValue || !MaxRotationLeft.HasValue)
                    {
                        vt.Text = "0";
                    }
                    else
                    {
                        if (MaxRotationRight.Value > MaxRotationLeft.Value)
                        {
                            vt.Text = Math.Round((MaxRotationLeft.Value/MaxRotationRight.Value)*100,2).ToString();
                        }
                        else
                        {
                            vt.Text = Math.Round((MaxRotationRight.Value / MaxRotationLeft.Value)*100, 2).ToString();
                        }
                    }
                    
                    vt.Margin = new Thickness(230, 1045, 0, 0);
                    vt.FontSize = 12;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);
                    #endregion

                    #endregion

                    #endregion
                }
            }
            #endregion
            #endregion

            #endregion
            mydocument = LoadPrint(vpages, pageSize);
        }

        private static List<double> SetPint(List<double> point, string[] ypoint)
        {
            point = new List<double>();
            if (ypoint != null && ypoint.Count() > 0)
            {
                if (ypoint != null && ypoint.Count() > 0)
                {
                    if (ypoint.Count() > 0 && ypoint.Count() < 600)
                    {

                        for (int i = 0; i < ypoint.Count(); i++)
                        {
                            point.Add(Convert.ToDouble(ypoint[i]));
                        }
                    }
                    else if (ypoint.Count() > 600 && ypoint.Count() < 900)
                    {

                        for (int i = 0; i < ypoint.Count(); i++)
                        {
                            if (i % 3 != 0)
                            {
                                point.Add(Convert.ToDouble(ypoint[i]));
                            }
                        }
                    }
                    else if (ypoint.Count() > 900 && ypoint.Count() < 1200)
                    {
                        for (int i = 0; i < ypoint.Count(); i++)
                        {
                            if (i % 2 != 0)
                            {
                                point.Add(Convert.ToDouble(ypoint[i]));
                            }
                        }
                    }
                    else if (ypoint.Count() > 1200 && ypoint.Count() < 1500)
                    {
                        for (int i = 0; i < ypoint.Count(); i++)
                        {
                            if (i % 1.5 != 0)
                            {
                                point.Add(Convert.ToDouble(ypoint[i]));
                            }
                        }
                    }
                    
                }
            }
            return point;
        }
        #endregion

        #endregion

        #region 获得图片
        
       
        public MyChart GetPng(MyChart chart,RenderAs mode)
        {
            // 新建一个 ColorSet 实例
            ColorSet cs = new ColorSet();
            cs.Id = "colorset1"; // 设置ColorSet 的 Id 为 colorset1
            cs.Brushes.Add(new SolidColorBrush(Colors.Green));
            cs.Brushes.Add(new SolidColorBrush(Colors.Red));
            cs.Brushes.Add(new SolidColorBrush(Colors.Blue));
            cs.Brushes.Add(new SolidColorBrush(Colors.Yellow));
            cs.Brushes.Add(new SolidColorBrush(Colors.Orange));

            chart.ColorSets.Add(cs);
            chart.ColorSet = "colorset1";  // 设置 Chart 使用自定义的颜色集合 colorset1
            chart.AnimationEnabled = false;
            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = mode;
            dataSeries.DataPoints.Add(new DataPoint
            {
                YValue = 20
            });
            dataSeries.DataPoints.Add(new DataPoint
            {
                YValue = 40
            });
          

            chart.Series.Add(dataSeries);
            return chart;

        }
        #endregion

        #region ExportToPng
        /// <summary>
        /// ExportToPng
        /// </summary>
        /// <param name="path"></param>
        /// <param name="surface"></param>
        public  void ExportToPng(Uri path, Visifire.Charts.Chart surface)
        {
            if (path == null) return;
            //Save current canvas transform 保存当前画布变换
            Transform transform = surface.LayoutTransform;
            //reset current transform (in case it is scaled or rotated) 重设当前画布（如果缩放或旋转）
            surface.LayoutTransform = null;
            //Create a render bitmap and push the surface to it 创建一个渲染位图和表面
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)surface.Width,
                (int)surface.Height,
                96d, 96d,
                PixelFormats.Pbgra32);
            renderBitmap.Render(surface);
            // Create a file stream for saving image
            using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
            {
                //Use png encoder for our data
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                // push the rendered bitmap to it
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                // save the data to the stream
                encoder.Save(outStream);
            }
            // Restore previously saved layout 恢复以前保存布局
            surface.LayoutTransform = transform;
        }
        #endregion

        #region 创建文件夹
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">路劲</param>
        public  void CreateDirectory(string sPath)
        {
            if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath);
            }
        }
        #endregion

        #region 获得最大值
         decimal tempValue = 0;
         public double MavValue(decimal value)
         {
             double showMaxValue = 0;
             value = Math.Abs(value);
             if (tempValue < value)
             {
                 tempValue = value;
             }

             if (tempValue % 2 == 0)
             {
                 showMaxValue = Convert.ToDouble(2 * Convert.ToInt32((tempValue / 2)));
             }
             else
             {
                 showMaxValue = Convert.ToDouble(2 * Convert.ToInt32(((tempValue / 2) + 1)));
             }
             return  showMaxValue;
         }
        #endregion

    }
}
