using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class CathedraInfoViewModel : ViewModel<Cathedra>
    {
        public CathedraInfoViewModel(Cathedra model)
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