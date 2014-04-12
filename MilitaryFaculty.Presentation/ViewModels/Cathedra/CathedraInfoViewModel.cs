using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraInfoViewModel : ViewModel<Cathedra>
    {
        public override string Title
        {
            get { return Model.Name; }
        }

        public CathedraInfoViewModel(Cathedra model)
            : base(model)
        {
            // Empty
        }
    }
}