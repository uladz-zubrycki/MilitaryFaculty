using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class PublicationRootViewModel : EntityRootViewModel<Domain.Publication>
    {
        public PublicationRootViewModel(Domain.Publication model)
            : base(model)
        {
            HeaderViewModel = new PublicationHeaderViewModel();
        }

        protected override IEnumerable<ViewModel<Domain.Publication>> GetViewModels()
        {
            return new ViewModel<Domain.Publication>[]
                   {
                       new PublicationInfoViewModel(Model),
                       new PublicationExtraInfoViewModel(Model)
                   };
        }
    }
}