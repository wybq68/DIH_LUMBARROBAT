using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.IView
{
    public interface IGenericInteractionView<T>
    {
        void SetEntity(T entity);
        T GetEntity();
    }
}
