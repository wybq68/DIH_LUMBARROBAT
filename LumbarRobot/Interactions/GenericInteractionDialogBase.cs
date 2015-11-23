using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using LumbarRobot.IView;


namespace LumbarRobot.Interactions
{
    public enum InteractionType
    {
        OK,
        Cancel
    }

    public class GenericInteractionDialogBase<T> : UserControl
    {

        class InteractionEventArgs : EventArgs
        {
            internal InteractionType Type { get; private set; }

            internal InteractionEventArgs(InteractionType _type)
            {
                this.Type = _type;
            }

            public override string ToString()
            {
                return this.Type.ToString();
            }
        }


        public GenericInteractionDialogBase() { }

        public event EventHandler ConfirmEventHandler;
        public event EventHandler CancelEventHandler;

        public virtual void Ok()
        {
            this.OnClose(new InteractionEventArgs(InteractionType.OK));
        }

        public virtual void Cancel()
        {
            this.OnClose(new InteractionEventArgs(InteractionType.Cancel));
        }

        private void OnClose(InteractionEventArgs e)
        {
            var handler = (e.Type == InteractionType.OK) ? this.ConfirmEventHandler : this.CancelEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        public static UIElement FindDialog(Grid parent)
        {
            IEnumerator en = parent.Children.GetEnumerator();
            UIElement element = null;
            while (en.MoveNext() && element == null)
            {
                if (en.Current is IGenericInteractionView<T>)
                    element = en.Current as UIElement;
            }
            return element;
        }
    }
}
