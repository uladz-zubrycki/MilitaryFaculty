using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceListItemViewModel : ListItemViewModel<Conference>
    {
        public ConferenceListItemViewModel(Conference model)
            : base(model)
        {
            TooltipViewModel = new ConferenceReportViewModel(Model);
            InitCommands();
        }

        public override string PrimaryInfo
        {
            get { return Model.Date.ToShortDateString(); }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
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

            return new ImagedCommandViewModel(Do.ConferenceRemove,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ImagedCommandViewModel CreateBrowseConferenceDetailsCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.ConferenceDetails,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        public static ConferenceListItemViewModel FromModel(Conference model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new ConferenceListItemViewModel(model);
        }
    }
}