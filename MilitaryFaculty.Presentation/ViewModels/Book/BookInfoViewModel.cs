using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookInfoViewModel : EntityViewModel<Book>
    {
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

        public BookInfoViewModel(Book model)
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