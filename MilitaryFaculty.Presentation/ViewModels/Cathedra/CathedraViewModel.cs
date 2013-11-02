using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraViewModel : ComplexViewModel<Cathedra>
    {
        #region Class Constructors

        public CathedraViewModel(Cathedra model)
            : base(model)
        {
            InitViewModels();
        }

        #endregion // Class Constructors

        #region Class Properties

        public CathedraInfoViewModel InfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Private Methods

        private void InitViewModels()
        {
            InfoViewModel = new CathedraInfoViewModel(Model);

            ViewModels.Add(InfoViewModel);
        }
        
        #endregion // Class Private Methods
    }
}