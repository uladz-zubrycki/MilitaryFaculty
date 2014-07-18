using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
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