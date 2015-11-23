using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace LumbarRobot.Common
{
    public class ReportPropInfo
    {
        #region 声明属性

        /// <summary>
        /// 0=不可以移动也不可以改变大小 1=可以移动，不能改变大小 2 = 可以移动也可以改变大小 3=不可以移动，可以改变大小
        /// </summary>
        public int nCanMoveSize { get; set; }

        private string _DateFormat;
        /// <summary>
        ///日期格式 
        /// </summary>
        public string DateFormat
        {
            get { return _DateFormat; }
            set
            {
                _DateFormat = value;
                // RaisePropertyChanged("DateFormat");
            }
        }

        private Thickness _ControlBorderThick;
        /// <summary>
        /// 边框
        /// </summary>
        public Thickness ControlBorderThick
        {
            get { return _ControlBorderThick; }
            set { _ControlBorderThick = value; }
        }

        private double _ControlLeft;
        /// <summary>
        /// 控件左边距  相对位置的
        /// </summary>
        public double ControlLeft
        {
            get { return _ControlLeft; }
            set { _ControlLeft = value; }
        }

        private double _ControlTop;
        /// <summary>
        /// 控件顶部位置 相对位置的
        /// </summary>
        public double ControlTop
        {
            get { return _ControlTop; }
            set { _ControlTop = value; }
        }

        private double _ControlHeight;
        /// <summary>
        /// 控件高度
        /// </summary>
        public double ControlHeight
        {
            get { return _ControlHeight; }
            set { _ControlHeight = value; }
        }

        private double _ControlWidth;
        /// <summary>
        /// 控件宽度
        /// </summary>
        public double ControlWidth
        {
            get { return _ControlWidth; }
            set { _ControlWidth = value; }
        }

        /// <summary>
        /// 控件类型 
        /// </summary>
        public int ControlType { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool isReadOnly { get; set; }

        private double _FontSize;
        public double FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }

        private string _FontFamily;
        public string FontFamily
        {
            get { return _FontFamily; }
            set { _FontFamily = value; }
        }

        private string _FontStyle;
        public string FontStyle
        {
            get { return _FontStyle; }
            set { _FontStyle = value; }
        }

        /// <summary>
        /// 水平对齐方式
        /// </summary>
        public HorizontalAlignment horizontalAlignment { get; set; }

        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public VerticalAlignment verticalAlignment { get; set; }

        #endregion

        internal const double defaultHeight = 300;
        internal const double defaultWidth = 300;


        #region 构造函数
        public ReportPropInfo()
        {
            ControlWidth = defaultWidth;
            ControlHeight = defaultHeight;
            ControlTop = 0;
            ControlLeft = 0;
            FontSize = 15;
            FontFamily = "微软雅黑";
            FontStyle = "0";
            horizontalAlignment = HorizontalAlignment.Left;
            verticalAlignment = VerticalAlignment.Top;

        }
        #endregion

        #region 公共方法

        public virtual void CreateReportChild(Grid ParentGrid, Control vControl)
        {
            SetBaseControlProp(vControl);
        }

        public void SetBaseControlProp(Control vControl)
        {
            vControl.HorizontalContentAlignment = horizontalAlignment;
            vControl.VerticalContentAlignment = verticalAlignment;
            vControl.FontFamily = new System.Windows.Media.FontFamily(FontFamily);
            vControl.FontSize = FontSize;
            vControl.Width = ControlWidth;
            vControl.Height = ControlHeight;
            SetHorizontalAlignmentContent(horizontalAlignment);
            SetVerticalAlignmentContent(verticalAlignment);
        }

        /// <summary>
        /// 获取xaml文本
        /// </summary>
        /// <returns></returns>
        public virtual string Getxaml()
        {
            return GetControlMargin() + GetBorderThickNess() + GetFontxaml() + GetAlignment() + GetHeightWidth();
        }

        private string GetHeightWidth()
        {
            return " Width=\"" + ControlWidth.ToString() + "\" Height=\"" + ControlHeight.ToString() + "\" ";
        }


        private string GetFontxaml()
        {
            return " FontSize=" + "\"" + FontSize.ToString() + "\"  FontFamily=\"" + FontFamily + "\" FontStyle=\"" + FontStyle + "\" ";
        }

        private string GetAlignment()
        {
            return " HorizontalAlignment=\"" + horizontalAlignment.ToString() + "\"  VerticalAlignment=\"" + verticalAlignment.ToString() + "\"";
        }


        public void getPropFromxaml(XmlReader vreader)
        {
            object o = vreader.GetAttribute("BorderThickness");
            if (o != null)
            {
                ControlBorderThick = Objtoobj.strToThicknewss(o.ToString());
            }

            o = vreader.GetAttribute("FontSize");
            if (o != null)
            {
                FontSize = Objtoobj.objectToInt(o);
            }

            o = vreader.GetAttribute("FontFamily");
            if (o != null)
            {
                FontFamily = o.ToString();
            }

            o = vreader.GetAttribute("FontStyle");
            if (o != null)
            {
                FontStyle = o.ToString();
            }

            o = vreader.GetAttribute("HorizontalAlignment");
            if (o != null)
            {
                string svalue = o.ToString();
                if (svalue.Equals("Left"))
                {
                    horizontalAlignment = HorizontalAlignment.Left;
                }
                else if (svalue.Equals("Left"))
                {
                    horizontalAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    horizontalAlignment = HorizontalAlignment.Center;
                }
            }

            o = vreader.GetAttribute("VerticalAlignment");
            if (o != null)
            {
                string svalue = o.ToString();
                if (svalue.Equals("Top"))
                {
                    verticalAlignment = VerticalAlignment.Top;
                }
                else if (svalue.Equals("Bottom"))
                {
                    verticalAlignment = VerticalAlignment.Bottom;
                }
                else
                {
                    verticalAlignment = VerticalAlignment.Center;
                }
            }

            o = vreader.GetAttribute("Width");
            if (o != null)
            {
                ControlWidth = Objtoobj.objectToDouble(o);
            }

            o = vreader.GetAttribute("Height");
            if (o != null)
            {
                ControlHeight = Objtoobj.objectToDouble(o);
            }

            o = vreader.GetAttribute("Margin");
            if (o != null)
            {
                Thickness vMargin = Objtoobj.strToThicknewss(o.ToString());
                ControlLeft = vMargin.Left;
                ControlTop = vMargin.Top;
            }
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="vdata"></param>
        public virtual void FillData(ReportData vdata)
        {
        }

        public string GetBorderThickNess()
        {
            return " BorderThickness=" + "\"" + ControlBorderThick.Left.ToString() + "," + ControlBorderThick.Top.ToString() + "," + ControlBorderThick.Right.ToString() + "," + ControlBorderThick.Bottom.ToString() + "\" ";
        }


        public string GetControlMargin()
        {
            string strRet = " Margin=" + "\"" + ControlLeft.ToString() + "," + Math.Max(0, ControlTop).ToString() + ",0,0" + "\" ";
            // vmargin.Top = vmargin.Top + ControlHeight;
            return strRet;
        }

        public virtual void SetVerticalAlignmentContent(VerticalAlignment verticalAlignment)
        { }

        public virtual void SetHorizontalAlignmentContent(HorizontalAlignment horizontalAlignment)
        { }

        #endregion

        internal void FillCommonPara(ReportData vdata, ref string sValue)
        {
            if (string.IsNullOrEmpty(sValue))
            {
                return;
            }
            if (sValue.IndexOf("@当前日期和时间") >= 0)
            {
                sValue = sValue.Replace("@当前日期和时间", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (sValue.IndexOf("@当前日期") >= 0)
            {
                sValue = sValue.Replace("@当前日期", System.DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else if (sValue.IndexOf("@当前时间") >= 0)
            {
                sValue = sValue.Replace("@当前时间", System.DateTime.Now.ToString("HH:mm:ss"));
            }

           
        }
    }
}

