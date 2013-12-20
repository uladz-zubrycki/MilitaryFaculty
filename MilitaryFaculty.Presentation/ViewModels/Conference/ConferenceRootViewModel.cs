using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceRootViewModel : EntityRootViewModel<Conference>
    {
        public override string Title
        {
            get { return "Информация о конференции"; }
        }

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