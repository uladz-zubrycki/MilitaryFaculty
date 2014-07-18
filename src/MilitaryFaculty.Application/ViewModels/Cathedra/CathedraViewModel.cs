using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
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