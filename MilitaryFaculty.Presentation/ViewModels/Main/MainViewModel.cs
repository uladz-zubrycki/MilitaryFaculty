using System;
using Autofac;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;
using MilitaryFaculty.Reporting.ReportDomain;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Cathedra> _cathedraRepository;
        private readonly IRepository<Conference> _conferenceRepository;
        private readonly ExcelReportingService _excelService;
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly IRepository<Professor> _professorRepository;
        private readonly IRepository<Publication> _publicationRepository;
        private readonly ViewModel _workWindow;

        public FacultyTreeViewModel FacultyTree { get; private set; }
        public CommandContainer CommandContainer { get; private set; }

        public ViewModel WorkWindow
        {
            get { return _workWindow; }
            internal set
            {
                var oldValue = _workWindow;

                if (SetValue(() => _workWindow, value))
                {
                    OnWorkWindowChanged(oldValue, value);
                }
            }
        }

        public MainViewModel(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _professorRepository = container.Resolve<IRepository<Professor>>();
            _cathedraRepository = container.Resolve<IRepository<Cathedra>>();
            _conferenceRepository = container.Resolve<IRepository<Conference>>();
            _publicationRepository = container.Resolve<IRepository<Publication>>();
            _exhibitionRepository = container.Resolve<IRepository<Exhibition>>();
            _bookRepository = container.Resolve<IRepository<Book>>();
            _excelService = container.Resolve<ExcelReportingService>();

            _workWindow = new StartupViewModel();

            InitFacultyTree();
            InitCommandContainer();
        }

        public event EventHandler<WorkWindowChangedEventArgs> WorkWindowChanged;

        private void OnWorkWindowChanged(ViewModel oldValue, ViewModel newValue)
        {
            var handler = WorkWindowChanged;
            if (handler != null)
            {
                handler(null, new WorkWindowChangedEventArgs(oldValue, newValue));
            }
        }

        private void FacultyTreeOnSelectedItemChanged(object sender, SelectedChangedEventArgs eventArgs)
        {
            var selected = eventArgs.NewValue;
            var model = selected.Model;

            if (model is Cathedra)
            {
                WorkWindow = new CathedraViewModel(model as Cathedra);
            }
            else if (model is Professor)
            {
                WorkWindow = new ProfessorRootViewModel(model as Professor,
                    _conferenceRepository,
                    _publicationRepository,
                    _exhibitionRepository,
                    _bookRepository);
            }
            else
            {
                throw new Exception();
            }
        }

        private void InitFacultyTree()
        {
            FacultyTree = new FacultyTreeViewModel(_professorRepository, _cathedraRepository);
            FacultyTree.SelectedItemChanged += FacultyTreeOnSelectedItemChanged;
        }

        private void InitCommandContainer()
        {
            CommandContainer = new CommandContainer();

            var modules = new ICommandContainerModule[]
                          {
                              new ProfessorCommandModule(_professorRepository),
                              new PublicationCommandModule(_publicationRepository),
                              new ConferenceCommandModule(_conferenceRepository),
                              new ExhibitionCommandModule(_exhibitionRepository),
                              new BookCommandModule(_bookRepository),
                              new CommonNavigationModule(this),
                              new PublicationNavigationModule(this),
                              new ProfessorNavigationModule(this),
                              new ConferenceNavigationModule(this),
                              new BookNavigationModule(this),
                              new ExhibitionNavigationModule(this),
                              new ReportingCommandModule(_excelService/*, Generator? */)
                          };

            modules.ForEach(m => m.RegisterModule(CommandContainer));
        }
    }
}