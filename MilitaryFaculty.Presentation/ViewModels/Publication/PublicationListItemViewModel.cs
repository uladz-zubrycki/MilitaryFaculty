using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationListItemViewModel : ViewModel<Publication>
    {
        #region Type Static Members

        public static Func<Publication, PublicationListItemViewModel> FromModel()
        {
            return book => new PublicationListItemViewModel(book);
        }  

        #endregion // Type Static Members

        #region Class Properties

        public PublicationInfoViewModel InfoViewModel { get; private set; }
        public PublicationExtraInfoViewModel ExtraInfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationListItemViewModel(Publication model)
            : base(model)
        {
            InitViewModels();
            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private void InitViewModels()
        {
            InfoViewModel = new PublicationInfoViewModel(Model);
            ExtraInfoViewModel = new PublicationExtraInfoViewModel(Model);
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowsePublicationCommand(),
                                  CreateRemovePublicationCommand(),
                              });
        }

        private ImagedCommandViewModel CreateRemovePublicationCommand()
        {
            const string tooltip = "Удалить публикацию";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(ApplicationCommands.RemovePublication,
                                              Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowsePublicationCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(NavigationCommands.BrowsePublicationDetails,
                                              Model, tooltip, imageSource);
        }

        #endregion // Class Private Methods
    }
}