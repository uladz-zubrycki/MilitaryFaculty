using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ConferenceRootViewModel : EntityRootViewModel<Conference>
    {
        public ConferenceRootViewModel(Conference model)
            : base(model)
        {
            HeaderViewModel = new ConferenceHeaderViewModel();
        }

        protected override IEnumerable<ViewModel<Conference>> GetViewModels()
        {
            return new ViewModel<Conference>[]
                   {
                       new ConferenceInfoViewModel(Model),
                       new ConferenceReportViewModel(Model)
                   };
        }
    }
}