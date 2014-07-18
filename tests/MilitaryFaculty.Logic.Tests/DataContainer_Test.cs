using System.Configuration;
using MilitaryFaculty.Data;
using NUnit.Framework;

namespace MilitaryFaculty.Logic.Tests
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class DataContainer_Test
    {
        [SetUp]
        public void SetUp()
        {
            const string conName = "Current";

            var connectionString = ConfigurationManager.ConnectionStrings[conName].ConnectionString;
            context = new EntityContext(connectionString);
        }

        private EntityContext context;

        [Test]
        public void TestExcel()
        {
            /*
            //To receive profs
            Repository<Professor> profRepository;

            //Register data provider
            var reportDataProvider = new ReportDataProvider(new DataProvidersContainer(
                new BooksDataProvider(new Repository<Book>(context)), 
                new CathedrasDataProvider(new Repository<Cathedra>(context)),
                new ConferencesDataProvider(new Repository<Conference>(context)),
                new ExhibitionsDataProvider(new Repository<Exhibition>(context)),
                new ImprovementSuggestionsDataProvider(new Repository<ImprovementSuggestion>(context)), 
                new ProfessorsDataProvider(profRepository = new Repository<Professor>(context)),
                new PublicationsDataProvider(new Repository<Publication>(context)),
                new ScientificRequestsDataProvider(new Repository<ScientificRequest>(context)),
                new ScientificResearchesDataProvider(new Repository<ScientificResearch>(context)),
                new SynopsesDataProvider(new Repository<Synopsis>(context))
                ));

            //Register formula provider
            const string curDir = @"d:\Git\MilitaryFaculty\MilitaryFaculty.Presentation\Formulas\";
            string fileName = "formulas.xml";
            var filePath = Path.Combine(curDir, fileName);
            var formulaProvider = new FormulaProvider(filePath);
            
            //Register report tables resolver
            var reportTableResolver = new ReportTableResolver();

            fileName = @"Professor\";
            filePath = Path.Combine(curDir, fileName);
            var tableProvider = new ReportTableProvider(filePath);
            reportTableResolver.RegisterTableProvider(typeof(Professor), tableProvider);

            fileName = @"Faculty\";
            filePath = Path.Combine(curDir, fileName);
            tableProvider = new ReportTableProvider(filePath);
            reportTableResolver.RegisterTableProvider(typeof(Cathedra), tableProvider);

            //Creating reports
            var interval = new TimeInterval(new DateTime(2000, 1, 1), DateTime.Now);
            var reportGenerator = new ReportGenerator(reportTableResolver, formulaProvider, reportDataProvider);

            var reports = new List<Report>();
            var prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Кашкаров"));
            reports.Add(reportGenerator.Generate(prof, interval));
            prof = profRepository.Table.Single(p => p.FullName.LastName.StartsWith("Касанин"));
            reports.Add(reportGenerator.Generate(prof, interval));

            var report = reportGenerator.Generate(null, interval);

            //Generation
            var reportingService = new ExcelReportingService();
            reportingService.ExportReport(@"D:\1.xlsx", reports);
            reportingService.ExportReport(@"D:\2.xlsx", report);*/
        }
    }

    // ReSharper restore InconsistentNaming
}