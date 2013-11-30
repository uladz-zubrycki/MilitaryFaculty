using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorPublicationsViewModel : ViewModel<Professor>
    {
        #region Class Fields

        private ObservableCollection<PublicationListItemViewModel> publications;

        #endregion // Class Fields

        #region Class Properties

        public override string Title
        {
            get { return "Публикация результатов научных исследований"; }
        }

        public ObservableCollection<PublicationListItemViewModel> Publications
        {
            get
            {
                if (publications == null)
                {
                    InitPublications();
                }

                return publications;
            } 
        }

        public int MonographsCount
        {
            get
            {
                return GetPublicationsCount(PublicationType.Monograph);
            }
        }

        public int ReviewedArticlesCount
        {
            get
            {
                return GetPublicationsCount(PublicationType.ReviewedArticle);
            }
        }

        public int ArticlesCount
        {
            get
            {
                return GetPublicationsCount(PublicationType.Article);
            }
        }

        public int ThesisesCount
        {
            get
            {
                return GetPublicationsCount(PublicationType.Thesis);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorPublicationsViewModel(Professor model, IPublicationRepository publicationRepository)
            : base(model)
        {
            if (publicationRepository == null)
            {
                throw new ArgumentNullException("publicationRepository");
            }

            publicationRepository.EntityCreated += OnPublicationCreated;
            publicationRepository.EntityDeleted += OnPublicationDeleted;
            Commands.Add(CreateAddPublicationCommand());
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private ImagedCommandViewModel CreateAddPublicationCommand()
        {
            const string tooltip = "Добавить публикацию";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.Publication.Add,
                                              Model, tooltip, imageSource);
        }

        private void InitPublications()
        {
            var converter = PublicationListItemViewModel.FromModel();
            var items = Model.Publications.Select(converter);

            publications = new ObservableCollection<PublicationListItemViewModel>(items);

            publications.CollectionChanged += (sender, args) =>
            {
                OnPropertyChanged("MonographsCount");
                OnPropertyChanged("ReviewedArticlesCount");
                OnPropertyChanged("ArticlesCount");
                OnPropertyChanged("ThesisesCount");
            };
        }

        private void OnPublicationCreated(object sender, ModifiedEntityEventArgs<Publication> e)
        {
            var publication = e.ModifiedEntity;
            Publications.Add(new PublicationListItemViewModel(publication));
        }

        private void OnPublicationDeleted(object sender, ModifiedEntityEventArgs<Publication> e)
        {
            var publication = e.ModifiedEntity;
            Publications.RemoveSingle(c => c.Model.Equals(publication));
        }

        private int GetPublicationsCount(PublicationType type)
        {
            return Publications.Count(vm => vm.Model.PublicationType == type);
        }

        #endregion // Class Private Methods
    }
}