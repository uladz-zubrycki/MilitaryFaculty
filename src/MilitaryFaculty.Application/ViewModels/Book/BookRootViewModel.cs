using System.Collections.Generic;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class BookRootViewModel : EntityRootViewModel<Domain.Book>
    {
        public BookRootViewModel(Domain.Book model)
            : base(model)
        {
            HeaderViewModel = new BookHeaderViewModel();
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