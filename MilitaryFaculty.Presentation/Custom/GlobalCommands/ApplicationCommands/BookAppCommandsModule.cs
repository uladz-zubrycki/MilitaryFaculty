using System;
using System.Windows;
using MilitaryFaculty.DataAccess.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class BookAppCommandsModule : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Book> bookRepository;

        #endregion // Class Fields

        #region Class Constructors

        public BookAppCommandsModule(IRepository<Book> bookRepository)
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

            container.RegisterCommand<Book>(GlobalAppCommands.AddBook,
                                            OnAddBook,
                                            CanAddBook);

            container.RegisterCommand<Book>(GlobalAppCommands.UpdateBook,
                                            OnUpdateBook,
                                            CanUpdateBook);

            container.RegisterCommand<Book>(GlobalAppCommands.RemoveBook,
                                            OnRemoveBook);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnAddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            bookRepository.Create(book);

            GlobalNavCommands.BrowseBack.Execute(null, null);
        }

        private bool CanAddBook(Book book)
        {
            // TODO: Validation here
            return true;
        }

        private void OnUpdateBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            bookRepository.Update(book);
        }

        private bool CanUpdateBook(Book book)
        {
            // TODO: Validation here
            return true;
        }

        private void OnRemoveBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            const string message = "Вы действительно хотите удалить учебник? Все данные будут утеряны.";
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                bookRepository.Delete(book.Id);
            }
        }

        #endregion // Class Private Methods
    }
}