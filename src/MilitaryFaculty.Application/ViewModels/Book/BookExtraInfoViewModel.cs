using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class BookExtraInfoViewModel : EntityViewModel<Domain.Book>
    {
        public BookExtraInfoViewModel(Domain.Book model)
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