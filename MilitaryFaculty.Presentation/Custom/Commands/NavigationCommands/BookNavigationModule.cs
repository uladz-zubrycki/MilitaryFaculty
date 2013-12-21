using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class BookNavigationModule : BaseNavigationModule
    {
        #region Class Constructors

        public BookNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public override void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand<Professor>(Browse.Book.Add, OnBrowseBookAdd);
            container.RegisterCommand<Book>(Browse.Book.Details, OnBrowseBookDetails);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnBrowseBookAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Book
                        {
                            Author = author
                        };

            ViewModel.WorkWindow = new AddBookViewModel(model);
        }

        private void OnBrowseBookDetails(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            ViewModel.WorkWindow = new BookRootViewModel(book);
        }

        #endregion // Class Private Methods
    }
}