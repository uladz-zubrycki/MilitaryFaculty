using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationViewModel : ComplexViewModel<Publication>
    {
        #region Class Properties

        public PublicationInfoViewModel InfoViewModel { get; private set; }
        public PublicationExtraInfoViewModel ExtraInfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationViewModel(Publication model)
            : this(model, EditableViewMode.Display)
        {
            // Empty
        }

        public PublicationViewModel(Publication model, EditableViewMode mode)
            : base(model)
        {
            const string title = "Информация о публикации";

            Title = title;
            InitViewModels(mode);
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels(EditableViewMode mode)
        {
            InfoViewModel = new PublicationInfoViewModel(Model, mode);
            ExtraInfoViewModel = new PublicationExtraInfoViewModel(Model, mode);

            ViewModels.Add(InfoViewModel);
            ViewModels.Add(ExtraInfoViewModel);
        }

        #endregion // Class Protected Methods
    }
}