using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class ProfessorView
    {
        internal class Root : EntityRootViewModel<Professor>
        {
            private readonly IRepository<Book> _bookRepository;
            private readonly IRepository<Dissertation> _dissertationRepository;
            private readonly IRepository<Conference> _conferenceRepository;
            private readonly IRepository<Exhibition> _exhibitionRepository;
            private readonly IRepository<Publication> _publicationRepository;
            private readonly IRepository<InventiveApplication> _inventiveApplicationsRepository;

            public Root(Professor model,
                        IRepository<Conference> conferenceRepository,
                        IRepository<Publication> publicationRepository,
                        IRepository<Exhibition> exhibitionRepository,
                        IRepository<Book> bookRepository,
                        IRepository<Dissertation> dissertationRepository,
                        IRepository<InventiveApplication> inventiveApplicationsRepository)
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

                if (dissertationRepository == null)
                {
                    throw new ArgumentNullException("dissertationRepository");
                }

                if (inventiveApplicationsRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationsRepository");
                }

                _conferenceRepository = conferenceRepository;
                _publicationRepository = publicationRepository;
                _exhibitionRepository = exhibitionRepository;
                _bookRepository = bookRepository;
                _dissertationRepository = dissertationRepository;
                _inventiveApplicationsRepository = inventiveApplicationsRepository;

                HeaderViewModel = new Header(Model);
            }

            protected override IEnumerable<ViewModel<Professor>> GetViewModels()
            {
                return new ViewModel<Professor>[]
                       {
                           new ExtraInfo(Model),
                           new Conferences(Model, _conferenceRepository),
                           new Publications(Model, _publicationRepository),
                           new Exhibitions(Model, _exhibitionRepository),
                           new Books(Model, _bookRepository),
                           new Dissertations(Model, _dissertationRepository), 
                           new InventiveApplications(Model, _inventiveApplicationsRepository), 
                       };
            }
        }

        internal class Header : ViewModel<Professor>
        {
            public Header(Professor model) : base(model) { }

            public string FullName
            {
                get { return Model.FullName.ToString(); }
            }

            public MilitaryRank MilitaryRank
            {
                get { return Model.MilitaryRank; }
                set { SetModelProperty(m => m.MilitaryRank, value); }
            }
        }
     
        internal class Add : AddEntityViewModel<Professor>
        {
            public Add(Professor model): base(model) { }

            public override string Title
            {
                get { return "Добавить преподавателя:"; }
            }

            public override ICommand AddCommand
            {
                get { return GlobalCommands.Add<Professor>(); }
            }

            protected override IEnumerable<ViewModel<Professor>> GetViewModels()
            {
                return new ViewModel<Professor>[]
                       {
                           new MainInfo(Model),
                           new ExtraInfo(Model)
                       };
            }
        }

        internal class ExtraInfo : EntityViewModel<Professor>
        {
            public ExtraInfo(Professor model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Professor>());
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

        internal class MainInfo : EntityViewModel<Professor>
        {
            public MainInfo(Professor model)
                : base(model)
            {
                this.Editable(GlobalCommands.Save<Professor>());
            }

            public override string Title
            {
                get { return "Базовая информация"; }
            }

            [TextProperty(Label = "Имя:")]
            public string FirstName
            {
                get { return Model.FullName.FirstName; }
                set { SetModelProperty(m => m.FullName.FirstName, value); }
            }

            [TextProperty(Label = "Отчество:")]
            public string MiddleName
            {
                get { return Model.FullName.MiddleName; }
                set { SetModelProperty(m => m.FullName.MiddleName, value); }
            }

            [TextProperty(Label = "Фамилия:")]
            public string LastName
            {
                get { return Model.FullName.LastName; }
                set { SetModelProperty(m => m.FullName.LastName, value); }
            }

            [EnumProperty(Label = "Звание:")]
            public MilitaryRank MilitaryRank
            {
                get { return Model.MilitaryRank; }
                set { SetModelProperty(m => m.MilitaryRank, value); }
            }
        }

        internal class Books : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<BookView.ListItem>> _items;

            public Books(Professor model, IRepository<Book> bookRepository)
                : base(model)
            {
                if (bookRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                bookRepository.EntityCreated += OnBookCreated;
                bookRepository.EntityDeleted += OnBookDeleted;
                Commands.Add(CreateAddBookCommand());
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
                get { return Items.Count(vm => vm.Model.BookType == BookType.Schoolbook); }
            }

            public int TutorialsCount
            {
                get { return Items.Count(vm => vm.Model.BookType == BookType.Tutorial); }
            }

            private ImagedCommandViewModel CreateAddBookCommand()
            {
                const string tooltip = "Добавить учебник";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<Book>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new BookView.ListItem(book));
            }

            private void OnBookDeleted(object sender, ModifiedEntityEventArgs<Book> e)
            {
                var book = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(book));
            }
        }

        internal class Conferences : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<ConferenceView.ListItem>> _items;

            public Conferences(Professor model, IRepository<Conference> conferenceRepository)
                : base(model)
            {
                if (conferenceRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                conferenceRepository.EntityCreated += OnConferenceCreated;
                conferenceRepository.EntityDeleted += OnConferenceDeleted;

                Commands.Add(CreateAddConferenceCommand());
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

            private ImagedCommandViewModel CreateAddConferenceCommand()
            {
                const string tooltip = "Добавить конференцию";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<Conference>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new ConferenceView.ListItem(conference));
            }

            private void OnConferenceDeleted(object sender, ModifiedEntityEventArgs<Conference> e)
            {
                var conference = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(conference));
            }
        }

        internal class Exhibitions : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<ExhibitionView.ListItem>> _items;

            public Exhibitions(Professor model, IRepository<Exhibition> exhibitionRepository)
                : base(model)
            {
                if (exhibitionRepository == null)
                {
                    throw new ArgumentNullException("exhibitionRepository");
                }

                _items = Lazy.Create(InitializeItems);

                exhibitionRepository.EntityCreated += OnExhibitionCreated;
                exhibitionRepository.EntityDeleted += OnExhibitionDeleted;

                Commands.Add(CreateAddExhibitionCommand());
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

            private ImagedCommandViewModel CreateAddExhibitionCommand()
            {
                const string tooltip = "Добавить научную выставку";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<Exhibition>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new ExhibitionView.ListItem(exhibition));
            }

            private void OnExhibitionDeleted(object sender, ModifiedEntityEventArgs<Exhibition> e)
            {
                var exhibition = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(exhibition));
            }
        }

        internal class Dissertations : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<DissertationView.ListItem>> _items;

            public Dissertations(Professor model, IRepository<Dissertation> dissertationRepository)
                : base(model)
            {
                if (dissertationRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                dissertationRepository.EntityCreated += OnDissertationCreated;
                dissertationRepository.EntityDeleted += OnDissertationDeleted;
                Commands.Add(CreateAddDissertationCommand());
            }

            public override string Title
            {
                get { return "Защита диссертаций"; }
            }

            public int DoctorDissertationsCount
            {
                get { return Items.Count(vm => vm.Model.TargetAcademicRank == AcademicRank.DoctorOfScience); }
            }

            public int CandidateDissertationsCount
            {
                get { return Items.Count(vm => vm.Model.TargetAcademicRank == AcademicRank.CandidateOfScience); }
            }

            public ObservableCollection<DissertationView.ListItem> Items
            {
                get { return _items.Value; }
            }

            private ImagedCommandViewModel CreateAddDissertationCommand()
            {
                const string tooltip = "Добавить диссертацию";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<Dissertation>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new DissertationView.ListItem(dissertation));
            }

            private void OnDissertationDeleted(object sender, ModifiedEntityEventArgs<Dissertation> e)
            {
                var dissertation = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(dissertation));
            }
        }

        internal class Publications : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<PublicationView.ListItem>> _items;

            public Publications(Professor model, IRepository<Publication> publicationRepository)
                : base(model)
            {
                if (publicationRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationRepository");
                }

                _items = Lazy.Create(InitializeItems);

                publicationRepository.EntityCreated += OnPublicationCreated;
                publicationRepository.EntityDeleted += OnPublicationDeleted;
                Commands.Add(CreateAddPublicationCommand());
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

            private ImagedCommandViewModel CreateAddPublicationCommand()
            {
                const string tooltip = "Добавить публикацию";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<Publication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new PublicationView.ListItem(publication));
            }

            private void OnPublicationDeleted(object sender, ModifiedEntityEventArgs<Publication> e)
            {
                var publication = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(publication));
            }

            private int GetPublicationsCount(PublicationType type)
            {
                return Items.Count(vm => vm.Model.PublicationType == type);
            }
        }
        internal class InventiveApplications : ViewModel<Professor>
        {
            private readonly Lazy<ObservableCollection<InventiveApplicationView.ListItem>> _items;

            public InventiveApplications(Professor model, IRepository<InventiveApplication> inventiveApplicationRepository)
                : base(model)
            {
                if (inventiveApplicationRepository == null)
                {
                    throw new ArgumentNullException("inventiveApplicationRepository");
                }

                _items = Lazy.Create(InitializeItems);

                inventiveApplicationRepository.EntityCreated += OnInventiveApplicationCreated;
                inventiveApplicationRepository.EntityDeleted += OnInventiveApplicationDeleted;
                Commands.Add(CreateAddInventiveApplicationCommand());
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
                    return GetApplicationsCount(InventiveApplicationType.Invention,
                                                InventiveApplicationStatus.Applied);
                }
            }

            public int AppliedUtilityModelApplicationsCount
            {
                get
                {
                    return GetApplicationsCount(InventiveApplicationType.UtilityModel,
                                                InventiveApplicationStatus.Applied);
                }
            }

            public int AcceptedInventionApplicationsCount
            {
                get
                {
                    return GetApplicationsCount(InventiveApplicationType.Invention,
                                                InventiveApplicationStatus.Accepted);
                }
            }

            public int AcceptedUtilityModelApplicationsCount
            {
                get
                {
                    return GetApplicationsCount(InventiveApplicationType.UtilityModel,
                                                InventiveApplicationStatus.Accepted);
                }
            }

            private ImagedCommandViewModel CreateAddInventiveApplicationCommand()
            {
                const string tooltip = "Добавить заявку";
                const string imageSource = @"..\Content\add.png";

                return new ImagedCommandViewModel(GlobalCommands.BrowseAdd<InventiveApplication>(),
                                                  Model,
                                                  tooltip,
                                                  imageSource);
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
                Items.Add(new InventiveApplicationView.ListItem(inventiveApplication));
            }

            private void OnInventiveApplicationDeleted(object sender, ModifiedEntityEventArgs<InventiveApplication> e)
            {
                var inventiveApplication = e.ModifiedEntity;
                Items.RemoveSingle(c => c.Model.Equals(inventiveApplication));
            }

            private int GetApplicationsCount(InventiveApplicationType type, InventiveApplicationStatus status)
            {
                return Items.Count(vm => vm.Model.Type == type &&
                                         vm.Model.Status == status);
            }
        }
    }
}