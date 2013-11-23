using System;
using System.Windows;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class PublicationCommandModule : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Publication> publicationRepository;

        #endregion // Class Fields

        #region Class Constructors

        public PublicationCommandModule(IRepository<Publication> publicationRepository)
        {
            if (publicationRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            this.publicationRepository = publicationRepository;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand<Publication>(Do.Publication.Add,
                                            OnAddPublication,
                                            CanAddPublication);

            container.RegisterCommand<Publication>(Do.Publication.Update,
                                            OnUpdatePublication,
                                            CanUpdatePublication);

            container.RegisterCommand<Publication>(Do.Publication.Remove,
                                            OnRemovePublication);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnAddPublication(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            publicationRepository.Create(publication);

            Browse.Back.Execute(null, null);
        }

        private bool CanAddPublication(Publication publication)
        {
            // TODO: Validation here
            return true;
        }

        private void OnUpdatePublication(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            publicationRepository.Update(publication);
        }

        private bool CanUpdatePublication(Publication publication)
        {
            // TODO: Validation here
            return true;
        }

        private void OnRemovePublication(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            const string message = "Вы действительно хотите удалить публикацию? Все данные будут утеряны.";
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                publicationRepository.Delete(publication.Id);
            }
        }

        #endregion // Class Private Methods
    }
}