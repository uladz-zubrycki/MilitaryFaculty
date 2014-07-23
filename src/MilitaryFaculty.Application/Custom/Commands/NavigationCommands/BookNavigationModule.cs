using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom
{
    public class BookNavigationModule : BaseNavigationModule
    {
        public BookNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand<Professor>(Browse.BookAdd, OnBrowseBookAdd);
            commands.AddCommand<Book>(Browse.BookDetails, OnBrowseBookDetails);
        }

        private void OnBrowseBookAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Book {Author = author};
            ViewModel.WorkWindow = new BookView.Add(model);
        }

        private void OnBrowseBookDetails(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException("book");
            }

            ViewModel.WorkWindow = new BookView.Root(book);
        }
    }
}