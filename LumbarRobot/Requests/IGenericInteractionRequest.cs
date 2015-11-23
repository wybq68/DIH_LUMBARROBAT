using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Requests
{
    public interface IGenericInteractionRequest<T>
    {
        event EventHandler<GenericInteractionRequestEventArgs<T>> Raised;

    }
}
