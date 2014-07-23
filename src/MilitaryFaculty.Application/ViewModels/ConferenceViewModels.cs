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
    internal static class ConferenceView
    {
        internal class Root : EntityRootViewModel<Conference>
        {
            public Root(Conference model)
                : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<Conference>> GetViewModels()
            {
                return new ViewModel<Conference>[]
                   {
                       new MainInfo(Model),
                       new Report(Model)
                   };
            }
        }

        internal class Header : ViewModel
        {
            public override string Title
            {
                get { return "Информация о конференции"; }
            }
        }

        internal class Add : AddEntityViewModel<Conference>
        {
            public Add(Conference model): base(model) { }

            public override string Title
            {
                get { return "Добавить конференцию"; }
            }

            public override ICommand AddCommand
            {
                get { return Do.ConferenceAdd; }
            }

            protected override IEnumerable<ViewModel<Conference>> GetViewModels()
            {
                return new ViewModel<Conference>[]
                   {
                       new MainInfo(Model),
                       new Report(Model)
                   };
            }
        }

        internal class MainInfo : EntityViewModel<Conference>
        {
            public MainInfo(Conference model)
                : base(model)
            {
                this.Editable(Do.ConferenceSave);
            }

            public override string Title
            {
                get { return "Базовая информация"; }
            }

            [EnumProperty(Label = "Уровень мероприятия:")]
            public EventLevel ConferenceType
            {
                get { return Model.EventLevel; }
                set { SetModelProperty(m => m.EventLevel, value); }
            }

            [TextProperty(Label = "Название:")]
            public string Name
            {
                get { return Model.Name; }
                set { SetModelProperty(m => m.Name, value); }
            }

            [DateProperty(Label = "Дата проведения")]
            public DateTime Date
            {
                get { return Model.Date; }
                set { SetModelProperty(m => m.Date, value); }
            }
        }

        internal class Report : EntityViewModel<Conference>
        {
            public Report(Conference model)
                : base(model)
            {
                this.Editable(Do.ConferenceSave);
            }

            public override string Title
            {
                get { return "Отчёт о конференции"; }
            }

            [EnumProperty(Label = "Актуальность тематики:")]
            public ConferenceAccordance ThemeActuality
            {
                get { return Model.ConferenceReport.ThemeActuality; }
                set { SetModelProperty(m => m.ConferenceReport.ThemeActuality, value); }
            }

            [EnumProperty(Label = "Правильность организации:")]
            public ConferenceAccordance OrganizationCorrectness
            {
                get { return Model.ConferenceReport.OrganizationCorrectness; }
                set { SetModelProperty(m => m.ConferenceReport.OrganizationCorrectness, value); }
            }

            [EnumProperty(Label = "Наличие отчётных материалов:")]
            public ConferenceAccordance ReportMaterials
            {
                get { return Model.ConferenceReport.ReportMaterials; }
                set { SetModelProperty(m => m.ConferenceReport.ReportMaterials, value); }
            }

            [EnumProperty(Label = "Внедрение результатов:")]
            public ConferenceAccordance ResultsUsage
            {
                get { return Model.ConferenceReport.ResultsUsage; }
                set { SetModelProperty(m => m.ConferenceReport.ResultsUsage, value); }
            }
        }

        internal class ListItem : ListItemViewModel<Conference>
        {
            public ListItem(Conference model)
                : base(model)
            {
                TooltipViewModel = new Report(Model);
                InitCommands();
            }

            public override string PrimaryInfo
            {
                get { return Model.Date.ToShortDateString(); }
            }

            public override string SecondaryInfo
            {
                get { return Model.Name; }
            }

            protected void InitCommands()
            {
                Commands.AddRange(new[]
                              {
                                  CreateBrowseConferenceDetailsCommand(),
                                  CreateRemoveConferenceCommand()
                              });
            }

            private ImagedCommandViewModel CreateRemoveConferenceCommand()
            {
                const string tooltip = "Удалить конференцию";
                const string imageSource = @"..\Content\remove.png";

                return new ImagedCommandViewModel(Do.ConferenceRemove,
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            private ImagedCommandViewModel CreateBrowseConferenceDetailsCommand()
            {
                const string tooltip = "Подробно";
                const string imageSource = @"..\..\Content\details.png";

                return new ImagedCommandViewModel(Browse.ConferenceDetails,
                                                  Model,
                                                  tooltip,
                                                  imageSource);
            }

            public static ListItem FromModel(Conference model)
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