using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddConferenceViewModel : AddEntityViewModel<Conference>
    {
        public override string Title
        {
            get { return "Добавить конференцию"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Conference.Add; }
        }

        public AddConferenceViewModel(Conference model)
            : base(model)
        {
            // Empty
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