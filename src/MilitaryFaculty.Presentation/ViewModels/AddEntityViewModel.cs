using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class AddEntityViewModel<T> : ComplexViewModel<T>
        where T : UniqueEntity
    {
        public abstract ICommand AddCommand { get; }

        protected AddEntityViewModel(T model)
            : base(model)
        {
            Tag = EditableViewMode.Edit;
        }
    }
}