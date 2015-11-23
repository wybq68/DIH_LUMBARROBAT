using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Interactions
{
    public class AlarmInteractionDialog : GenericInteractionDialogBase<AlarmInfo>
    {


        public AlarmInteractionDialog()
        {
            //if (GlobalVar.RobotControl != null)
            //{
            //    //remoteControl = new RemoteControlHandler(RobotControl_RemoteControl);
            //    //GlobalVar.RobotControl.RemoteControl += remoteControl;
            //}

        }
        public override void Ok()
        {
            base.Ok();
        }

        public override void Cancel()
        {
            base.Cancel();
        }
    }
}