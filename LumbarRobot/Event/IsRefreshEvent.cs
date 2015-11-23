using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace LumbarRobot.Event
{
    public class IsRefreshEvent : CompositePresentationEvent<bool>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly IsRefreshEvent _event;

        static IsRefreshEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<IsRefreshEvent>();
        }

        public static IsRefreshEvent Instance
        {
            get { return _event; }
        }
    }
}

