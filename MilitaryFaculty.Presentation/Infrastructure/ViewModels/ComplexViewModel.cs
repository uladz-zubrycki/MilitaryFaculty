using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class ComplexViewModel<T> : ViewModel<T>
        where T : class
    {
        private readonly object _tag;
        private ObservableCollection<ViewModel<T>> _viewModels;

        public ObservableCollection<ViewModel<T>> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new ObservableCollection<ViewModel<T>>(GetViewModels());
                }

                return _viewModels;
            }
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

        protected ComplexViewModel(T model)
            : base(model)
        {
            // Empty
        }

        protected abstract IEnumerable<ViewModel<T>> GetViewModels();
    }
}