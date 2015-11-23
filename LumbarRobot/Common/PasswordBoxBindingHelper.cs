using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace LumbarRobot.Common
{
    public static class PasswordBoxBindingHelper
    {
        public static string Pwd { get; set; }
        public static string Pwd_Chg { get; set; }
        public static string PwdNew_Chg { get; set; }
        public static string PwdNewOk_Chg { get; set; }

        public static readonly DependencyProperty IsPasswordBindingEnabledProperty = DependencyProperty.RegisterAttached("IsPasswordBindingEnabled", typeof(bool), typeof(PasswordBoxBindingHelper), new UIPropertyMetadata(false, OnIsPasswordBindingEnabledChanged));

        public static bool GetIsPasswordBindingEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsPasswordBindingEnabledProperty);
        }

        public static void SetIsPasswordBindingEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsPasswordBindingEnabledProperty, value);
        }

        private static void OnIsPasswordBindingEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) { var passwordBox = obj as PasswordBox; if (passwordBox != null) { passwordBox.PasswordChanged -= PasswordBoxPasswordChanged; if ((bool)e.NewValue) { passwordBox.PasswordChanged += PasswordBoxPasswordChanged; } } }

        //when the passwordBox's password changed, update the buffer 

        static void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e) { var passwordBox = (PasswordBox)sender; if (!String.Equals(GetBindedPassword(passwordBox), passwordBox.Password)) { SetBindedPassword(passwordBox, passwordBox.Password); } }

        public static string GetBindedPassword(DependencyObject obj) { return (string)obj.GetValue(BindedPasswordProperty); }

        public static void SetBindedPassword(DependencyObject obj, string value) { obj.SetValue(BindedPasswordProperty, value); }

        public static readonly DependencyProperty BindedPasswordProperty = DependencyProperty.RegisterAttached("BindedPassword", typeof(string), typeof(PasswordBoxBindingHelper), new UIPropertyMetadata(string.Empty, OnBindedPasswordChanged));

        //when the buffer changed, upate the passwordBox's password  
        private static void OnBindedPasswordChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = obj as PasswordBox;
            if (passwordBox != null)
            {
                //Common
                passwordBox.Password = e.NewValue == null ? string.Empty : e.NewValue.ToString();

                //Change password
                if (passwordBox.Name == "pwd_Chg")
                {
                    //Pwd_Chg = (string.IsNullOrEmpty(passwordBox.Password)) ? (Pwd_Chg) : (passwordBox.Password);
                    //passwordBox.Password = Pwd_Chg;

                    Pwd_Chg = passwordBox.Password;
                }
                else if (passwordBox.Name == "pwdNew_Chg")
                {
                    //PwdNew_Chg = (string.IsNullOrEmpty(passwordBox.Password)) ? (PwdNew_Chg) : (passwordBox.Password);
                    //passwordBox.Password = PwdNew_Chg;

                    PwdNew_Chg = passwordBox.Password;
                }
                else if (passwordBox.Name == "pwdNewOk_Chg")
                {
                    //PwdNewOk_Chg = (string.IsNullOrEmpty(passwordBox.Password)) ? (PwdNewOk_Chg) : (passwordBox.Password);
                    //passwordBox.Password = PwdNewOk_Chg;

                    PwdNewOk_Chg = passwordBox.Password;
                }
                //else
                //{
                //    Pwd = (string.IsNullOrEmpty(passwordBox.Password)) ? (Pwd) : (passwordBox.Password);
                //    passwordBox.Password = Pwd;
                //}

                //SetPasswordBoxSelection(passwordBox, 0, Pwd.Length);
                SetPasswordBoxSelection(passwordBox, passwordBox.Password.Length, 0);
                //select.Invoke(pb, new object[] { pb.Password.Length, 0 });
            }
        }

        private static void SetPasswordBoxSelection(PasswordBox passwordBox, int start, int length)
        {
            var select = passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic);
            select.Invoke(passwordBox, new object[] { start, length });
        }
    }
}
