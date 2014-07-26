using System;
using System.Collections.Generic;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class EfficiencyProposalView
    {
        internal class Root : EntityRootViewModel<EfficiencyProposal>
        {
            public Root(EfficiencyProposal model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<EfficiencyProposal>> GetViewModels()
            {
                return new ViewModel<EfficiencyProposal>[]
                       {
                           new MainInfo(Model),
                           new ExtraInfo(Model), 
                       };
            }
        }

        internal class Header : ViewModel
        {
            public override string Title
            {
                get { return "Информация о рационализаторском предложении"; }
            }
        }

        internal class Add : AddEntityViewModel<EfficiencyProposal>
        {
            public Add(EfficiencyProposal model) : base(model) { }

            public override string Title
            {
                get { return "Добавить рационализаторское предложение"; }
            }

            protected override IEnumerable<ViewModel<EfficiencyProposal>> GetViewModels()
            {
                return new ViewModel<EfficiencyProposal>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model), 
                   };
            }
        }

        internal class MainInfo : EntityViewModel<EfficiencyProposal>
        {
            public MainInfo(EfficiencyProposal model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<EfficiencyProposal>());
            }

            public override string Title
            {
                get { return "Основная информация"; }
            }

            [TextProperty(Label = "Предложение:")]
            public string Text
            {
                get { return Model.Text; }
                set { SetModelProperty(m => m.Text, value); }
            }

            [DateProperty(Label = "Дата:")]
            public DateTime CreatedAt
            {
                get { return Model.CreatedAt; }
                set { SetModelProperty(m => m.CreatedAt, value); }
            }
        }

        internal class ExtraInfo : EntityViewModel<EfficiencyProposal>
        {
            public ExtraInfo(EfficiencyProposal model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<EfficiencyProposal>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Статус предложения:")]
            public ApplicationStatus Status
            {
                get { return Model.Status; }
                set { SetModelProperty(m => m.Status, value); }
            }
        }

        internal class ListItem : ListItemViewModel<EfficiencyProposal>
        {
            public ListItem(EfficiencyProposal model)
                : base(model)
            {
                TooltipViewModel = new MainInfo(model);

                this.Browsable(GlobalCommands.BrowseDetails<EfficiencyProposal>());
                this.Removable(GlobalCommands.Remove<EfficiencyProposal>());
            }

            public override string PrimaryInfo
            {
                get { return GetCreationDate(); }
            }

            public override string SecondaryInfo
            {
                get { return Model.Text; }
            }

            public static ListItem FromModel(EfficiencyProposal model)
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model");
                }

                return new ListItem(model);
            }

            private string GetCreationDate()
            {
                return Model.CreatedAt.ToShortDateString();
            } 
        }
    }
}

