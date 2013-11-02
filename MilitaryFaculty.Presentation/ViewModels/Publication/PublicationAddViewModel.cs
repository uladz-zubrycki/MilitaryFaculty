using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationAddViewModel : ComplexViewModel<Publication>
    {
        #region Class Properties

        public PublicationViewModel PublicationViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationAddViewModel(Publication model)
            : base(model)
        {
            const string title = "Добавить публикацию";
            
            this.Title = title;

            PublicationViewModel = new PublicationViewModel(Model, EditableViewMode.Edit);
        }

        #endregion // Class Constructors
    }
}