using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionRootViewModel : EntityRootViewModel<Exhibition>
    {
        public ExhibitionRootViewModel(Exhibition model)
            : base(model)
        {
            HeaderViewModel = new ExhibitionHeaderViewModel();
        }

        protected override IEnumerable<ViewModel<Exhibition>> GetViewModels()
        {
            return new[]
                   {
                       new ExhibitionInfoViewModel(Model)
                   };
        }
    }
}