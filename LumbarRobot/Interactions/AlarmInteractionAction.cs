using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.Common;
using System.Windows;

namespace LumbarRobot.Interactions
{
    public class AlarmInfo : ViewModelBase
    {
        private string message;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                this.RaisePropertyChangedEvent("Message");
            }
        }
        private string caption;

        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                this.RaisePropertyChangedEvent("Caption");
            }
        }

        private bool isCanClose = false;

        public bool IsCanClose
        {
            get { return isCanClose; }
            set
            {
                isCanClose = value;
                this.RaisePropertyChangedEvent("IsCanClose");
            }
        }

        private Visibility okButtonVisibility;

        public Visibility OkButtonVisibility
        {
            get { return okButtonVisibility; }
            set
            {
                okButtonVisibility = value;
                this.RaisePropertyChangedEvent("OkButtonVisibility");
            }
        }

        private Visibility cancelVisibility;

        public Visibility CancelVisibility
        {
            get { return cancelVisibility; }
            set
            {
                cancelVisibility = value;
                this.RaisePropertyChangedEvent("CancelVisibility");
            }
        }

        private int alarmCode;

        public int AlarmCode
        {
            get { return alarmCode; }
            set
            {
                alarmCode = value;
                this.RaisePropertyChangedEvent("AlarmCode");
            }
        }
    }
    public class AlarmInteractionAction : GenericInteractionAction<AlarmInfo>
    {

    }
}
