using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.Menu;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class ProfessorView
    {
        internal class Root : EntityRootViewModel<Person>
        {
            private readonly IRepository<Book> _bookRepository;
            private readonly IRepository<Dissertation> _dissertationRepository;
            private readonly IRepository<Conference> _conferenceRepository;
            private readonly IRepository<CouncilParticipation> _councilParticipationRepository;
            private readonly IRepository<Exhibition> _exhibitionRepository;
            private readonly IRepository<Publication> _publicationRepository;
            private readonly IRepository<InventiveApplication> _inventiveApplicationsRepository;
            private readonly IRepository<EfficiencyProposal> _efficiencyProposalRepository;
            private readonly IRepository<Research> _researchRepository;

            public Root(Person model,
                        IRepository<Conference> conferenceRepository,
                        IRepository<Publication> publicationRepository,
                        IRepository<Exhibition> exhibitionRepository,
                        IRepository<Book> bookRepository,
                        IRepository<CouncilParticipation> councilParticipationRepository,
                        IRepository<Dissertation> dissertationRepository,
                        IRepository<InventiveApplication> inventiveApplicationsRepository, 
                        IRepository<EfficiencyProposal> efficiencyProposalRepository, 
                        IRepository<Research> researchRepository)
                : base(model)
            {
                if (conferenceRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                if (publicationRepository == null)
                {
                    throw new ArgumentNullException("booksRepository");
                }

                if (exhibitionRepository == null)
                {
                    throw new ArgumentNullException("exhibitionRepository");
                }

                if (bookRepository == null)
                {
                    throw new ArgumentNullException("dissertationRepository");
                }

                if (councilParticipationRepository == null)
                {
                    throw new ArgumentNullException("councilParticipationRepository");
                }

                if (dissertationRepository == null)
                {
                    throw new ArgumentNullException("dissertationRepository");
                }

                if (inventiveApplicationsRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationsRepository");
                }

                if (researchRepository == null)
                {
                    throw new ArgumentNullException("researchRepository");
                }

                _conferenceRepository = conferenceRepository;
                _publicationRepository = publicationRepository;
                _exhibitionRepository = exhibitionRepository;
                _bookRepository = bookRepository;
                _councilParticipationRepository = councilParticipationRepository;
                _dissertationRepository = dissertationRepository;
                _inventiveApplicationsRepository = inventiveApplicationsRepository;
                _efficiencyProposalRepository = efficiencyProposalRepository;
                _researchRepository = researchRepository;

                HeaderViewModel = new Header(Model);
            }

            protected override IEnumerable<ViewModel<Person>> GetViewModels()
            {
                return new ViewModel<Person>[]
                       {
                           new ExtraInfo(Model),
                           new Conferences(Model, _conferenceRepository),
                           new Publications(Model, _publicationRepository),
                           new Exhibitions(Model, _exhibitionRepository),
                           new Books(Model, _bookRepository),
                           new CouncilParticipations(Model, _councilParticipationRepository),
                           new Dissertations(Model, _dissertationRepository), 
                           new InventiveApplications(Model, _inventiveApplicationsRepository), 
                           new EfficiencyProposals(Model, _efficiencyProposalRepository),
                           new Researches(Model, _researchRepository), 
                       };
            }
        }

        internal class Header : ViewModel<Person>
        {
            public Header(Person model) : base(model)
            {
                ProfessorMenu = CreateProfessorMenu();
            }

            public MenuViewModel ProfessorMenu { get; private set; }

            public string FullName { get { return GetFullName(); } }

            private string GetFullName()
            {
                return Model.FullName.ToString();
            }

            private MenuViewModel CreateProfessorMenu()
            {
                var menuItems =
                    new[]
                    {
                        new MenuItemViewModel("Создать отчёт", GlobalCommands.GenerateReport, Model),
                        new MenuItemViewModel("Уволить", GlobalCommands.DismissProfessor, Model),
                        new MenuItemViewModel("Удалить", GlobalCommands.Remove<Person>(), Model)
                    };

                return new MenuViewModel(menuItems);
            }
        }
     
        internal class Add : AddEntityViewModel<Person>
        {
            public Add(Person model): base(model) { }

            public override string Title
            {
                get { return "Добавить преподавателя:"; }
            }

            protected override IEnumerable<ViewModel<Person>> GetViewModels()
            {
                return new ViewModel<Person>[]
                       {
                           new MainInfo(Model),
                           new ExtraInfo(Model)
                       };
            }
        }

        internal class MainInfo : EntityViewModel<Person>
        {
            public MainInfo(Person model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Person>());
            }

            public override string Title
            {
                get { return "Основная информация"; }
            }

            [TextProperty(Label = "Имя:")]
            public string FirstName
            {
                get { return Model.FullName.FirstName; }
                set { SetModelProperty(m => m.FullName.FirstName, value); }
            }

            [TextProperty(Label = "Фамилия:")]
            public string LastName
            {
                get { return Model.FullName.LastName; }
                set { SetModelProperty(m => m.FullName.LastName, value); }
            }

            [TextProperty(Label = "Отчество:")]
            public string MiddleName
            {
                get { return Model.FullName.MiddleName; }
                set { SetModelProperty(m => m.FullName.MiddleName, value); }
            }
        }

        internal class ExtraInfo : EntityViewModel<Person>
        {
            public ExtraInfo(Person model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Person>());
            }

            public override string Title
            {
                get { return "Дополнительная информация"; }
            }

            [EnumProperty(Label = "Занимаемая должность:")]
            public JobPosition JobPosition
            {
                get { return Model.JobPosition; }
                set { SetModelProperty(m => m.JobPosition, value); }
            }

            [EnumProperty(Label = "Звание:")]
            public MilitaryRank MilitaryRank
            {
                get { return Model.MilitaryRank; }
                set { SetModelProperty(m => m.MilitaryRank, value); }
            }

            [DateProperty(Label = "Дата трудоустройства:")]
            public DateTime EnrollmentDate
            {
                get { return Model.EnrollmentDate; }
                set { SetModelProperty(m => m.EnrollmentDate, value); }
            }

            [EnumProperty(Label = "Учёное звание:")]
            public AcademicDegree AcademicDegree
            {
                get { return Model.AcademicDegree; }
                set { SetModelProperty(m => m.AcademicDegree, value); }
            }

            [EnumProperty(Label = "Учёная степень:")]
            public AcademicRank AcademicRank
            {
                get { return Model.AcademicRank; }
                set { SetModelProperty(m => m.AcademicRank, value); }
            }
        }

        internal class Books : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<BookView.ListItem>> _items;

            public Books(Person model, IRepository<Book> bookRepository)
                : base(model)
            {
                if (bookRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                bookRepository.EntityCreated += OnBookCreated;
                bookRepository.EntityDeleted += OnBookDeleted;

                this.Addable(GlobalCommands.BrowseAdd<Book>());
            }

            public override string Title
            {
                get { return "Разработка учебников, учебных пособий"; }
            }

            public ObservableCollection<BookView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public int SchoolbooksCount
            {
                get { return CountOf(BookType.Schoolbook); }
            }

            public int TutorialsCount
            {
                get { return CountOf(BookType.Tutorial); }
            }

            private int CountOf(BookType type)
            {
                return Items.Count(vm => vm.Model.BookType == type);
            }

            private ObservableCollection<BookView.ListItem> InitializeItems()
            {
                var books = Model.Books
                                 .Select(BookView.ListItem.FromModel)
                                 .ToList();

                var result = new ObservableCollection<BookView.ListItem>(books);
                result.CollectionChanged += (sender, args) =>
                                            {
                                                //todo property name from expression
                                                OnPropertyChanged("SchoolbooksCount");
                                                OnPropertyChanged("TutorialsCount");
                                            };

                return result;
            }

            private void OnBookCreated(object sender, ModifiedEntityEventArgs<Book> e)
            {
                var book = e.ModifiedEntity;

                if (book.Author.Equals(Model))
                {
                    Items.Add(new BookView.ListItem(book));
                }
            }

            private void OnBookDeleted(object sender, ModifiedEntityEventArgs<Book> e)
            {
                var book = e.ModifiedEntity;

                if (book.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(book));
                }
            }
        }

        internal class Conferences : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<ConferenceView.ListItem>> _items;

            public Conferences(Person model, IRepository<Conference> conferenceRepository)
                : base(model)
            {
                if (conferenceRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                conferenceRepository.EntityCreated += OnConferenceCreated;
                conferenceRepository.EntityDeleted += OnConferenceDeleted;


                this.Addable(GlobalCommands.BrowseAdd<Conference>());
            }

            public ObservableCollection<ConferenceView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public override string Title
            {
                get { return "Участие в конференциях"; }
            }

            public int ConferencesCount
            {
                get { return Items.Count; }
            }

            private ObservableCollection<ConferenceView.ListItem> InitializeItems()
            {
                var conferences = Model.Conferences
                                       .Select(ConferenceView.ListItem.FromModel)
                                       .ToList();

                var result = new ObservableCollection<ConferenceView.ListItem>(conferences);
                result.CollectionChanged += (sender, args) => OnPropertyChanged("ConferencesCount");

                return result;
            }

            private void OnConferenceCreated(object sender, ModifiedEntityEventArgs<Conference> e)
            {
                var conference = e.ModifiedEntity;

                if (conference.Curator.Equals(Model))
                {
                    Items.Add(new ConferenceView.ListItem(conference));
                }
            }

            private void OnConferenceDeleted(object sender, ModifiedEntityEventArgs<Conference> e)
            {
                var conference = e.ModifiedEntity;

                if (conference.Curator.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(conference));
                }
            }
        }

        internal class Exhibitions : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<ExhibitionView.ListItem>> _items;

            public Exhibitions(Person model, IRepository<Exhibition> exhibitionRepository)
                : base(model)
            {
                if (exhibitionRepository == null)
                {
                    throw new ArgumentNullException("exhibitionRepository");
                }

                _items = Lazy.Create(InitializeItems);

                exhibitionRepository.EntityCreated += OnExhibitionCreated;
                exhibitionRepository.EntityDeleted += OnExhibitionDeleted;

                this.Addable(GlobalCommands.BrowseAdd<Exhibition>());
            }

            public override string Title
            {
                get { return "Участие в научных выставках и конкурсах"; }
            }

            public ObservableCollection<ExhibitionView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public int ExhibitionsCount
            {
                get { return Items.Count; }
            }

            private ObservableCollection<ExhibitionView.ListItem> InitializeItems()
            {
                var exhibitions = Model.Exhibitions
                                       .Select(ExhibitionView.ListItem.FromModel)
                                       .ToList();

                var result = new ObservableCollection<ExhibitionView.ListItem>(exhibitions);
                result.CollectionChanged += (sender, args) => OnPropertyChanged("ExhibitionsCount");

                return result;
            }

            private void OnExhibitionCreated(object sender, ModifiedEntityEventArgs<Exhibition> e)
            {
                var exhibition = e.ModifiedEntity;

                if (exhibition.Participant.Equals(Model))
                {
                    Items.Add(new ExhibitionView.ListItem(exhibition));
                }
            }

            private void OnExhibitionDeleted(object sender, ModifiedEntityEventArgs<Exhibition> e)
            {
                var exhibition = e.ModifiedEntity;

                if (exhibition.Participant.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(exhibition));
                }
            }
        }

        internal class CouncilParticipations : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<CouncilParticipationView.ListItem>> _items;

            public CouncilParticipations(Person model, IRepository<CouncilParticipation> councilParticipationRepository)
                : base(model)
            {
                if (councilParticipationRepository == null)
                {
                    throw new ArgumentNullException("councilParticipationRepository");
                }

                _items = Lazy.Create(InitializeItems);

                councilParticipationRepository.EntityCreated += OnCouncilParticipationCreated;
                councilParticipationRepository.EntityDeleted += OnCouncilParticipationDeleted;

                this.Addable(GlobalCommands.BrowseAdd<CouncilParticipation>());
            }

            public override string Title
            {
                get { return "Участие в научных советах"; }
            }

            public ObservableCollection<CouncilParticipationView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public int CouncilParticipationsCount
            {
                get { return Items.Count; }
            }

            private ObservableCollection<CouncilParticipationView.ListItem> InitializeItems()
            {
                var councilParticipations = Model.CouncilsParticipations
                                                 .Select(CouncilParticipationView.ListItem.FromModel)
                                                 .ToList();

                var result = new ObservableCollection<CouncilParticipationView.ListItem>(councilParticipations);
                result.CollectionChanged += (sender, args) => OnPropertyChanged("CouncilParticipationsCount");

                return result;
            }

            private void OnCouncilParticipationCreated(object sender, ModifiedEntityEventArgs<CouncilParticipation> e)
            {
                var councilParticipations = e.ModifiedEntity;

                if (councilParticipations.Participant.Equals(Model))
                {
                    Items.Add(new CouncilParticipationView.ListItem(councilParticipations));
                }
            }

            private void OnCouncilParticipationDeleted(object sender, ModifiedEntityEventArgs<CouncilParticipation> e)
            {
                var councilParticipations = e.ModifiedEntity;

                if (councilParticipations.Participant.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(councilParticipations));
                }
            }
        }

        internal class Dissertations : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<DissertationView.ListItem>> _items;

            public Dissertations(Person model, IRepository<Dissertation> dissertationRepository)
                : base(model)
            {
                if (dissertationRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                dissertationRepository.EntityCreated += OnDissertationCreated;
                dissertationRepository.EntityDeleted += OnDissertationDeleted;

                this.Addable(GlobalCommands.BrowseAdd<Dissertation>());
            }

            public override string Title
            {
                get { return "Защита диссертаций"; }
            }

            public int DoctorDissertationsCount
            {
                get { return CountOf(AcademicRank.DoctorOfScience); }
            }

            public int CandidateDissertationsCount
            {
                get { return CountOf(AcademicRank.CandidateOfScience); }
            }

            public ObservableCollection<DissertationView.ListItem> Items
            {
                get { return _items.Value; }
            }

            private ObservableCollection<DissertationView.ListItem> InitializeItems()
            {
                var dissertations = Model.Dissertations
                                         .Select(DissertationView.ListItem.FromModel)
                                         .ToList();

                var result = new ObservableCollection<DissertationView.ListItem>(dissertations);

                result.CollectionChanged += (sender, args) =>
                {
                    //todo property name from expression
                    OnPropertyChanged("DoctorDissertationsCount");
                    OnPropertyChanged("CandidateDissertationsCount");
                };

                return result;
            }

            private void OnDissertationCreated(object sender, ModifiedEntityEventArgs<Dissertation> e)
            {
                var dissertation = e.ModifiedEntity;

                if (dissertation.Author.Equals(Model))
                {
                    Items.Add(new DissertationView.ListItem(dissertation));
                }
            }

            private void OnDissertationDeleted(object sender, ModifiedEntityEventArgs<Dissertation> e)
            {
                var dissertation = e.ModifiedEntity;

                if (dissertation.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(dissertation));
                }
            }

            private int CountOf(AcademicRank rank)
            {
                return Items.Count(vm => vm.Model.TargetAcademicRank == rank);
            }
        }

        internal class Publications : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<PublicationView.ListItem>> _items;

            public Publications(Person model, IRepository<Publication> publicationRepository)
                : base(model)
            {
                if (publicationRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationRepository");
                }

                _items = Lazy.Create(InitializeItems);

                publicationRepository.EntityCreated += OnPublicationCreated;
                publicationRepository.EntityDeleted += OnPublicationDeleted;

                this.Addable(GlobalCommands.BrowseAdd<Publication>());
            }

            public override string Title
            {
                get { return "Публикация результатов научных исследований"; }
            }

            public ObservableCollection<PublicationView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public int MonographsCount
            {
                get { return GetPublicationsCount(PublicationType.Monograph); }
            }

            public int ReviewedArticlesCount
            {
                get { return GetPublicationsCount(PublicationType.ReviewedArticle); }
            }

            public int ArticlesCount
            {
                get { return GetPublicationsCount(PublicationType.Article); }
            }

            public int ThesisesCount
            {
                get { return GetPublicationsCount(PublicationType.Thesis); }
            }
           
            private ObservableCollection<PublicationView.ListItem> InitializeItems()
            {
                var publications = Model.Publications
                                        .Select(PublicationView.ListItem.FromModel)
                                        .ToList();

                var result = new ObservableCollection<PublicationView.ListItem>(publications);
                result.CollectionChanged += (sender, args) =>
                {
                    //todo property name from expression 
                    OnPropertyChanged("MonographsCount");
                    OnPropertyChanged("ReviewedArticlesCount");
                    OnPropertyChanged("ArticlesCount");
                    OnPropertyChanged("ThesisesCount");
                };

                return result;
            }

            private void OnPublicationCreated(object sender, ModifiedEntityEventArgs<Publication> e)
            {
                var publication = e.ModifiedEntity;

                if (publication.Author.Equals(Model))
                {
                    Items.Add(new PublicationView.ListItem(publication));
                }
            }

            private void OnPublicationDeleted(object sender, ModifiedEntityEventArgs<Publication> e)
            {
                var publication = e.ModifiedEntity;

                if (publication.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(publication));
                }
            }

            private int GetPublicationsCount(PublicationType type)
            {
                return Items.Count(vm => vm.Model.PublicationType == type);
            }
        }

        internal class InventiveApplications : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<InventiveApplicationView.ListItem>> _items;

            public InventiveApplications(Person model,
                                         IRepository<InventiveApplication> inventiveApplicationRepository)
                : base(model)
            {
                if (inventiveApplicationRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationRepository");
                }

                _items = Lazy.Create(InitializeItems);

                inventiveApplicationRepository.EntityCreated += OnInventiveApplicationCreated;
                inventiveApplicationRepository.EntityDeleted += OnInventiveApplicationDeleted;

                this.Addable(GlobalCommands.BrowseAdd<InventiveApplication>());
            }

            public override string Title
            {
                get { return "Результативность изобретательской работы"; }
            }

            public ObservableCollection<InventiveApplicationView.ListItem> Items
            {
                get { return _items.Value; }
            }

            public int AppliedInventionApplicationsCount
            {
                get
                {
                    return CountOf(InventiveApplicationType.Invention,
                                   ApplicationStatus.Applied);
                }
            }

            public int AppliedUtilityModelApplicationsCount
            {
                get
                {
                    return CountOf(InventiveApplicationType.UtilityModel,
                                   ApplicationStatus.Applied);
                }
            }

            public int AcceptedInventionApplicationsCount
            {
                get
                {
                    return CountOf(InventiveApplicationType.Invention,
                                   ApplicationStatus.Accepted);
                }
            }

            public int AcceptedUtilityModelApplicationsCount
            {
                get
                {
                    return CountOf(InventiveApplicationType.UtilityModel,
                                   ApplicationStatus.Accepted);
                }
            }

            private ObservableCollection<InventiveApplicationView.ListItem> InitializeItems()
            {
                var inventiveApplications = Model.InventiveApplications
                                                 .Select(InventiveApplicationView.ListItem.FromModel)
                                                 .ToList();

                var result = new ObservableCollection<InventiveApplicationView.ListItem>(inventiveApplications);

                result.CollectionChanged += (sender, args) =>
                                            {
                                                //todo property name from expression 
                                                OnPropertyChanged("AppliedInventionApplicationsCount");
                                                OnPropertyChanged("AppliedUtilityModelApplicationsCount");
                                                OnPropertyChanged("AcceptedInventionApplicationsCount");
                                                OnPropertyChanged("AcceptedUtilityModelApplicationsCount");
                                            };

                return result;
            }

            private void OnInventiveApplicationCreated(object sender, ModifiedEntityEventArgs<InventiveApplication> e)
            {
                var inventiveApplication = e.ModifiedEntity;

                if (inventiveApplication.Author.Equals(Model))
                {
                    Items.Add(new InventiveApplicationView.ListItem(inventiveApplication));
                }
            }

            private void OnInventiveApplicationDeleted(object sender, ModifiedEntityEventArgs<InventiveApplication> e)
            {
                var inventiveApplication = e.ModifiedEntity;

                if (inventiveApplication.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(inventiveApplication));
                }
            }

            private int CountOf(InventiveApplicationType type, ApplicationStatus status)
            {
                return Items.Count(vm => vm.Model.Type == type &&
                                         vm.Model.Status == status);
            }
        }

        internal class EfficiencyProposals : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<EfficiencyProposalView.ListItem>> _items;

            public EfficiencyProposals(Person model, IRepository<EfficiencyProposal> efficiencyProposalRepository)
                : base(model)
            {
                if (efficiencyProposalRepository == null)
                {
                    throw new ArgumentNullException("researchRepository");
                }

                _items = Lazy.Create(InitializeItems);

                efficiencyProposalRepository.EntityCreated += OnEfficiencyProposalCreated;
                efficiencyProposalRepository.EntityDeleted += OnEfficiencyProposalDeleted;

                this.Addable(GlobalCommands.BrowseAdd<EfficiencyProposal>());
            }

            public override string Title
            {
                get { return "Результативность рационализаторской работы"; }
            }

            public int ProposalsCount
            {
                get { return Items.Count; }
            }

            public ObservableCollection<EfficiencyProposalView.ListItem> Items
            {
                get { return _items.Value; }
            }

            private ObservableCollection<EfficiencyProposalView.ListItem> InitializeItems()
            {
                var efficiencyProposals = Model.EfficiencyProposals
                                               .Select(EfficiencyProposalView.ListItem.FromModel)
                                               .ToList();

                var result = new ObservableCollection<EfficiencyProposalView.ListItem>(efficiencyProposals);
                result.CollectionChanged += (sender, e) => OnPropertyChanged("ProposalsCount");

                return result;
            }

            private void OnEfficiencyProposalCreated(object sender, ModifiedEntityEventArgs<EfficiencyProposal> e)
            {
                var efficiencyProposal = e.ModifiedEntity;

                if (efficiencyProposal.Author.Equals(Model))
                {
                    Items.Add(new EfficiencyProposalView.ListItem(efficiencyProposal));
                }
            }

            private void OnEfficiencyProposalDeleted(object sender, ModifiedEntityEventArgs<EfficiencyProposal> e)
            {
                var efficiencyProposal = e.ModifiedEntity;

                if (efficiencyProposal.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(efficiencyProposal));
                }
            }
        }

        internal class Researches : ViewModel<Person>
        {
            private readonly Lazy<ObservableCollection<ResearchView.ListItem>> _items;

            public Researches(Person model, IRepository<Research> researchRepository)
                : base(model)
            {
                if (researchRepository == null)
                {
                    throw new ArgumentNullException("researchRepository");
                }

                _items = Lazy.Create(InitializeItems);

                researchRepository.EntityCreated += OnResearchCreated;
                researchRepository.EntityDeleted += OnResearchDeleted;

                this.Addable(GlobalCommands.BrowseAdd<Research>());
            }

            public override string Title
            {
                get { return "Проведение НИР"; }
            }

            public int ResearchesCount
            {
                get { return Items.Count; }
            }

            public ObservableCollection<ResearchView.ListItem> Items
            {
                get { return _items.Value; }
            }

            private ObservableCollection<ResearchView.ListItem> InitializeItems()
            {
                var researches = Model.Researches
                                      .Select(ResearchView.ListItem.FromModel)
                                      .ToList();

                var result = new ObservableCollection<ResearchView.ListItem>(researches);
                result.CollectionChanged += (sender, e) => OnPropertyChanged("ResearchesCount");

                return result;
            }

            private void OnResearchCreated(object sender, ModifiedEntityEventArgs<Research> e)
            {
                var research = e.ModifiedEntity;

                if (research.Author.Equals(Model))
                {
                    Items.Add(new ResearchView.ListItem(research));
                }
            }

            private void OnResearchDeleted(object sender, ModifiedEntityEventArgs<Research> e)
            {
                var research = e.ModifiedEntity;

                if (research.Author.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(research));
                }
            }
        }
    }
}