using System;
using System.Linq;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class ScienceRankHandlers : EntityCommandModule<ScienceRank>
    {
        private readonly IRepository<ScienceRankMetric> _scienceRankMetricRepository;

        public ScienceRankHandlers(IRepository<ScienceRank> scienceRankRepository,
                                   IRepository<ScienceRankMetric> scienceRankMetricRepository)
            : base(scienceRankRepository)
        {
            if (scienceRankMetricRepository == null)
            {
                throw new ArgumentNullException("scienceRankMetricRepository");
            }

            _scienceRankMetricRepository = scienceRankMetricRepository;
        }

        protected override string GetRemovalMessage(ScienceRank book)
        {
            return ("Вы действительно хотите удалить оценку проведения научной работы? " +
                    "Все данные будут утеряны.");
        }

        //protected override void RemoveEntity(ScienceRank entity)
        //{
        //    foreach (var metric in entity.Metrics)
        //    {
        //        _scienceRankMetricRepository.Delete(metric.Id);
        //    }

        //    Repository.Delete(entity.Id);
        //}
    }

    public class ScienceRankNavigation : NavigationCommandModule
    {
        private readonly IRepository<ScienceRankMetricDefinition> _scienceRankMetricDefinitionRepository;

        public ScienceRankNavigation(MainViewModel viewModel, IRepository<ScienceRankMetricDefinition> scienceRankMetricDefinitionRepository)
            : base(viewModel)
        {
            _scienceRankMetricDefinitionRepository = scienceRankMetricDefinitionRepository;
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand<Cathedra>(GlobalCommands.BrowseAdd<ScienceRank>(), OnBrowseScienceRankAdd);
            commands.AddCommand<ScienceRank>(GlobalCommands.BrowseDetails<ScienceRank>(), OnBrowseScienceRankDetails);
        }

        private void OnBrowseScienceRankAdd(Cathedra cathedra)
        {
            if (cathedra == null)
            {
                throw new ArgumentNullException("cathedra");
            }

            Func<ScienceRankMetricDefinition, ScienceRankMetric> createMetric =
                def => new ScienceRankMetric {Definition = def, Value = 0};

            var metricDefinitions = _scienceRankMetricDefinitionRepository.Table.ToList();
            var metrics = metricDefinitions.Select(createMetric).ToList();
            var model = new ScienceRank { Cathedra = cathedra, Metrics = metrics };

            ViewModel.WorkWindow = new ScienceRankView.Add(model);
        }

        private void OnBrowseScienceRankDetails(ScienceRank scienceRank)
        {
            if (scienceRank == null)
            {
                throw new ArgumentNullException("scienceRank");
            }

            ViewModel.WorkWindow = new ScienceRankView.Root(scienceRank);
        }
    }
}