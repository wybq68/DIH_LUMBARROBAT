using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Requests
{
    public class GenericInteractionRequest<T> : IGenericInteractionRequest<T>
    {
        public event EventHandler<GenericInteractionRequestEventArgs<T>> Raised;
        public event EventHandler<GenericInteractionRequestEventArgs<T>> Closed;

        public void Raise(T entity, Action<T> callback, Action cancelCallback)
        {
            if (this.Raised != null)
            {
                this.Raised(this, new GenericInteractionRequestEventArgs<T>(entity, callback, cancelCallback));
            }
        }

        public void Close(T entity)
        {
            if (this.Raised != null)
            {
                this.Raised(this, new GenericInteractionRequestEventArgs<T>(entity));
            }
        }
    }
}
