using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.IViewModel
{
    public interface IGenericAdapter<T>
    {
        IGenericViewModel<T> ViewModel { get; }
    }
}
