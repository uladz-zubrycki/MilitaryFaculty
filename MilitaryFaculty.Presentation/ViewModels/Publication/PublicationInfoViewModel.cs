using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationInfoViewModel : EntityViewModel<Publication>
    {
        #region Class Properties

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

        [TextProperty(Label = "Количество страниц:")]
        public int PagesCount
        {
            get { return Model.PagesCount; }
            set { SetModelProperty(m => m.PagesCount, value); }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationInfoViewModel(Publication model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Publication>(Do.Publication.Update, Model);
            editCommands.Inject(this);
        }

        #endregion // Class Constructors
    }
}