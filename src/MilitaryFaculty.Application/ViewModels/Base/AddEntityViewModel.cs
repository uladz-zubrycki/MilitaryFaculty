using System.Windows.Input;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels.Base
{
    public abstract class AddEntityViewModel<T> : ComplexViewModel<T>
        where T : UniqueEntity
    {
        public virtual ICommand AddCommand
        {
            get { return GlobalCommands.Add<T>(); }
        }

        protected AddEntityViewModel(T model)
            : base(model)
        {
            Tag = EditableViewMode.Edit;
        }
    }
}