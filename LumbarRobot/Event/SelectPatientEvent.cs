using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using LumbarRobot.Data;

namespace LumbarRobot.Event
{
    public class SelectPatientEvent : CompositePresentationEvent<Syspatient>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly SelectPatientEvent _event;

        static SelectPatientEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<SelectPatientEvent>();
        }

        public static SelectPatientEvent Instance
        {
            get { return _event; }
        }
    }
}
