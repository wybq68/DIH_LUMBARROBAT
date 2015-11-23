using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.IViewModel
{
    public class GenericAdapter<T> : IGenericAdapter<T>
    {
        private readonly IGenericViewModel<T> viewModel;

        public GenericAdapter()
        {
            this.viewModel = new GenericViewModel<T>();
        }

        public IGenericViewModel<T> ViewModel
        {
            get { return this.viewModel; }
        }

        public void SetEntity(T entity)
        {
            this.ViewModel.SetEntity(entity);
        }

        public T GetEntity()
        {
            return this.ViewModel.GetEntity();
        }
    }
}
