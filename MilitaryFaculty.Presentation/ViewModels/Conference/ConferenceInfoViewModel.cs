using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceInfoViewModel : ViewModel<Conference>
    {
        #region Class Properties

        public string ConferenceTypeString
        {
            get { return ConferenceType.GetName(); }
        }

        public IEnumerable<Tuple<EventLevel, string>> ConferenceTypeList
        {
            get
            {
                return Enum.GetValues(typeof(EventLevel))
                           .Cast<EventLevel>()
                           .Select(val => new Tuple<EventLevel, string>(val, val.GetName()));
            }
        }

        public EventLevel ConferenceType
        {
            get { return Model.ConferenceType; }
            set
            {
                SetModelProperty(m => m.ConferenceType, value);
                OnPropertyChanged("ConferenceTypeString");
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
            Title = "Базовая информация";

            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}