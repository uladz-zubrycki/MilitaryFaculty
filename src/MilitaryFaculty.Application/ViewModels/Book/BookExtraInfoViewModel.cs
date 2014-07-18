using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class BookExtraInfoViewModel : EntityViewModel<Book>
    {
        public BookExtraInfoViewModel(Book model)
            : base(model)
        {
            this.Editable(Do.BookSave);
        }

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
    }
}