using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ConferenceRootViewModel : EntityRootViewModel<Domain.Conference>
    {
        public ConferenceRootViewModel(Domain.Conference model)
            : base(model)
        {
            HeaderViewModel = new ConferenceHeaderViewModel();
        }

        protected override IEnumerable<ViewModel<Domain.Conference>> GetViewModels()
        {
            return new ViewModel<Domain.Conference>[]
                   {
                       new ConferenceInfoViewModel(Model),
                       new ConferenceReportViewModel(Model)
                   };
        }
    }
}