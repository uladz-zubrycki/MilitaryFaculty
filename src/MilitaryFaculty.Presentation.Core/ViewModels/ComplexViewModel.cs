using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Presentation.Core.ViewModels
{
    public abstract class ComplexViewModel<T> : ViewModel<T>
        where T : class
    {
        private readonly Lazy<ObservableCollection<ViewModel<T>>> _viewModels;
        private readonly object _tag;

        protected ComplexViewModel(T model)
            : base(model)
        {
            _viewModels = Lazy.Create(() => new ObservableCollection<ViewModel<T>>(GetViewModels()));
        }

        public ObservableCollection<ViewModel<T>> ViewModels
        {
            get { return _viewModels.Value; }
        }

        public override sealed object Tag
        {
            get { return _tag; }
            set
            {
                SetValue(() => _tag, value);
                ViewModels.ForEach(vm => vm.Tag = value);
            }
        }

        protected abstract IEnumerable<ViewModel<T>> GetViewModels();
    }
}