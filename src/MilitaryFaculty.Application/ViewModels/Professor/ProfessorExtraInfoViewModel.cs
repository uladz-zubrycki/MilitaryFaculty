using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorExtraInfoViewModel : EntityViewModel<Domain.Professor>
    {
        public ProfessorExtraInfoViewModel(Domain.Professor model)
            : base(model)
        {
            this.Editable(Do.ProfessorSave);
        }

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
    }
}