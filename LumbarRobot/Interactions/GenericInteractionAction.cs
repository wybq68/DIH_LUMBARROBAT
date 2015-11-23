using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using LumbarRobot.Requests;
using LumbarRobot.IView;

namespace LumbarRobot.Interactions
{
    public class GenericInteractionAction<T> : TriggerAction<Grid>
    {
        public static readonly DependencyProperty DialogProperty =
            DependencyProperty.Register("Dialog", typeof(GenericInteractionDialogBase<T>), typeof(GenericInteractionAction<T>), new PropertyMetadata(null));

        public GenericInteractionDialogBase<T> Dialog
        {
            get { return (GenericInteractionDialogBase<T>)GetValue(DialogProperty); }
            set { SetValue(DialogProperty, value); }
        }

        protected override void Invoke(object parameter)
        {
            var args = parameter as GenericInteractionRequestEventArgs<T>;
            //查询改窗口是否已打开
            if (args != null)
            {
                UIElement element = GenericInteractionDialogBase<T>.FindDialog(this.AssociatedObject);
                if (element != null)
                    this.AssociatedObject.Children.Remove(element);
            }

            if (!args.Close) //非关闭请求状态
            {
                this.SetDialog(args.Entity, args.Callback, args.CancelCallback, null);
            }
        }

        private void SetDialog(T entity, Action<T> callback, Action cancelCallback, UIElement element)
        {
            if (this.Dialog is IGenericInteractionView<T>)
            {
                IGenericInteractionView<T> view = this.Dialog as IGenericInteractionView<T>;
                view.SetEntity(entity);
                EventHandler handler = null;
                handler = (s, e) =>
                {

                    this.Dialog.ConfirmEventHandler -= handler;
                    this.Dialog.CancelEventHandler -= handler;
                    this.AssociatedObject.Children.Remove(this.Dialog);

                    if (e.ToString() == InteractionType.OK.ToString())
                        callback(view.GetEntity());
                    else
                        cancelCallback();
                };

                this.Dialog.ConfirmEventHandler += handler;
                this.Dialog.CancelEventHandler += handler;
                this.Dialog.SetValue(Grid.RowSpanProperty, this.AssociatedObject.RowDefinitions.Count == 0 ? 1 : this.AssociatedObject.RowDefinitions.Count);
                this.Dialog.SetValue(Grid.ColumnSpanProperty, this.AssociatedObject.ColumnDefinitions.Count == 0 ? 1 : this.AssociatedObject.ColumnDefinitions.Count);
                this.AssociatedObject.Children.Add(this.Dialog);
            }
        }
    }
}
