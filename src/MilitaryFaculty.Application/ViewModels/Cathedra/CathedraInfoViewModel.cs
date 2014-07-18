using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class CathedraInfoViewModel : ViewModel<Domain.Cathedra>
    {
        public CathedraInfoViewModel(Domain.Cathedra model)
            : base(model)
        {
            // Empty
        }

        public override string Title
        {
            get { return Model.Name; }
        }
    }
}