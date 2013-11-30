using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorExtraInfoViewModel : ViewModel<Professor>
    {
        #region Class Properties

        public override string Title
        {
            get { return "Дополнительная информация"; }
        }

        public JobPosition JobPosition
        {
            get { return Model.JobPosition; }
            set
            {
                SetModelProperty(m => m.JobPosition, value);
                OnPropertyChanged("JobPositionString");
            }
        }

        public AcademicDegree AcademicDegree
        {
            get { return Model.AcademicDegree; }
            set
            {
                SetModelProperty(m => m.AcademicDegree, value);
                OnPropertyChanged("AcademicDegreeString");
            }
        }

        public AcademicRank AcademicRank
        {
            get { return Model.AcademicRank; }
            set
            {
                SetModelProperty(m => m.AcademicRank, value);

                //OnPropertyChanged("AcademicRankString");
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorExtraInfoViewModel(Professor model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Professor>(Do.Professor.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}