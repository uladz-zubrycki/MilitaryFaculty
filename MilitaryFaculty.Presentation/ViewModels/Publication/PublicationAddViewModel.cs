using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class PublicationAddViewModel : EntityAddViewModel<Publication>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Добавить публикацию";
            }
        }

        public override ICommand AddCommand
        {
            get { return Do.Publication.Add; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationAddViewModel(Publication model)
            : base(model)
        {
            var publicationViewModel = new PublicationViewModel(Model, EditableViewMode.Edit);
            ViewModels.AddRange(publicationViewModel.ViewModels);
        }

        #endregion // Class Constructors
    }
}