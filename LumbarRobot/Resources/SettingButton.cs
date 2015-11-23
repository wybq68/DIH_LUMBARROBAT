using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace LumbarRobot.Resources
{
    public class SettingButton : Button
    {
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool isSelected
        {
            get { return (bool)GetValue(isSelectedProperty); }
            set { SetValue(isSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isSelectedProperty =
            DependencyProperty.Register("isSelected", typeof(bool), typeof(SettingButton), new PropertyMetadata(false));


    }
}
