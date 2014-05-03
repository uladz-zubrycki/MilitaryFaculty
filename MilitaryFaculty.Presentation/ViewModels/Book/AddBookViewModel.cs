using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class AddBookViewModel : AddEntityViewModel<Book>
    {
        public AddBookViewModel(Book model)
            : base(model)
        {
            // Empty
        }

        public override string Title
        {
            get { return "Добавить учебник"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.BookAdd; }
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