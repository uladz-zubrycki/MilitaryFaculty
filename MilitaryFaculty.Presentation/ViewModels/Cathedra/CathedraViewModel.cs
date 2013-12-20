using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraViewModel : ComplexViewModel<Cathedra>
    {
        public CathedraViewModel(Cathedra model)
            : base(model)
        {
            // Empty
        }

        protected override IEnumerable<ViewModel<Cathedra>> GetViewModels()
        {
            return new[]
                   {
                       new CathedraInfoViewModel(Model)
                   };
        }
    }
}