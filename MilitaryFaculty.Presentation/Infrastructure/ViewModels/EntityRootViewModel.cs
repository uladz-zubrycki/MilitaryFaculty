namespace MilitaryFaculty.Presentation.Infrastructure
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