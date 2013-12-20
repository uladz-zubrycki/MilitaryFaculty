using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorHeaderViewModel: ViewModel<Professor>
    {
        #region Class Properties

        public string FullName
        {
            get { return Model.FullName.ToString(); }
        }

        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set { SetModelProperty(m => m.MilitaryRank, value); }
        }
        
        #endregion // Class Properties

        #region Class Constructors
        
        public ProfessorHeaderViewModel(Professor model) 
            : base(model)
        {
            // Empty
        }

        #endregion // Class Constructors
    }
}
