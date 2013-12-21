using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class AddEntityViewModel<T>: ComplexViewModel<T>
        where T: UniqueEntity
    {
        public abstract ICommand AddCommand { get; }

        protected AddEntityViewModel(T model)
            : base(model)
        {
            Tag = EditableViewMode.Edit;
        }
    }
}
