using System;
using Autofac;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Class Fields

        private ViewModel workWindow;

        private readonly IRepository<Professor> professorRepository;
        private readonly IRepository<Cathedra> cathedraRepository;
        private readonly IRepository<Conference> conferenceRepository;
        private readonly IRepository<Publication> publicationRepository;
        private readonly IRepository<Exhibition> exhibitionRepository;
        private readonly IRepository<Book> bookRepository;
        private readonly ExcelReportingService excelReporting;

        #endregion // Class Fields

        #region Class Events

        public event EventHandler<WorkWindowChangedEventArgs> WorkWindowChanged;

        #endregion // Class Events

        #region Class Properties

        public FacultyTreeViewModel FacultyTree { get; private set; }
        public CommandContainer CommandContainer { get; private set; }

        public ViewModel WorkWindow
        {
            get { return workWindow; }
            internal set
            {
                var oldValue = workWindow;

                if (SetValue(() => workWindow, value))
                {
                    OnWorkWindowChanged(oldValue, value);
                }
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public MainViewModel(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            professorRepository = container.Resolve<IRepository<Professor>>();
            cathedraRepository = container.Resolve<IRepository<Cathedra>>();
            conferenceRepository = container.Resolve<IRepository<Conference>>();
            publicationRepository = container.Resolve<IRepository<Publication>>();
            exhibitionRepository = container.Resolve<IRepository<Exhibition>>();
            bookRepository = container.Resolve<IRepository<Book>>();
            excelReporting = container.Resolve<ExcelReportingService>();

            workWindow = new StartupViewModel();

            InitFacultyTree();
            InitCommandContainer();
        }

        #endregion // Class Constructors

        #region Class Private Methods

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
                                                        conferenceRepository,
                                                        publicationRepository,
                                                        exhibitionRepository,
                                                        bookRepository);
            }
            else
            {
                throw new Exception();
            }
        }

        private void InitFacultyTree()
        {
            FacultyTree = new FacultyTreeViewModel(professorRepository, cathedraRepository);
            FacultyTree.SelectedItemChanged += FacultyTreeOnSelectedItemChanged;
        }

        private void InitCommandContainer()
        {
            CommandContainer = new CommandContainer();

            var modules = new ICommandContainerModule[]
                          {
                              new ProfessorCommandModule(professorRepository),
                              new PublicationCommandModule(publicationRepository),
                              new ConferenceCommandModule(conferenceRepository),
                              new ExhibitionCommandModule(exhibitionRepository),
                              new BookCommandModule(bookRepository),
                              new CommonNavigationModule(this),
                              new PublicationNavigationModule(this),
                              new ProfessorNavigationModule(this),
                              new ConferenceNavigationModule(this),
                              new BookNavigationModule(this),
                              new ExhibitionNavigationModule(this),
                              new ReportingCommandModule(excelReporting), 
                          };

            modules.ForEach(m => m.RegisterModule(CommandContainer));
        }

        #endregion // Class Private Methods
    }
}