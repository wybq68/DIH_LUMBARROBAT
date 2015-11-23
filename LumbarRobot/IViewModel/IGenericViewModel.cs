using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LumbarRobot.IView;
using System.ComponentModel;

namespace LumbarRobot.IViewModel
{
    public interface IGenericViewModel<T> : IGenericInteractionView<T>, INotifyPropertyChanged
    {
        T Entity { get; set; }
    }
}
