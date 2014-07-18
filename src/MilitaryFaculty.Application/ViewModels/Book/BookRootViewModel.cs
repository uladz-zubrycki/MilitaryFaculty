using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class BookRootViewModel : EntityRootViewModel<Book>
    {
        public BookRootViewModel(Book model)
            : base(model)
        {
            HeaderViewModel = new BookHeaderViewModel();
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