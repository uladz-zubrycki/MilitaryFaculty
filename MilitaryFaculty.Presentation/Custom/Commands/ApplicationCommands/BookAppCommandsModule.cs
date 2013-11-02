using System;
using System.Windows;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class BookAppCommandsModule : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Publication> bookRepository;

        #endregion // Class Fields

        #region Class Constructors

        public BookAppCommandsModule(IRepository<Publication> bookRepository)
        {
            if (bookRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            this.bookRepository = bookRepository;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand<Publication>(ApplicationCommands.AddPublication,
                                            OnAddBook,
                                            CanAddBook);

            container.RegisterCommand<Publication>(ApplicationCommands.UpdatePublication,
                                            OnUpdateBook,
                                            CanUpdateBook);

            container.RegisterCommand<Publication>(ApplicationCommands.RemovePublication,
                                            OnRemoveBook);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnAddBook(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            bookRepository.Create(publication);

            NavigationCommands.BrowseBack.Execute(null, null);
        }

        private bool CanAddBook(Publication publication)
        {
            // TODO: Validation here
            return true;
        }

        private void OnUpdateBook(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            bookRepository.Update(publication);
        }

        private bool CanUpdateBook(Publication publication)
        {
            // TODO: Validation here
            return true;
        }

        private void OnRemoveBook(Publication publication)
        {
            if (publication == null)
            {
                throw new ArgumentNullException("book");
            }

            const string message = "Вы действительно хотите удалить учебник? Все данные будут утеряны.";
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                bookRepository.Delete(publication.Id);
            }
        }

        #endregion // Class Private Methods
    }
}