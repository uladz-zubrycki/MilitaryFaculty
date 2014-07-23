﻿using System;
using Autofac;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.Custom.CommandHandlers;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.TreeView.Events;
using MilitaryFaculty.Reporting;
using MilitaryFaculty.Reporting.Excel;

namespace MilitaryFaculty.Application.ViewModels
{
    public class WorkWindowChangedEventArgs : EventArgs
    {
        public ViewModel OldValue { get; set; }
        public ViewModel NewValue { get; set; }

        public WorkWindowChangedEventArgs(ViewModel oldValue, ViewModel newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public class MainViewModel : ViewModel
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Cathedra> _cathedraRepository;
        private readonly IRepository<Conference> _conferenceRepository;
        private readonly IExcelReportingService _excelService;
        private readonly IReportGenerator _reportGenerator;
        private readonly IRepository<Exhibition> _exhibitionRepository;
        private readonly IRepository<Professor> _professorRepository;
        private readonly IRepository<Publication> _publicationRepository;
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

            _professorRepository = container.Resolve<IRepository<Professor>>();
            _cathedraRepository = container.Resolve<IRepository<Cathedra>>();
            _conferenceRepository = container.Resolve<IRepository<Conference>>();
            _publicationRepository = container.Resolve<IRepository<Publication>>();
            _exhibitionRepository = container.Resolve<IRepository<Exhibition>>();
            _bookRepository = container.Resolve<IRepository<Book>>();
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

            if (model is Cathedra)
            {
                WorkWindow = new CathedraView.Root(model as Cathedra);
            }
            else if (model is Professor)
            {
                WorkWindow = new ProfessorView.Root(model as Professor,
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
                              new ProfessorHandlers(_professorRepository),
                              new PublicationHandlers(_publicationRepository),
                              new ConferenceHandlers(_conferenceRepository),
                              new ExhibitionHandlers(_exhibitionRepository),
                              new BookHandlers(_bookRepository),
                              new NavigationHistory(this),
                              new PublicationNavigation(this),
                              new ProfessorNavigation(this),
                              new ConferenceNavigation(this),
                              new BookNavigation(this),
                              new ExhibitionNavigation(this),
                              new ReportingHandlers(_excelService, _reportGenerator)
                          };

            modules.ForEach(m => m.LoadModule(RoutedCommands));
        }
    }
}