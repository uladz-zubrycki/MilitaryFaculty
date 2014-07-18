using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class PublicationInfoViewModel : EntityViewModel<Publication>
    {
        public PublicationInfoViewModel(Publication model)
            : base(model)
        {
            this.Editable(Do.PublicationSave);
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