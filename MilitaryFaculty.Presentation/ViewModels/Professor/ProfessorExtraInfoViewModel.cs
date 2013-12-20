using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorExtraInfoViewModel : EntityViewModel<Professor>
    {
        #region Class Properties

        public override string Title
        {
            get { return "Дополнительная информация"; }
        }

        [EnumProperty(Label = "Занимаемая должность:")]
        public JobPosition JobPosition
        {
            get { return Model.JobPosition; }
            set { SetModelProperty(m => m.JobPosition, value); }
        }

        [EnumProperty(Label = "Учёное звание:")]
        public AcademicDegree AcademicDegree
        {
            get { return Model.AcademicDegree; }
            set { SetModelProperty(m => m.AcademicDegree, value); }
        }

        [EnumProperty(Label = "Учёная степень:")]
        public AcademicRank AcademicRank
        {
            get { return Model.AcademicRank; }
            set { SetModelProperty(m => m.AcademicRank, value); }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorExtraInfoViewModel(Professor model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Professor>(Do.Professor.Update, Model);
            editCommands.Inject(this);
        }

        #endregion // Class Constructors
    }
}