using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorExhibitionsViewModel : ViewModel<Professor>
    {
        private Lazy<ObservableCollection<ExhibitionListItemViewModel>> _exhibitions;

        public ProfessorExhibitionsViewModel(Professor model, IRepository<Exhibition> exhibitionRepository)
            : base(model)
        {
            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            _exhibitions = Lazy.Create(CreateExhibitionsViewModel);

            exhibitionRepository.EntityCreated += OnExhibitionCreated;
            exhibitionRepository.EntityDeleted += OnExhibitionDeleted;

            Commands.Add(CreateAddExhibitionCommand());
        }

        public override string Title
        {
            get { return "Участие в научных выставках и конкурсах"; }
        }

        public ObservableCollection<ExhibitionListItemViewModel> Exhibitions
        {
            get { return _exhibitions.Value; }
        }

        public int ExhibitionsCount
        {
            get { return Exhibitions.Count; }
        }

        private ImagedCommandViewModel CreateAddExhibitionCommand()
        {
            const string tooltip = "Добавить научную выставку";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.ExhibitionAdd,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private ObservableCollection<ExhibitionListItemViewModel> CreateExhibitionsViewModel()
        {
            var exhibitions = Model.Exhibitions
                                   .Select(ExhibitionListItemViewModel.FromModel)
                                   .ToList();

            var result = new ObservableCollection<ExhibitionListItemViewModel>(exhibitions);
            result.CollectionChanged += (sender, args) => OnPropertyChanged("ExhibitionsCount");

            return result;
        }

        private void OnExhibitionCreated(object sender, ModifiedEntityEventArgs<Exhibition> e)
        {
            var exhibition = e.ModifiedEntity;
            Exhibitions.Add(new ExhibitionListItemViewModel(exhibition));
        }

        private void OnExhibitionDeleted(object sender, ModifiedEntityEventArgs<Exhibition> e)
        {
            var exhibition = e.ModifiedEntity;
            Exhibitions.RemoveSingle(c => c.Model.Equals(exhibition));
        }
    }
}