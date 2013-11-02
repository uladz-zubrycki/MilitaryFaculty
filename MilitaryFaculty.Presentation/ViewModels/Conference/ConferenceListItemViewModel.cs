using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceListItemViewModel : ComplexViewModel<Conference>
    {
        #region Type Static Members

        public static Func<Conference, ConferenceListItemViewModel> FromModel()
        {
            return conference => new ConferenceListItemViewModel(conference);
        }

        #endregion // Type Static Members

        #region Class Properties

        public ConferenceInfoViewModel InfoViewModel { get; private set; }
        public ConferenceReportViewModel ReportViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceListItemViewModel(Conference model)
            : base(model)
        {
            InitViewModels();
            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels()
        {
            InfoViewModel = new ConferenceInfoViewModel(Model);
            ReportViewModel = new ConferenceReportViewModel(Model);

            ViewModels.Add(InfoViewModel);
        }

        protected void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseConferenceDetailsCommand(),
                                  CreateRemoveConferenceCommand(),
                              });
        }

        private ImagedCommandViewModel CreateRemoveConferenceCommand()
        {
            const string tooltip = "Удалить конференцию";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(ApplicationCommands.RemoveConference,
                                              Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseConferenceDetailsCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(NavigationCommands.BrowseConferenceDetails,
                                              Model, tooltip, imageSource);
        }

        #endregion // Class Protected Methods
    }
}