using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorHeaderViewModel : ViewModel<Professor>
    {
        public string FullName
        {
            get { return Model.FullName.ToString(); }
        }

        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set { SetModelProperty(m => m.MilitaryRank, value); }
        }

        public ProfessorHeaderViewModel(Professor model)
            : base(model)
        {
            // Empty
        }
    }
}