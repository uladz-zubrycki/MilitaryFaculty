using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal class AddConferenceViewModel : AddEntityViewModel<Domain.Conference>
    {
        public AddConferenceViewModel(Domain.Conference model)
            : base(model)
        {
            // Empty
        }

        public override string Title
        {
            get { return "Добавить конференцию"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.ConferenceAdd; }
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