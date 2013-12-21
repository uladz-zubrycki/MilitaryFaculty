using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookExtraInfoViewModel : EntityViewModel<Book>
    {
        public override string Title
        {
            get { return "Дополнительная информация"; }
        }

        [EnumProperty(Label = "Тип книги:")]
        public BookType BookType
        {
            get { return Model.BookType; }
            set { SetModelProperty(m => m.BookType, value); }
        }

        public BookExtraInfoViewModel(Book model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Book>(Do.Book.Update, Model);
            editCommands.Inject(this);
        }
    }
}