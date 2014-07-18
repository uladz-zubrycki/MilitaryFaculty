using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ExhibitionRootViewModel : EntityRootViewModel<Domain.Exhibition>
    {
        public ExhibitionRootViewModel(Domain.Exhibition model)
            : base(model)
        {
            HeaderViewModel = new ExhibitionHeaderViewModel();
        }

        protected override IEnumerable<ViewModel<Domain.Exhibition>> GetViewModels()
        {
            return new[]
                   {
                       new ExhibitionInfoViewModel(Model)
                   };
        }
    }
}