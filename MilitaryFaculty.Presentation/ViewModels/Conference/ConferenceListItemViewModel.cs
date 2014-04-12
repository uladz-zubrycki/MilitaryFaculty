using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceListItemViewModel : ListItemViewModel<Conference>
    {
        public override string PrimaryInfo
        {
            get { return Model.Date.ToShortDateString(); }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        public ConferenceListItemViewModel(Conference model)
            : base(model)
        {
            TooltipViewModel = new ConferenceReportViewModel(Model);
            InitCommands();
        }

        public static Func<Conference, ConferenceListItemViewModel> FromModel()
        {
            return conference => new ConferenceListItemViewModel(conference);
        }

        protected void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseConferenceDetailsCommand(),
                                  CreateRemoveConferenceCommand()
                              });
        }

        private ImagedCommandViewModel CreateRemoveConferenceCommand()
        {
            const string tooltip = "Удалить конференцию";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.Conference.Remove,
                Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseConferenceDetailsCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.Conference.Details,
                Model, tooltip, imageSource);
        }
    }
}