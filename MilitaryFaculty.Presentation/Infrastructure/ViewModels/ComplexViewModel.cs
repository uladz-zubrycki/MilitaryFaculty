using System.Collections.ObjectModel;
using System.Linq;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class ComplexViewModel<T> : ViewModel<T>
        where T : class
    {
        #region Class Properties

        public ObservableCollection<ViewModel<T>> ViewModels { get; protected set; }

        public override object Tag
        {
            get
            {
                if (ViewModels.Count == 0)
                {
                    return null;
                }

                var tags = ViewModels.Select(vm => vm.Tag)
                                     .GroupBy(tag => tag)
                                     .Select(gr => gr.Key)
                                     .ToList();

                return tags.Single();
            }

            set
            {
                if (value.Equals(Tag))
                {
                    return;
                }

                foreach (var viewModel in ViewModels)
                {
                    viewModel.Tag = value;
                }

                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        protected ComplexViewModel(T model)
            : base(model)
        {
            ViewModels = new ObservableCollection<ViewModel<T>>();
        }

        #endregion // Class Constructors
    }
}