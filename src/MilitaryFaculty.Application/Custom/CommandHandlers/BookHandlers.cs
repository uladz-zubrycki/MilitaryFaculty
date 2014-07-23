using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom.CommandHandlers
{
    public class BookHandlers : EntityCommandModule<Book>
    {
        public BookHandlers(IRepository<Book> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(Book book)
        {
            return ("Вы действительно хотите удалить книгу? " +
                    "Все данные будут утеряны.");
        }
    }

    public class BookNavigation : NavigationCommandModule
    {
        public BookNavigation(MainViewModel viewModel)
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

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<Book>(), OnBrowseBookAdd);
            commands.AddCommand<Book>(GlobalCommands.BrowseDetails<Book>(), OnBrowseBookDetails);
        }

        private void OnBrowseBookAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new Book { Author = author };
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