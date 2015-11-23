using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace LumbarRobot.Event
{
    public class SaveCourseEvent: CompositePresentationEvent<bool>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly SaveCourseEvent _event;

        static SaveCourseEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<SaveCourseEvent>();
        }

        public static SaveCourseEvent Instance
        {
            get { return _event; }
        }
    }
}
