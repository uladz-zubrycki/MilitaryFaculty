using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Application.Custom
{
    public class BookCommandModule : EntityCommandModule<Book>
    {
        public BookCommandModule(IRepository<Book> repository)
            : base(repository)
        {
            // Empty
        }

        protected override RoutedCommand AddCommand
        {
            get { return Do.BookAdd; }
        }

        protected override RoutedCommand SaveCommand
        {
            get { return Do.BookSave; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.BookRemove; }
        }

        protected override string GetRemovalMessage()
        {
            return ("Вы действительно хотите удалить книгу? " +
                    "Все данные будут утеряны.");
        }
    }
}