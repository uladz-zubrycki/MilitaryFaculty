using System;
using Autofac;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class MainViewModel : ViewModel
    {
        #region Class Fields

        private ViewModel workWindow;

        private readonly IProfessorRepository professorRepository;
        private readonly ICathedraRepository cathedraRepository;
        private readonly IConferenceRepository conferenceRepository;
        private readonly IPublicationRepository bookRepository;

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
                
                if (SetValue(() => this.workWindow, value))
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

            professorRepository = container.Resolve<IProfessorRepository>();
            cathedraRepository = container.Resolve<ICathedraRepository>();
            conferenceRepository = container.Resolve<IConferenceRepository>();
            bookRepository = container.Resolve<IPublicationRepository>();

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
                WorkWindow = new ProfessorViewModel(model as Professor, conferenceRepository, bookRepository);
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
                              new PublicationCommandModule(bookRepository),
                              new ConferenceCommandModule(conferenceRepository),
                              new CommonNavigationModule(this),
                              new PublicationNavigationModule(this),
                              new ProfessorNavigationModule(this),
                              new ConferenceNavigationModule(this)
                          };

            foreach (var module in modules)
            {
                module.RegisterModule(CommandContainer);
            }
        }

        #endregion // Class Private Methods
    }
}