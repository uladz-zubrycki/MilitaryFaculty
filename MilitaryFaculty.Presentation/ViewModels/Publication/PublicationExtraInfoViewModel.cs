using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationExtraInfoViewModel : EntityViewModel<Publication>
    {
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

        public PublicationExtraInfoViewModel(Publication model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Publication>(Do.Publication.Update, Model);
            editCommands.Inject(this);
        }
    }
}