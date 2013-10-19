using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Common.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceInfoViewModel : ViewModel<Conference>
    {
        #region Class Properties

        public string ConferenceTypeString
        {
            get { return Model.ConferenceType.GetName(); }
        }

        public IEnumerable<Tuple<ConferenceType, string>> ConferenceTypeList
        {
            get
            {
                return Enum.GetValues(typeof (ConferenceType))
                           .Cast<ConferenceType>()
                           .Select(val => new Tuple<ConferenceType, string>(val, val.GetName()));
            }
        }

        public ConferenceType ConferenceType
        {
            get { return Model.ConferenceType; }
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

                Model.ConferenceType = value;
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

        public ConferenceInfoViewModel(Conference model)
            : this(model, EditViewMode.Display)
        {
            // Empty
        }

        public ConferenceInfoViewModel(Conference model, EditViewMode mode)
            : base(model)
        {
            const string title = "Базовая информация";
            
            Title = title;

            var editCommands = new EditUICommandsPackage<Conference>(GlobalAppCommands.UpdateConference, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}