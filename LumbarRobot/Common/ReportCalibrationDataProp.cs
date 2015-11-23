using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using LumbarRobot.Data;

namespace LumbarRobot.Common
{
    public class ReportCalibrationDataProp : ReportPropInfo
    {

        #region  定义变量
        private int nColWidth = 80;
        private int nRowHeight = 40;
        private List<NameValueInfo> vlist = new List<NameValueInfo>();
        #endregion

        #region 声明属性
        /// <summary>
        /// 定标类型  0=流量定标 1=成分定标
        /// </summary>
        public int CallibrationType { get; set; }
        /// <summary>
        /// 要显示的数据条数
        /// </summary>
        public int DataCount { get; set; }
        #endregion


        #region 构造函数

        public ReportCalibrationDataProp(int ntype)
        {
            CallibrationType = ntype;
            DataCount = 10;
            nCanMoveSize = 1;
            vlist.Clear();
            if (ntype == 0)
            {
                NameValueInfo nv = new NameValueInfo();
                nv.sName = "FCTime";
                nv.sValue = "FCTime";
                vlist.Add(nv);


                nv = new NameValueInfo();
                nv.sName = "InFactor1";
                nv.sValue = "InFactor1";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "InFactor2";
                nv.sValue = "InFactor2";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "InFactor3";
                nv.sValue = "InFactor3";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "InFactor4";
                nv.sValue = "InFactor4";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "InFactor5";
                nv.sValue = "InFactor5";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "ExFactor1";
                nv.sValue = "ExFactor1";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "ExFactor2";
                nv.sValue = "ExFactor2";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "ExFactor3";
                nv.sValue = "ExFactor3";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "ExFactor4";
                nv.sValue = "ExFactor4";
                vlist.Add(nv);

                nv = new NameValueInfo();
                nv.sName = "ExFactor5";
                nv.sValue = "ExFactor5";
                vlist.Add(nv);

            }
            ControlBorderThick = new System.Windows.Thickness(1, 1, 1, 1);

        }
        #endregion

        #region 公共方法
        public override void CreateReportChild(System.Windows.Controls.Grid GridText, System.Windows.Controls.Control vControl)
        {
            double nleft = 0;
            double ntop = 10;
            if (vlist == null || vlist.Count == 0)
            {
                return;
            }

            foreach (var item in vlist)
            {
                TextBlock vt = new TextBlock();
                vt.Text = item.sName;
                if ("FCTime".Equals(vt.Text))
                {
                    vt.Text = "定标时间";
                }
                else
                {
                    vt.Text = vt.Text.Replace("In", "吸气").Replace("Ex", "呼气").Replace("Factor", "系数");
                }
                vt.Margin = new System.Windows.Thickness(nleft, ntop, 0, 0);
                vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                vt.Width = nColWidth;
                vt.Height = nRowHeight;
                vt.TextAlignment = System.Windows.TextAlignment.Center;
                GridText.Children.Add(vt);

                Line line = new Line();
                line.X1 = nleft;
                line.X2 = nleft;
                line.Y1 = 0;
                line.Y2 = nRowHeight;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.5;
                line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                line.Margin = new System.Windows.Thickness(0.5);
                GridText.Children.Add(line);

                line = new Line();
                line.X1 = nColWidth + nleft;
                line.X2 = nColWidth + nleft;
                line.Y1 = 0;
                line.Y2 = nRowHeight;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.5;
                line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                line.Margin = new System.Windows.Thickness(0.5);
                GridText.Children.Add(line);

                line = new Line();
                line.X1 = 0 + nleft;
                line.X2 = nColWidth + nleft;
                line.Y1 = 0;
                line.Y2 = 0;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.5;
                line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                line.Margin = new System.Windows.Thickness(0.5);
                GridText.Children.Add(line);

                line = new Line();
                line.X1 = 0 + nleft;
                line.X2 = nColWidth + nleft;
                line.Y1 = nRowHeight;
                line.Y2 = nRowHeight;
                line.Stroke = Brushes.Black;
                line.StrokeThickness = 0.5;
                line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                line.Margin = new System.Windows.Thickness(0.5);
                line.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                GridText.Children.Add(line);

                nleft += nColWidth;
            }
            ControlWidth = nleft;
            ControlHeight = nRowHeight;
        }

        /// <summary>
        /// 保存结果
        /// </summary>
        /// <returns></returns>
        public override string Getxaml()
        {
            StringBuilder sReportHeader = new StringBuilder();
            sReportHeader.Append("<chart:CalibrationData ");
            sReportHeader.Append(base.Getxaml());
            sReportHeader.Append("CallibrationType=\"" + CallibrationType.ToString() + "\"");
            sReportHeader.Append(">");
            sReportHeader.Append("</chart:CalibrationData>");
            return sReportHeader.ToString();
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="vlist"></param>
        public void FillData(List<Exerciserecord> vGasList, System.Windows.Controls.Grid GridText)
        {
            if (vGasList == null || vGasList.Count == 0)
            {
                return;
            }
            List<Exerciserecord> vtemlist = vGasList.Take(DataCount).ToList();
            vtemlist.Sort((left, right) =>
            {
                return -1 * left.StartTime.CompareTo(right.StartTime);
            });
            double ntop = nRowHeight;
            foreach (var item in vtemlist)
            {
                double nleft = 0;
                foreach (var itemcol in vlist)
                {
                    TextBlock vt = new TextBlock();
                    Type type = typeof(Exerciserecord);
                    vt.Text = type.GetProperty(itemcol.sValue).GetValue(item, null).ToString();
                    vt.TextWrapping = System.Windows.TextWrapping.WrapWithOverflow;
                    vt.Margin = new System.Windows.Thickness(nleft, ntop + 10, 0, 0);
                    vt.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    vt.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    vt.Width = nColWidth;
                    vt.Height = nRowHeight;
                    vt.TextAlignment = System.Windows.TextAlignment.Center;
                    GridText.Children.Add(vt);

                    Line line = new Line();
                    line.X1 = nleft;
                    line.X2 = nleft;
                    line.Y1 = 0 + ntop;
                    line.Y2 = nRowHeight + ntop;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    GridText.Children.Add(line);

                    line = new Line();
                    line.X1 = nColWidth + nleft;
                    line.X2 = nColWidth + nleft;
                    line.Y1 = 0 + ntop;
                    line.Y2 = nRowHeight + ntop;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    GridText.Children.Add(line);

                    line = new Line();
                    line.X1 = 0 + nleft;
                    line.X2 = nColWidth + nleft;
                    line.Y1 = 0 + ntop;
                    line.Y2 = 0 + ntop;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    GridText.Children.Add(line);

                    line = new Line();
                    line.X1 = 0 + nleft;
                    line.X2 = nColWidth + nleft;
                    line.Y1 = nRowHeight + ntop;
                    line.Y2 = nRowHeight + ntop;
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 0.5;
                    line.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    line.Margin = new System.Windows.Thickness(0.5);
                    line.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    GridText.Children.Add(line);

                    nleft += nColWidth;

                }
                ntop += nRowHeight;
            }
            ControlHeight = (vtemlist.Count + 1) * nRowHeight;
        }
        #endregion
    }
}
