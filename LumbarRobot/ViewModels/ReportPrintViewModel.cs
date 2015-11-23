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

///**********************************
///打印模块
///**********************************
namespace LumbarRobot.ViewModels
{
    public class ReportPrintViewModel : ViewModelBase
    {

        #region 变量
        private int nColWidth = 120;
        private int nRowHeight = 40;
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
        public ReportPrintViewModel(ReportPrintControl myWin)
        {
            FitResultXList = new ObservableCollection<FitResultX>();
            FitResultQList = new ObservableCollection<FitResultQ>();
            FitResultXPowerList = new ObservableCollection<FitResultX>();
            FitResultQPowerList = new ObservableCollection<FitResultQ>();
            FitResultList = new ObservableCollection<MyFitResult>();
            //PrintBtnCommand = new PrintCommand(this);
            MyPrintWin = myWin;
            BindFitResultX(ModuleConstant.PatientId);
            BindFitResultQ(ModuleConstant.PatientId);
            BindData(ModuleConstant.PatientId);
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

        #region 显示打印内容
        /// <summary>
        /// 显示打印内容
        /// </summary>
        private void ShowPrintContent()
        {
            int TotalPageCount = 0; //总页数
            int TotalCount = 0;     //记录总条数

            if (FitResultList.Count > 0)
            {
                TotalCount = FitResultList.Count;
                int indexTemp = 0;
                if (TotalCount > 4)
                {
                    indexTemp = TotalCount - 4;
                    if (indexTemp % 5 == 0)
                    {
                        TotalPageCount = indexTemp / 5 + 1;
                    }
                    else
                    {
                        TotalPageCount = indexTemp / 5 + 2;
                    }
                }
            }
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
          
            double nleft = 20;
            double ntop = 200;

            #region 标题
            TextBlock tbTitle = new TextBlock();
            tbTitle.Text = "评测报告";

            tbTitle.Margin = new Thickness(330,10,0,0);
            tbTitle.FontSize = 34;
            tbTitle.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            tbTitle.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            tbTitle.TextAlignment = System.Windows.TextAlignment.Center;
            curPage.Children.Add(tbTitle);
            #endregion

            TextBlock vt = new TextBlock();
            Line line = new Line();
            int selectIndex = 0;
            foreach (FixedPage item in vpages)
            {
                selectIndex++;
                curPage = item;

                #region 页码
                vt = new TextBlock();
                vt.Text = "第" + selectIndex + "页";
                vt.Margin = new Thickness(630, 1080, 0, 0);
                vt.FontSize = 20;
                vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                curPage.Children.Add(vt);

                vt = new TextBlock();
                vt.Text = "共" + TotalPageCount + "页";
                vt.Margin = new Thickness(695, 1080, 0, 0);
                vt.FontSize = 20;
                vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                curPage.Children.Add(vt);
                #endregion

                if (selectIndex == 1)
                {
                    #region 患者信息

                    vt = new TextBlock();
                    vt.Text = "姓名：" + ModuleConstant.Syspatient.UserName;
                    vt.Margin = new Thickness(45, 80, 0, 0);
                    vt.FontSize = 20;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    if(string.IsNullOrEmpty(ModuleConstant.Syspatient.Sex))
                    {
                    vt.Text = "性别:未输入";
                    }
                    else
                    {
                        vt.Text = "性别:" + ModuleConstant.Syspatient.Sex=="1"?"男":"女";
                    }
                    vt.Margin = new Thickness(45, 115, 0, 0);
                    vt.FontSize = 20;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "年龄:"+(DateTime.Now.Year-Convert.ToDateTime( ModuleConstant.Syspatient.BirthDay).Year);
                    vt.Margin = new Thickness(220, 80, 0, 0);
                    vt.FontSize = 20;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "病历号:" + ModuleConstant.Syspatient.PatientCarNo;
                    vt.Margin = new Thickness(220, 115, 0, 0);
                    vt.FontSize = 20;
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    curPage.Children.Add(vt);

                    vt = new TextBlock();
                    vt.Text = "打印日期：" + DateTime.Now.ToString("yyyy-MM-dd") ;
                    vt.Margin = new Thickness(560, 215, 0, 0);
                    vt.FontSize = 20;
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
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //列表与病人信息分割线
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 250;
                    line.Y2 = 250;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);
                    #endregion

                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 260;
                    line.Y2 = 260;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 260;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 260;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 1060;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线
                    //第一格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 460;
                    line.Y2 = 460;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //第二格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 660;
                    line.Y2 = 660;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //第三格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 860;
                    line.Y2 = 860;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //日期分割线
                    line = new Line();
                    line.X1 = 150;
                    line.X2 = 150;
                    line.Y1 = 260;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //类别分割线
                    line = new Line();
                    line.X1 = 460;
                    line.X2 = 460;
                    line.Y1 = 260;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);
                    #endregion

                    #region 循环添加数据
                    //检测报告共有多少数量
                    int showCount = 0;
                    if (TotalCount > 4)
                    {
                        showCount = 4;
                    }
                    else
                    {
                        showCount = TotalCount;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                           #region 子项分割线
                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 300 + ntop * i;
                        line.Y2 = 300 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 340 + ntop * i;
                        line.Y2 = 340 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 380 + ntop * i;
                        line.Y2 = 380 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 420 + ntop * i;
                        line.Y2 = 420 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 300;
                        line.X2 = 300;
                        line.Y1 = 300 + ntop * i;
                        line.Y2 = 460 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 620;
                        line.X2 = 620;
                        line.Y1 = 300 + ntop * i;
                        line.Y2 = 460 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        #endregion
                    }
                    for (int i = 0; i < showCount; i++)
                    {
                     MyFitResult result=FitResultList[i];
                        #region 日期

                        vt = new TextBlock();
                        vt.Text =Convert.ToDateTime( FitResultList[i].DayTime).ToString("yyyy-MM-dd");// "2015-4-16";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(25, 355 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 标题

                        #region 屈伸
                        vt = new TextBlock();
                        vt.Text = "屈伸";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(240, 268 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 旋转
                        vt = new TextBlock();
                        vt.Text = "旋转";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(560, 268 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈
                        vt = new TextBlock();
                        vt.Text = "前屈";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 305 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈值
                        vt = new TextBlock();
                        vt.Text = result.FrontValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 345 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸
                        vt = new TextBlock();
                        vt.Text = "后伸";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 305 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸值
                        vt = new TextBlock();
                        vt.Text = result.BehindValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 345 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈力量
                        vt = new TextBlock();
                        vt.Text = "前屈力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 385 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈力量值
                        vt = new TextBlock();
                        vt.Text = result.FrontPowerValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 425 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸力量
                        vt = new TextBlock();
                        vt.Text = "后伸力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 385 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸力量值
                        vt = new TextBlock();
                        vt.Text = result.BehindPowerValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 425 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左
                        vt = new TextBlock();
                        vt.Text = "左";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 305 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左值
                        vt = new TextBlock();
                        vt.Text = result.LeftValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 350 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右
                        vt = new TextBlock();
                        vt.Text = "右";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 305 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右值
                        vt = new TextBlock();
                        vt.Text = result.RightValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 350 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左力量
                        vt = new TextBlock();
                        vt.Text = "左力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 385 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = 36;

                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左力量值
                        vt = new TextBlock();
                        vt.Text = result.MaxValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 425 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右力量
                        vt = new TextBlock();
                        vt.Text = "右力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 385 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;

                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右力量值
                        vt = new TextBlock();
                        vt.Text = result.RightValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 425 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #endregion
                    }
                    #endregion

                }
                #region 其它
               
                else
                {
                    #region 显示框
                    //上边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 60;
                    line.Y2 = 60;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //左边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 30;
                    line.Y1 = 60;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //右边框
                    line = new Line();
                    line.X1 = 770;
                    line.X2 = 770;
                    line.Y1 = 60;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //下边框
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 1060;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    #endregion

                    #region 表格线
                    //第一格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 260;
                    line.Y2 = 260;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //第2格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 460;
                    line.Y2 = 460;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //第3格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 660;
                    line.Y2 = 660;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //第4格
                    line = new Line();
                    line.X1 = 30;
                    line.X2 = 770;
                    line.Y1 = 860;
                    line.Y2 = 860;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //日期分割线
                    line = new Line();
                    line.X1 = 150;
                    line.X2 = 150;
                    line.Y1 = 60;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);

                    //类别分割线
                    line = new Line();
                    line.X1 = 460;
                    line.X2 = 460;
                    line.Y1 = 60;
                    line.Y2 = 1060;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    curPage.Children.Add(line);
                    #endregion

                    #region 循环添加数据

                    #region 判断数据
                    int showCount = 0;
                    int startIndex = 0;

                    if (5 * (selectIndex - 1) > TotalCount - 4)
                    {
                        showCount = (TotalCount - 4) % 5;
                    }
                    else
                    {
                        showCount = 5;
                    }
                    
                    startIndex = 5 * (selectIndex - 2) + 4;

                    for (int i = 0; i < 5; i++)
                    {
                        #region 子项分割线
                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 100 + ntop * i;
                        line.Y2 = 100 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 140 + ntop * i;
                        line.Y2 = 140 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 180 + ntop * i;
                        line.Y2 = 180 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 150;
                        line.X2 = 770;
                        line.Y1 = 220 + ntop * i;
                        line.Y2 = 220 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 300;
                        line.X2 = 300;
                        line.Y1 = 100 + ntop * i;
                        line.Y2 = 260 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        line = new Line();
                        line.X1 = 620;
                        line.X2 = 620;
                        line.Y1 = 100 + ntop * i;
                        line.Y2 = 260 + ntop * i;
                        line.Stroke = Brushes.Black;
                        line.StrokeThickness = 0.5;
                        line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        line.Margin = new System.Windows.Thickness(0.5);
                        curPage.Children.Add(line);

                        #endregion
                    }

                    #endregion
                    //检测报告共有多少数量
                    for (int i = 0; i < showCount; i++)
                    {
                        MyFitResult result = FitResultList[startIndex+i];
                        #region 日期

                        vt = new TextBlock();
                        vt.Text = Convert.ToDateTime(result.DayTime).ToString("yyyy-MM-dd");// "2015-4-16";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(25, 155 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 标题

                        #region 屈伸
                        vt = new TextBlock();
                        vt.Text = "屈伸";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(240, 68 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 旋转
                        vt = new TextBlock();
                        vt.Text = "旋转";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(560, 68 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈
                        vt = new TextBlock();
                        vt.Text = "前屈";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 105 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈值
                        vt = new TextBlock();
                        vt.Text = result.FrontValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 145 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸
                        vt = new TextBlock();
                        vt.Text = "后伸";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 105 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸值
                        vt = new TextBlock();
                        vt.Text = result.BehindValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 145 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈力量
                        vt = new TextBlock();
                        vt.Text = "前屈力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 185 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 前屈力量值
                        vt = new TextBlock();
                        vt.Text = result.FrontPowerValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(165, 225 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸力量
                        vt = new TextBlock();
                        vt.Text = "后伸力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 185 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 后伸力量值
                        vt = new TextBlock();
                        vt.Text = result.BehindPowerValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(317, 225 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左
                        vt = new TextBlock();
                        vt.Text = "左";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 105 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左值
                        vt = new TextBlock();
                        vt.Text = result.LeftValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 150 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右
                        vt = new TextBlock();
                        vt.Text = "右";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 105 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右值
                        vt = new TextBlock();
                        vt.Text = result.RightValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 150 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左力量
                        vt = new TextBlock();
                        vt.Text = "左力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 185 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 左力量值
                        vt = new TextBlock();
                        vt.Text = result.MaxValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(485, 225 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右力量
                        vt = new TextBlock();
                        vt.Text = "右力量";
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 185 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion

                        #region 右力量值
                        vt = new TextBlock();
                        vt.Text = result.RightValue;
                        vt.FontSize = 20;
                        vt.Margin = new System.Windows.Thickness(631, 225 + ntop * i, 0, 0);
                        vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        vt.Width = nColWidth;
                        vt.Height = nRowHeight;
                        vt.TextAlignment = System.Windows.TextAlignment.Center;
                        curPage.Children.Add(vt);
                        #endregion
                        #endregion
                    }
                    #endregion 
                }
            }
            #endregion
            #endregion

            #endregion
            mydocument = LoadPrint(vpages, pageSize);
        }
        #endregion

        #endregion
        
    }
}
