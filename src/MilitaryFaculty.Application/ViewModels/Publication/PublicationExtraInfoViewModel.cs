using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class PublicationExtraInfoViewModel : EntityViewModel<Publication>
    {
        public PublicationExtraInfoViewModel(Publication model)
            : base(model)
        {
            this.Editable(Do.PublicationSave);
        }

        public override string Title
        {
            get { return "Дополнительная информация"; }
        }

        [EnumProperty(Label = "Тип:")]
        public PublicationType PublicationType
        {
            get { return Model.PublicationType; }
            set { SetModelProperty(m => m.PublicationType, value); }
        }
    }
}