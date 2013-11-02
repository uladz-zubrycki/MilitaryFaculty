namespace MilitaryFaculty.Presentation.Infrastructure
{
    public interface IViewBehaviour
    {
        void Inject(ViewModel viewModel);
        void Inject(ViewModel viewModel, object defaultTag);
    }
}
