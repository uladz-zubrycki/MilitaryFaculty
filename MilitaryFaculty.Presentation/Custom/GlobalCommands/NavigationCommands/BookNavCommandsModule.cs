using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class BookNavCommandsModule : BaseNavCommandsModule
    {
        #region Class Constructors

        public BookNavCommandsModule(MainViewModel viewModel)
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

            container.RegisterCommand<Professor>(GlobalNavCommands.BrowseBookAdd, OnBrowseBookAdd);
            container.RegisterCommand<Book>(GlobalNavCommands.BrowseBookDetails, OnBrowseBookDetails);
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

            ViewModel.WorkWindow = new BookAddViewModel(model);
        }

        private void OnBrowseBookDetails(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            ViewModel.WorkWindow = new BookViewModel(book);
        }

        #endregion // Class Private Methods
    }
}