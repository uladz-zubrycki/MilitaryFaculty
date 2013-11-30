using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraViewModel : ComplexViewModel<Cathedra>
    {
        public CathedraViewModel(Cathedra model)
            : base(model)
        {
            ViewModels.AddRange(new[]
                                {

                                    new CathedraInfoViewModel(Model)
                                });
        }
    }
}