using Microsoft.Win32;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LumbarRobot.Common
{
    /// <summary>
    /// ReportTextControl.xaml 的交互逻辑
    /// </summary>
    public partial class ReportTextControl : UserControl
    {
        #region 定义变量
        #endregion

        #region 声明属性

        private ReportPropInfo _myProp;
        /// <summary>
        /// 属性
        /// </summary>
        public ReportPropInfo myProp
        {
            get { return _myProp; }
            set
            {
                _myProp = value;
                // setFrame();
            }
        }
        /// <summary>
        /// 是否设计模式
        /// </summary>
        public bool bDesign { get; set; }

        public delegate void updateHeaderFooter();
        public updateHeaderFooter updateHF;
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nType">类型 0表头 1表尾 2多行文本</param>
        /// <param name="vlist"></param>
        public ReportTextControl(ReportPropInfo vProp)
            : base()
        {
            InitializeComponent();
            this.Background = Brushes.White;
            bDesign = true;
            myProp = vProp;
            if (vProp == null)
            {
                return;
            }
            
            myProp.CreateReportChild(GridText, this);
            Height = myProp.ControlHeight;
            Width = myProp.ControlWidth;
            Margin = new Thickness(myProp.ControlLeft, myProp.ControlTop, 0, 0);
           
        }
        #endregion

        #region 窗体事件
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LeftFrame.X1 = 0;
            LeftFrame.X2 = 0;
            LeftFrame.Y1 = 0;
            LeftFrame.Y2 = this.ActualHeight;

            TopFrame.X1 = 0;
            TopFrame.X2 = this.ActualWidth;
            TopFrame.Y1 = 0;
            TopFrame.Y2 = 0;

            RightFrame.X1 = this.ActualWidth;
            RightFrame.X2 = this.ActualWidth;
            RightFrame.Y1 = 0;
            RightFrame.Y2 = this.ActualHeight;

            BottomFrame.X1 = 0;
            BottomFrame.X2 = this.ActualWidth;
            BottomFrame.Y1 = this.ActualHeight;
            BottomFrame.Y2 = this.ActualHeight;

            if (!(myProp is ReportCalibrationDataProp))
                setFrame();
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 根据属性设置边框
        /// </summary>
        private void setFrame()
        {
            if (GridText.ColumnDefinitions == null || GridText.ColumnDefinitions.Count == 0)
            {
                Grid.SetColumnSpan(TopFrame, 1);
                Grid.SetColumnSpan(LeftFrame, 1);
                Grid.SetColumnSpan(RightFrame, 1);
                Grid.SetColumnSpan(BottomFrame, 1);
            }
            else
            {
                Grid.SetColumnSpan(TopFrame, GridText.ColumnDefinitions.Count);
                Grid.SetColumnSpan(LeftFrame, GridText.ColumnDefinitions.Count);
                Grid.SetColumnSpan(RightFrame, GridText.ColumnDefinitions.Count);
                Grid.SetColumnSpan(BottomFrame, GridText.ColumnDefinitions.Count);
            }
            if (GridText.RowDefinitions == null || GridText.RowDefinitions.Count == 0)
            {
                Grid.SetRowSpan(TopFrame, 1);
                Grid.SetRowSpan(LeftFrame, 1);
                Grid.SetRowSpan(RightFrame, 1);
                Grid.SetRowSpan(BottomFrame, 1);
            }
            else
            {
                Grid.SetRowSpan(TopFrame, GridText.RowDefinitions.Count);
                Grid.SetRowSpan(LeftFrame, GridText.RowDefinitions.Count);
                Grid.SetRowSpan(RightFrame, GridText.RowDefinitions.Count);
                Grid.SetRowSpan(BottomFrame, GridText.RowDefinitions.Count);
            }
            if (myProp.ControlBorderThick.Top == 0)
            {
                TopFrame.StrokeDashArray = new DoubleCollection { 3, 3 };
                if (!bDesign)
                {
                    TopFrame.Visibility = Visibility.Hidden;
                }
            }
            else if (myProp.ControlBorderThick.Top > 0)
            {
                TopFrame.StrokeDashArray = null;
                TopFrame.StrokeThickness = myProp.ControlBorderThick.Top;
            }

            if (myProp.ControlBorderThick.Left == 0)
            {
                LeftFrame.StrokeDashArray = new DoubleCollection { 3, 3 };
                if (!bDesign)
                {
                    LeftFrame.Visibility = Visibility.Hidden;
                }
            }
            else if (myProp.ControlBorderThick.Left > 0)
            {
                LeftFrame.StrokeDashArray = null;
                LeftFrame.StrokeThickness = myProp.ControlBorderThick.Left;
            }

            if (myProp.ControlBorderThick.Right == 0)
            {
                RightFrame.StrokeDashArray = new DoubleCollection { 3, 3 };
                if (!bDesign)
                {
                    RightFrame.Visibility = Visibility.Hidden;
                }
            }
            else if (myProp.ControlBorderThick.Right > 0)
            {
                RightFrame.StrokeDashArray = null;
                RightFrame.StrokeThickness = myProp.ControlBorderThick.Right;
            }

            if (myProp.ControlBorderThick.Bottom == 0)
            {
                BottomFrame.StrokeDashArray = new DoubleCollection { 3, 3 };
                if (!bDesign)
                {
                    BottomFrame.Visibility = Visibility.Hidden;
                }
            }
            else if (myProp.ControlBorderThick.Bottom > 0)
            {
                BottomFrame.StrokeDashArray = null;
                BottomFrame.StrokeThickness = myProp.ControlBorderThick.Bottom;
            }
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 加载数据
        /// </summary>
        public void FillData()
        {
            object vdata = this.DataContext;
            if (vdata == null)
            {
                return;
            }
            if (myProp is ReportCalibrationDataProp)
            {
                if ((myProp as ReportCalibrationDataProp).CallibrationType == 0)
                {
                    List<Data.Exerciserecord> vlist = Objtoobj.DataTableToList<Data.Exerciserecord>((vdata as ReportData).GetDataTableByName("FlowCalibration"));
                    (myProp as ReportCalibrationDataProp).FillData(vlist, GridText);
                }
            }
            else
            {
                myProp.FillData(vdata as ReportData);
            }
        }
        #endregion
    }
}
