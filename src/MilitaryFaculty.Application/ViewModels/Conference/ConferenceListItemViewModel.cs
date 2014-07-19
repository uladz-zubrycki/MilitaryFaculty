﻿using System;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Common;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ConferenceListItemViewModel : ListItemViewModel<Domain.Conference>
    {
        public ConferenceListItemViewModel(Domain.Conference model)
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

        public static ConferenceListItemViewModel FromModel(Domain.Conference model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new ConferenceListItemViewModel(model);
        }
    }
}