using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.ViewBehaviours
{
    public interface IViewBehaviour
    {
        void Inject(ViewModel viewModel);
    }

    public interface IViewBehaviour<T> where T : class
    {
        void Inject(ViewModel<T> viewModel);
    }
}