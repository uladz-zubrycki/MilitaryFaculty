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
    public class ProfessorExhibitionsViewModel : EntityViewModel<Professor>
    {
        #region Class Fields

        private ObservableCollection<ExhibitionListItemViewModel> exhibitions;

        #endregion // Class Fields

        #region Class Properties

        public override string Title
        {
            get { return "Участие в научных выставках и конкурсах"; }
        }

        public ObservableCollection<ExhibitionListItemViewModel> Exhibitions
        {
            get
            {
                if (exhibitions == null)
                {
                    InitExhibitions();
                }

                return exhibitions;
            }
        }

        public int ExhibitionsCount
        {
            get { return Exhibitions.Count; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorExhibitionsViewModel(Professor model, IExhibitionRepository exhibitionRepository)
            : base(model)
        {
            if (exhibitionRepository == null)
            {
                throw new ArgumentNullException("exhibitionRepository");
            }

            exhibitionRepository.EntityCreated += OnExhibitionCreated;
            exhibitionRepository.EntityDeleted += OnExhibitionDeleted;

            Commands.Add(CreateAddConferenceCommand());
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private ImagedCommandViewModel CreateAddConferenceCommand()
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

            exhibitions = new ObservableCollection<ExhibitionListItemViewModel>(items);
            exhibitions.CollectionChanged += (sender, args) => { OnPropertyChanged("ExhibitionsCount"); };
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

        #endregion // Class Private Methods
    }
}