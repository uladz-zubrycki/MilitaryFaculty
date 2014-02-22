using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class AddBookViewModel : AddEntityViewModel<Book>
    {
        public override string Title
        {
            get { return "Добавить учебник"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Book.Add; }
        }

        public AddBookViewModel(Book model)
            : base(model)
        {
            // Empty
        }

        protected override IEnumerable<ViewModel<Book>> GetViewModels()
        {
            return new ViewModel<Book>[]
                   {
                       new BookInfoViewModel(Model),
                       new BookExtraInfoViewModel(Model)
                   };
        }
    }
}