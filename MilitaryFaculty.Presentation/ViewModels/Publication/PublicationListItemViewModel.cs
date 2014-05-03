using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationListItemViewModel : ListItemViewModel<Publication>
    {
        public PublicationListItemViewModel(Publication model)
            : base(model)
        {
            TooltipViewModel = new PublicationExtraInfoViewModel(Model);
            InitCommands();
        }

        public override string PrimaryInfo
        {
            get { return Model.PagesCount + " стр."; }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowsePublicationCommand(),
                                  CreateRemovePublicationCommand()
                              });
        }

        private ImagedCommandViewModel CreateRemovePublicationCommand()
        {
            const string tooltip = "Удалить публикацию";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.PublicationRemove,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ImagedCommandViewModel CreateBrowsePublicationCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.PublicationDetails,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        public static PublicationListItemViewModel FromModel(Publication model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            return new PublicationListItemViewModel(model);
        }
    }
}