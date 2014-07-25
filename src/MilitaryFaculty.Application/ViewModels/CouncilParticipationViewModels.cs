using System;
using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class CouncilParticipationView
    {
        internal class Root : EntityRootViewModel<CouncilParticipation>
        {
            public Root(CouncilParticipation model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<CouncilParticipation>> GetViewModels()
            {
                return new[]
                       {
                           new MainInfo(Model)
                       };
            }
        }

        internal class Header : ViewModel
        {
            public override string Title
            {
                get { return "Информация о научной выставке"; }
            }
        }

        internal class Add : AddEntityViewModel<CouncilParticipation>
        {
            public Add(CouncilParticipation model) : base(model) { }

            public override string Title
            {
                get { return "Добавить научную выставку "; }
            }

            protected override IEnumerable<ViewModel<CouncilParticipation>> GetViewModels()
            {
                return new[]
                       {
                           new MainInfo(Model)
                       };
            }
        }

        internal class MainInfo : EntityViewModel<CouncilParticipation>
        {
            public MainInfo(CouncilParticipation model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<CouncilParticipation>());
            }

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

            [DateProperty(Label = "Дата вступления:")]
            public DateTime Date
            {
                get { return Model.Start; }
                set { SetModelProperty(m => m.Start, value); }
            }

            [EnumProperty(Label = "Тип совета:")]
            public CouncilType EventLevel
            {
                get { return Model.Type; }
                set { SetModelProperty(m => m.Type, value); }
            }
        }

        internal class ListItem : ListItemViewModel<CouncilParticipation>
        {
            public ListItem(CouncilParticipation model)
                : base(model)
            {
                TooltipViewModel = new MainInfo(Model);

                this.Removable(GlobalCommands.Remove<CouncilParticipation>());
                this.Browsable(GlobalCommands.BrowseDetails<CouncilParticipation>());
            }

            public override string PrimaryInfo
            {
                get { return Model.Start.ToShortDateString(); }
            }

            public override string SecondaryInfo
            {
                get { return Model.Name; }
            }

            public static ListItem FromModel(CouncilParticipation model)
            {
                return new ListItem(model);
            }
        }
    }
}