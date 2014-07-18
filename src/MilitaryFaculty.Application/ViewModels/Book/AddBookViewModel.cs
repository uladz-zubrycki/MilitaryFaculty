using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class AddBookViewModel : AddEntityViewModel<Domain.Book>
    {
        public AddBookViewModel(Domain.Book model)
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

        protected override IEnumerable<ViewModel<Domain.Book>> GetViewModels()
        {
            return new ViewModel<Domain.Book>[]
                   {
                       new BookInfoViewModel(Model),
                       new BookExtraInfoViewModel(Model)
                   };
        }
    }
}