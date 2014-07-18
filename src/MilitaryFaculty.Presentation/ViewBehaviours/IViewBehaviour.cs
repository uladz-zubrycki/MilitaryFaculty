using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.ViewBehaviours
{
    public interface IViewBehaviour
    {
        void Inject(ViewModel viewModel);
    }
}