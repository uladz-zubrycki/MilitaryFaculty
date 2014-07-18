using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorPublicationsViewModel : ViewModel<Domain.Professor>
    {
        private readonly Lazy<ObservableCollection<PublicationListItemViewModel>> _publications;

        public ProfessorPublicationsViewModel(Domain.Professor model,
                                              IRepository<Domain.Publication> publicationRepository)
            : base(model)
        {
            if (publicationRepository == null)
            {
                throw new ArgumentNullException("publicationRepository");
            }

            _publications = Lazy.Create(CreatePublicationsViewModel);

            publicationRepository.EntityCreated += OnPublicationCreated;
            publicationRepository.EntityDeleted += OnPublicationDeleted;
            Commands.Add(CreateAddPublicationCommand());
        }

        public override string Title
        {
            get { return "Публикация результатов научных исследований"; }
        }

        public ObservableCollection<PublicationListItemViewModel> Publications
        {
            get { return _publications.Value; }
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

            return new ImagedCommandViewModel(Browse.PublicationAdd,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ObservableCollection<PublicationListItemViewModel> CreatePublicationsViewModel()
        {
            var publications = Model.Publications
                                    .Select(PublicationListItemViewModel.FromModel)
                                    .ToList();

            var result = new ObservableCollection<PublicationListItemViewModel>(publications);
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

        private void OnPublicationCreated(object sender, ModifiedEntityEventArgs<Domain.Publication> e)
        {
            var publication = e.ModifiedEntity;
            Publications.Add(new PublicationListItemViewModel(publication));
        }

        private void OnPublicationDeleted(object sender, ModifiedEntityEventArgs<Domain.Publication> e)
        {
            var publication = e.ModifiedEntity;
            Publications.RemoveSingle(c => c.Model.Equals(publication));
        }

        private int GetPublicationsCount(PublicationType type)
        {
            return Publications.Count(vm => vm.Model.PublicationType == type);
        }
    }
}