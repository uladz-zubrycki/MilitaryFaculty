using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ExhibitionListItemViewModel : ListItemViewModel<Domain.Exhibition>
    {
        public ExhibitionListItemViewModel(Domain.Exhibition model)
            : base(model)
        {
            InitCommands();
            TooltipViewModel = new ExhibitionInfoViewModel(Model);
        }

        public override string PrimaryInfo
        {
            get { return Model.Date.ToShortDateString(); }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseExhibitionDetailsCommand(),
                                  CreateRemoveExhibitionCommand()
                              });
        }

        private ImagedCommandViewModel CreateRemoveExhibitionCommand()
        {
            const string tooltip = "Удалить выставку";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.ExhibitionRemove,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ImagedCommandViewModel CreateBrowseExhibitionDetailsCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.ExhibitionDetails,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        public static ExhibitionListItemViewModel FromModel(Domain.Exhibition model)
        {
            return new ExhibitionListItemViewModel(model);
        }
    }
}