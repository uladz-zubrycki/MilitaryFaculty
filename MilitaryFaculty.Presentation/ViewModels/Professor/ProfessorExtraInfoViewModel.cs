using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorExtraInfoViewModel : EntityViewModel<Professor>
    {
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

        public ProfessorExtraInfoViewModel(Professor model)
            : base(model)
        {
            // Empty
        }

        protected override IEnumerable<IViewBehaviour> GetBehaviours()
        {
            yield return new EditableViewBehaviour<Professor>(Do.Professor.Update);
        }
    }
}