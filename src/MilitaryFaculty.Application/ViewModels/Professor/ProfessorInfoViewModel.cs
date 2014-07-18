using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorInfoViewModel : EntityViewModel<Professor>
    {
        public ProfessorInfoViewModel(Professor model)
            : base(model)
        {
            this.Editable(Do.ProfessorSave);
        }

        public override string Title
        {
            get { return "Базовая информация"; }
        }

        [TextProperty(Label = "Имя:")]
        public string FirstName
        {
            get { return Model.FullName.FirstName; }
            set { SetModelProperty(m => m.FullName.FirstName, value); }
        }

        [TextProperty(Label = "Отчество:")]
        public string MiddleName
        {
            get { return Model.FullName.MiddleName; }
            set { SetModelProperty(m => m.FullName.MiddleName, value); }
        }

        [TextProperty(Label = "Фамилия:")]
        public string LastName
        {
            get { return Model.FullName.LastName; }
            set { SetModelProperty(m => m.FullName.LastName, value); }
        }

        [EnumProperty(Label = "Звание:")]
        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set { SetModelProperty(m => m.MilitaryRank, value); }
        }
    }
}