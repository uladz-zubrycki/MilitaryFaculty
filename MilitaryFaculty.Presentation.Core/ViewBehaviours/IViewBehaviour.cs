using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.Core.ViewBehaviours
{
    public interface IViewBehaviour
    {
        void Inject(ViewModel viewModel);
    }
}