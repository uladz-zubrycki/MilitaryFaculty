using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal class AddConferenceViewModel : AddEntityViewModel<Conference>
    {
        public AddConferenceViewModel(Conference model)
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