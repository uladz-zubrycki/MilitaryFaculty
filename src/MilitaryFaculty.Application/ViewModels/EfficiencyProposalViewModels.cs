﻿using System;
using System.Collections.Generic;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
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

        internal class ListItem : ListItemViewModel<EfficiencyProposal>
        {
            public ListItem(EfficiencyProposal model)
                : base(model)
            {
                TooltipViewModel = new MainInfo(model);

                this.Removable(GlobalCommands.Remove<EfficiencyProposal>());
                this.Browsable(GlobalCommands.BrowseDetails<EfficiencyProposal>());
            }

            public override string PrimaryInfo
            {
                get { return Model.CreatedAt.ToShortDateString(); }
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
        }
    }
}
