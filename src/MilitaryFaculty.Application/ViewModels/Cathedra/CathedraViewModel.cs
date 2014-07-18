using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class CathedraViewModel : ComplexViewModel<Domain.Cathedra>
    {
        public CathedraViewModel(Domain.Cathedra model)
            : base(model)
        {
            // Empty
        }

        protected override IEnumerable<ViewModel<Domain.Cathedra>> GetViewModels()
        {
            return new[]
                   {
                       new CathedraInfoViewModel(Model)
                   };
        }
    }
}