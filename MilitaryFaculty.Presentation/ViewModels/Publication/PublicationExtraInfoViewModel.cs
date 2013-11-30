using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationExtraInfoViewModel : ViewModel<Publication>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Дополнительная информация";
            }
        }

        public PublicationType PublicationType
        {
            get { return Model.PublicationType; }
            set
            {
                SetModelProperty(m => m.PublicationType, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationExtraInfoViewModel(Publication model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Publication>(Do.Publication.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}