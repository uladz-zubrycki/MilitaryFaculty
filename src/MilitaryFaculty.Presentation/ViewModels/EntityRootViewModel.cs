using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.ViewModels
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