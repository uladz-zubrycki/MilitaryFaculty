﻿using System;
using System.Windows;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ConferenceCommandModule : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Conference> conferenceRepository;

        #endregion // Class Fields

        #region Class Constructors

        public ConferenceCommandModule(IRepository<Conference> conferenceRepository)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            this.conferenceRepository = conferenceRepository;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            container.RegisterCommand<Conference>(Do.Conference.Add,
                                                  OnAddConference,
                                                  CanAddConference);

            container.RegisterCommand<Conference>(Do.Conference.Update,
                                                  OnUpdateConference,
                                                  CanUpdateConference);

            container.RegisterCommand<Conference>(Do.Conference.Remove,
                                                  OnRemoveConference);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnAddConference(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            conferenceRepository.Create(conference);
            Browse.Back.Execute(null, null);
        }

        private bool CanAddConference(Conference conference)
        {
            // TODO: Validation here
            return true;
        }

        private void OnUpdateConference(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            conferenceRepository.Update(conference);
        }

        private bool CanUpdateConference(Conference conference)
        {
            // TODO: Validation here
            return true;
        }

        private void OnRemoveConference(Conference conference)
        {
            if (conference == null)
            {
                throw new ArgumentNullException("conference");
            }

            const string message = "Вы действительно хотите удалить конференцию? Все данные будут утеряны.";
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                conferenceRepository.Delete(conference.Id);
            }
        }

        #endregion // Class Private Methods
    }
}