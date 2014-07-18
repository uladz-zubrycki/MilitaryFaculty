using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class BookInfoViewModel : EntityViewModel<Domain.Book>
    {
        public BookInfoViewModel(Domain.Book model)
            : base(model)
        {
            this.Editable(Do.BookSave);
        }

        public override string Title
        {
            get { return "Основная информация"; }
        }

        [TextProperty(Label = "Название:")]
        public string Name
        {
            get { return Model.Name; }
            set { SetModelProperty(m => m.Name, value); }
        }

        [IntProperty(Label = "Количество страниц:")]
        public int PagesCount
        {
            get { return Model.PagesCount; }
            set { SetModelProperty(m => m.PagesCount, value); }
        }
    }
}