using System;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorExhibitionsViewModel : ViewModel<Domain.Professor>
    {
        private readonly Lazy<ObservableCollection<ExhibitionListItemViewModel>> _exhibitions;

        public ProfessorExhibitionsViewModel(Domain.Professor model, IRepository<Domain.Exhibition> exhibitionRepository)
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

        private void OnExhibitionCreated(object sender, ModifiedEntityEventArgs<Domain.Exhibition> e)
        {
            var exhibition = e.ModifiedEntity;
            Exhibitions.Add(new ExhibitionListItemViewModel(exhibition));
        }

        private void OnExhibitionDeleted(object sender, ModifiedEntityEventArgs<Domain.Exhibition> e)
        {
            var exhibition = e.ModifiedEntity;
            Exhibitions.RemoveSingle(c => c.Model.Equals(exhibition));
        }
    }
}