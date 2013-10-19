// ReSharper disable InconsistentNaming
namespace MilitaryFaculty.Presentation.Infrastructure
{
    public interface IUICommandPackage
    {
        void Inject(ViewModel viewModel);
        void Inject(ViewModel viewModel, object defaultTag);
    }
}
// ReSharper restore InconsistentNaming
