using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class ComplexViewModel<T> : ViewModel<T>
        where T : class
    {
        #region Class Fields
        
        private object tag;
        private ObservableCollection<ViewModel<T>> viewModels;

        #endregion // Class Fields

        #region Class Properties
        
        public ObservableCollection<ViewModel<T>> ViewModels
        {
            get
            {
                if (viewModels == null)
                {
                    viewModels = new ObservableCollection<ViewModel<T>>(GetViewModels());
                }

                return viewModels;
            }
        }

        public sealed override object Tag
        {
            get { return tag; }

            set
            {
                SetValue(() => tag, value);
                ViewModels.ForEach(vm => vm.Tag = value);
            }
        }
        
        #endregion // Class Properties

        #region Class Constructors

        protected ComplexViewModel(T model)
            : base(model)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected abstract IEnumerable<ViewModel<T>> GetViewModels();

        #endregion // Class Protected Methods
    }
}