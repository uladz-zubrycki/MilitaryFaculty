using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
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