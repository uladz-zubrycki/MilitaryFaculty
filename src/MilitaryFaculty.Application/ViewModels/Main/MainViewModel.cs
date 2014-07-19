using System;
using Autofac;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.TreeView.Events;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Application.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly IRepository<Domain.Book> _bookRepository;
        private readonly IRepository<Domain.Cathedra> _cathedraRepository;
        private readonly IRepository<Domain.Conference> _conferenceRepository;
        private readonly IExcelReportingService _excelService;
        private readonly IReportGenerator _reportGenerator;
        private readonly IRepository<Domain.Exhibition> _exhibitionRepository;
        private readonly IRepository<Domain.Professor> _professorRepository;
        private readonly IRepository<Domain.Publication> _publicationRepository;
        private readonly ViewModel _workWindow;

        public FacultyTreeViewModel FacultyTree { get; private set; }
        public RoutedCommands RoutedCommands { get; private set; }

        public event EventHandler<WorkWindowChangedEventArgs> WorkWindowChanged;

        public MainViewModel(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _professorRepository = container.Resolve<IRepository<Domain.Professor>>();
            _cathedraRepository = container.Resolve<IRepository<Domain.Cathedra>>();
            _conferenceRepository = container.Resolve<IRepository<Domain.Conference>>();
            _publicationRepository = container.Resolve<IRepository<Domain.Publication>>();
            _exhibitionRepository = container.Resolve<IRepository<Domain.Exhibition>>();
            _bookRepository = container.Resolve<IRepository<Domain.Book>>();
            _excelService = container.Resolve<IExcelReportingService>();
            _reportGenerator = container.Resolve<IReportGenerator>();

            _workWindow = new StartupViewModel();

            InitFacultyTree();
            InitRoutedCommands();
        }

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

            if (model is Domain.Cathedra)
            {
                WorkWindow = new CathedraViewModel(model as Domain.Cathedra);
            }
            else if (model is Domain.Professor)
            {
                WorkWindow = new ProfessorRootViewModel(model as Domain.Professor,
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

        private void InitRoutedCommands()
        {
            RoutedCommands = new RoutedCommands();

            //todo get module from container
            var modules = new ICommandModule[]
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
                              new ReportingCommandModule(_excelService, _reportGenerator)
                          };

            modules.ForEach(m => m.LoadModule(RoutedCommands));
        }
    }
}