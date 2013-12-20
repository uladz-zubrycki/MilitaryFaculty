using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationListItemViewModel : ListItemViewModel<Publication>
    {
        #region Type Static Members

        public static Func<Publication, PublicationListItemViewModel> FromModel()
        {
            return book => new PublicationListItemViewModel(book);
        }

        #endregion // Type Static Members

        #region Class Properties

        public override string PrimaryInfo
        {
            get { return Model.PagesCount + " стр."; }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationListItemViewModel(Publication model)
            : base(model)
        {
            TooltipViewModel = new PublicationExtraInfoViewModel(Model);
            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Private Methods

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

            return new ImagedCommandViewModel(Do.Publication.Remove,
                                              Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowsePublicationCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.Publication.Details,
                                              Model, tooltip, imageSource);
        }

        #endregion // Class Private Methods

        #region Overrides of ListItemViewModel<Publication>

      
        #endregion
    }
}