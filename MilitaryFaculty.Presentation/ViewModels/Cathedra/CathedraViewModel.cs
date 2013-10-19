using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraViewModel : ViewModel<Cathedra>
    {
        #region Class Constructors

        public CathedraViewModel(Cathedra cathedra)
            : base(cathedra)
        {
            Title = Model.Name;
        }

        #endregion // Class Constructors
    }
}