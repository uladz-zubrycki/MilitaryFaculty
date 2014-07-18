using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorHeaderViewModel : ViewModel<Professor>
    {
        public ProfessorHeaderViewModel(Professor model)
            : base(model)
        {
            // Empty
        }

        public string FullName
        {
            get { return Model.FullName.ToString(); }
        }

        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set { SetModelProperty(m => m.MilitaryRank, value); }
        }
    }
}