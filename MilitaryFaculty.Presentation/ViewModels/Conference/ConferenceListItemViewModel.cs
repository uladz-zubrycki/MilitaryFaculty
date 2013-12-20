using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceListItemViewModel : ListItemViewModel<Conference>
    {
        #region Type Static Members

        public static Func<Conference, ConferenceListItemViewModel> FromModel()
        {
            return conference => new ConferenceListItemViewModel(conference);
        }

        #endregion // Type Static Members

        #region Class Properties

        public override string PrimaryInfo
        {
            get { return Model.Date.ToShortDateString(); }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceListItemViewModel(Conference model)
            : base(model)
        {
            TooltipViewModel = new ConferenceReportViewModel(Model);
            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseConferenceDetailsCommand(),
                                  CreateRemoveConferenceCommand(),
                              });
        }

        #endregion // Class Protected Methods

        #region Class Private Methods

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

        #endregion // Class Private Methods
    }
}