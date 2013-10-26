using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraInfoViewModel : ViewModel<Cathedra>
    {
        public CathedraInfoViewModel(Cathedra model)
            :base(model)
        {
            Title = model.Name;
        }
    }
}
