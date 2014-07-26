using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.ViewModels.Properties;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class ScienceRankView
    {
        internal class Root : EntityRootViewModel<ScienceRank>
        {
            public Root(ScienceRank model) : base(model)
            {
                HeaderViewModel = new Header();
            }

            protected override IEnumerable<ViewModel<ScienceRank>> GetViewModels()
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
                get { return "Оценка проведения научной работы"; }
            }
        }

        internal class Add : AddEntityViewModel<ScienceRank>
        {
            public Add(ScienceRank model) : base(model) { }

            public override string Title
            {
                get { return "Добавить оценку научной работы "; }
            }

            protected override IEnumerable<ViewModel<ScienceRank>> GetViewModels()
            {
                return new[]
                       {
                           new MainInfo(Model),
                       };
            }
        }

        internal class MainInfo : EntityViewModel<ScienceRank>
        {
            public MainInfo(ScienceRank model) : base(model)
            {
                RegisterProperties();
            }

            public override string Title
            {
                get { return "Оценочные показатели"; }
            }

            [DateProperty(Label = "Дата оценки")]
            public DateTime CreatedAt
            {
                get { return Model.CreatedAt; }
                set { SetModelProperty(m => m.CreatedAt, value); }
            }

            protected void RegisterProperties()
            {
                foreach (var metric in Model.Metrics)
                {
                    Func<object> getter = () => metric.Value;
                    Action<object> setter = value => metric.Value = (int)value;
                    var label = metric.Definition.Name;

                    HasProperty(new IntPropertyViewModel(getter, setter, label));
                }
            }
        }

        internal class ListItem: ListItemViewModel<ScienceRank>
        {
            public ListItem(ScienceRank model) : base(model)
            {
                this.Browsable(GlobalCommands.BrowseDetails<ScienceRank>());
                this.Removable(GlobalCommands.Remove<ScienceRank>());
            }

            public override string PrimaryInfo
            {
                get { return GetDateString(); }
            }
            
            public override string SecondaryInfo
            {
                get { return GetSummaryRankString(); }
            }

            public static ListItem FromModel(ScienceRank model)
            {
                return new ListItem(model);
            }

            private string GetSummaryRankString()
            {
                var sum = Model.Metrics.Sum(metric => metric.Value);
                var result = "Суммарная оценка - {0}".f(sum);
                
                return result;
            }

            private string GetDateString()
            {
                return Model.CreatedAt.ToShortDateString();
            }
        }

        internal class ScienceRankMetric : ViewModel<Domain.ScienceRankMetric>
        {
            public ScienceRankMetric(Domain.ScienceRankMetric model) : base(model) { }

            public string Name
            {
                get { return Model.Definition.Name; }
            }

            public int? MaxValue
            {
                get { return Model.Definition.MaxValue; }
            }

            public bool HasMaxValue
            {
                get { return MaxValue.HasValue; }
            }

            public int Value
            {
                get { return Model.Value; }
                set { SetModelProperty(m => m.Value, value); }
            }

            public static ScienceRankMetric FromModel(Domain.ScienceRankMetric model)
            {
                var viewModel = new ScienceRankMetric(model);

                return viewModel;
            }
        } 
    }
}
