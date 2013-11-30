using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal abstract class EntityAddViewModel<T>: ComplexViewModel<T>
        where T: UniqueEntity
    {
        public abstract ICommand AddCommand { get; }

        protected EntityAddViewModel(T model)
            : base(model)
        {
            // Empty
        } 
    }
}
