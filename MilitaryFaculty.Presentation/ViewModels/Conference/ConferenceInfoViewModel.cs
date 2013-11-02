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
            get { return Model.EventLevel.GetName(); }
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
            get { return Model.EventLevel; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == ConferenceType)
                {
                    return;
                }

                Model.EventLevel = value;
                OnPropertyChanged();
                OnPropertyChanged("ConferenceTypeString");
            }
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (value == Name)
                {
                    return;
                }

                Model.Name = value;
                OnPropertyChanged();
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
                if (value == Date)
                {
                    return;
                }

                Model.Date = value;
                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceInfoViewModel(Conference model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            Title = "Базовая информация"; ;

            var editCommands = new EditableViewBehaviour<Conference>(ApplicationCommands.UpdateConference,
                                                                     Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}