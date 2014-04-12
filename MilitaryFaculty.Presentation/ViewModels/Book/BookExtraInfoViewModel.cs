using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

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
            // Empty
        }

        protected override IEnumerable<IViewBehaviour> GetBehaviours()
        {
            yield return new EditableViewBehaviour<Book>(Do.Book.Update);
        }
    }
}