using System;
using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class BookCommandModule : EntityCommandModule<Book>
    {
        protected override RoutedCommand AddCommand
        {
            get { return Do.Book.Add; }
        }

        protected override RoutedCommand UpdateCommand
        {
            get { return Do.Book.Update; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.Book.Remove; }
        }

        public BookCommandModule(IRepository<Book> repository) 
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage()
        {
            return "Вы действительно хотите удалить книгу? Все данные будут утеряны.";
        }
    }
}