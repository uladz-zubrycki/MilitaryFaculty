using System;
using System.Collections.Generic;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class DissertationView
    {
        internal class Root : EntityRootViewModel<Dissertation>
        {
            public Root(Dissertation model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<Dissertation>> GetViewModels()
            {
                return new ViewModel<Dissertation>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class Header : ViewModel
        {
            public override string Title
            {
                get { return "Информация о диссертации"; }
            }
        }

        internal class Add : AddEntityViewModel<Dissertation>
        {
            public Add(Dissertation model) : base(model) { }

            public override string Title
            {
                get { return "Добавить диссертацию"; }
            }

            protected override IEnumerable<ViewModel<Dissertation>> GetViewModels()
            {
                return new ViewModel<Dissertation>[]
                   {
                       new MainInfo(Model),
                       new ExtraInfo(Model)
                   };
            }
        }

        internal class MainInfo : EntityViewModel<Dissertation>
        {
            public MainInfo(Dissertation model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Dissertation>());
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

            [DateProperty(Label = "Дата защиты:")]
            public DateTime CreatedAt
            {
                get { return Model.CreatedAt; }
                set { SetModelProperty(m => m.CreatedAt, value); }
            }
        }

        internal class ExtraInfo : EntityViewModel<Dissertation>
        {
            public ExtraInfo(Dissertation model) : base(model)
            {
                this.Editable(GlobalCommands.Save<Dissertation>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Научная степень:")]
            public AcademicRank TargetAcademiceRank
            {
                get { return Model.TargetAcademicRank; }
                set { SetModelProperty(m => m.TargetAcademicRank, value); }
            }
            
            [EnumProperty(Label = "Состояние работы:")]
            public DissertationState State
            {
                get { return Model.State; }
                set { SetModelProperty(m => m.State, value); }
            }
        }

        internal class ListItem : ListItemViewModel<Dissertation>
        {
            public ListItem(Dissertation model)
                : base(model)
            {
                TooltipViewModel = new ExtraInfo(Model);

                this.Browsable(GlobalCommands.BrowseDetails<Dissertation>());
                this.Removable(GlobalCommands.Remove<Dissertation>());
            }

            public override string PrimaryInfo
            {
                get { return GetCreationDate(); }
            }

            public override string SecondaryInfo
            {
                get { return Model.Name; }
            }
            
            public static ListItem FromModel(Dissertation model)
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
