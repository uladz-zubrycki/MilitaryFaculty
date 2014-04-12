using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionListItemViewModel : ListItemViewModel<Exhibition>
    {
        public override string PrimaryInfo
        {
            get { return Model.Date.ToShortDateString(); }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        public ExhibitionListItemViewModel(Exhibition model)
            : base(model)
        {
            InitCommands();
            TooltipViewModel = new ExhibitionInfoViewModel(Model);
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

            return new ImagedCommandViewModel(Do.Exhibition.Remove,
                Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseExhibitionDetailsCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.Exhibition.Details,
                Model, tooltip, imageSource);
        }

        public static Func<Exhibition, ExhibitionListItemViewModel> FromModel()
        {
            return conference => new ExhibitionListItemViewModel(conference);
        }
    }
}