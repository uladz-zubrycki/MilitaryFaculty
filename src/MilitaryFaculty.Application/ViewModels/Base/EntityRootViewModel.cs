using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels.Base
{
    public abstract class EntityRootViewModel<T> : ComplexViewModel<T>
        where T : class
    {
        public ViewModel HeaderViewModel { get; protected set; }

        protected EntityRootViewModel(T model)
            : base(model)
        {
            // Empty
        }
    }
}