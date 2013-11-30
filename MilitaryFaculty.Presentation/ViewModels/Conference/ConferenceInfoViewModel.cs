using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceInfoViewModel : ViewModel<Conference>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Базовая информация";
            }
        }

        public EventLevel ConferenceType
        {
            get { return Model.ConferenceType; }
            set
            {
                SetModelProperty(m => m.ConferenceType, value);
            }
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                SetModelProperty(m => m.Name, value);
            }
        }

        public string DateString
        {
            get { return Date.ToShortDateString(); }
        }

        public DateTime Date
        {
            get { return Model.Date; }
            set
            {
               SetModelProperty(m => m.Date, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceInfoViewModel(Conference model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}