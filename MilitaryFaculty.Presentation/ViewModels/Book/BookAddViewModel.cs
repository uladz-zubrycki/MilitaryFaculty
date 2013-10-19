using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookAddViewModel : ComplexViewModel<Book>
    {
        #region Class Properties

        public BookViewModel BookViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public BookAddViewModel(Book model)
            : base(model)
        {
            const string title = "Добавить учебник";
            
            this.Title = title;

            BookViewModel = new BookViewModel(Model, EditViewMode.Edit);
        }

        #endregion // Class Constructors
    }
}