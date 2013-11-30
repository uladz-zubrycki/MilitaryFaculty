using System.ComponentModel;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorInfoViewModel : ViewModel<Professor>
    {
        #region Class Properties

        public override string Title
        {
            get { return "Базовая информация"; }
        }

        public string FullName
        {
            get { return Model.FullName.ToString(); }
        }

        public string FirstName
        {
            get { return Model.FullName.FirstName; }
            set
            {
                SetModelProperty(m => m.FullName.FirstName, value);
            }
        }

        public string MiddleName
        {
            get { return Model.FullName.MiddleName; }
            set
            {
               SetModelProperty(m => m.FullName.MiddleName, value);
            }
        }

        public string LastName
        {
            get { return Model.FullName.LastName; }
            set
            {
                SetModelProperty(m => m.FullName.LastName, value);
            }
        }

        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set
            {
                SetModelProperty(m => m.MilitaryRank, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorInfoViewModel(Professor model)
            : this(model, EditableViewMode.Display)
        {
            // Empty
        }

        public ProfessorInfoViewModel(Professor model, EditableViewMode mode)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Professor>(Do.Professor.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}