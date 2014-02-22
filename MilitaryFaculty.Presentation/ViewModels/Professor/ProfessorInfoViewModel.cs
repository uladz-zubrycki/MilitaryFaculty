using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorInfoViewModel : EntityViewModel<Professor>
    {
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

        public ProfessorInfoViewModel(Professor model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Professor>(Do.Professor.Update, Model);
            editCommands.Inject(this);
        }
    }
}