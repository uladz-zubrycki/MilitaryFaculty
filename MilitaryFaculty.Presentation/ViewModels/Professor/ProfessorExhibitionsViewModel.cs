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
        private ObservableCollection<ExhibitionListItemViewModel> _exhibitions;

        public override string Title
        {
            get { return "Участие в научных выставках и конкурсах"; }
        }

        public ObservableCollection<ExhibitionListItemViewModel> Exhibitions
        {
            get
            {
                if (_exhibitions == null)
                {
                    InitExhibitions();
                }

                return _exhibitions;
            }
        }

        public int ExhibitionsCount
        {
            get { return Exhibitions.Count; }
        }

        public ProfessorExhibitionsViewModel(Professor model, IRepository<Exhibition> exhibitionRepository)
            : base(model)
        {
            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            exhibitionRepository.EntityCreated += OnExhibitionCreated;
            exhibitionRepository.EntityDeleted += OnExhibitionDeleted;

            Commands.Add(CreateAddExhibitionCommand());
        }

        private ImagedCommandViewModel CreateAddExhibitionCommand()
        {
            const string tooltip = "Добавить научную выставку";
            const string imageSource = @"..\Content\add.png";

            return new ImagedCommandViewModel(Browse.Exhibition.Add,
                Model, tooltip, imageSource);
        }

        private void InitExhibitions()
        {
            var converter = ExhibitionListItemViewModel.FromModel();
            var items = Model.Exhibitions.Select(converter);

            _exhibitions = new ObservableCollection<ExhibitionListItemViewModel>(items);
            _exhibitions.CollectionChanged += (sender, args) => { OnPropertyChanged("ExhibitionsCount"); };
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