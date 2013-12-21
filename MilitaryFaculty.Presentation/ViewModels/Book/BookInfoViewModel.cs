using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookInfoViewModel : EntityViewModel<Book>
    {
        public override string Title
        {
            get
            {
                return "Основная информация";
            } 
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

        public BookInfoViewModel(Book model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Book>(Do.Book.Update, Model);
            editCommands.Inject(this);
        }
    }
}