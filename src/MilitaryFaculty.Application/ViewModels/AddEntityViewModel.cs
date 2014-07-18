using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
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