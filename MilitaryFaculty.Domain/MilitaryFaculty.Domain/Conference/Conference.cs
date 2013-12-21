using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Conference : UniqueEntity, IImitator<Conference>
    {
        #region Class Constants

        public const int NameMaxLength = 200;

        #endregion // Class Constants

        #region Class Fields

        private EventLevel eventLevel;

        #endregion // Class Fields

        #region Class Properties

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Curator { get; set; }
        public virtual ConferenceReport ConferenceReport { get; set; }

        public virtual EventLevel EventLevel
        {
            get { return eventLevel; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                eventLevel = value;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public Conference()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            ConferenceReport = new ConferenceReport();
            Date = DateTime.Now;
            Curator = null;
        }

        public Conference(Conference other)
            : this()
        {
            Imitate(other);
        }

        #endregion // Class Constructors

        #region Implementation of IImitator<Conference>

        public void Imitate(Conference other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            Date = other.Date;
            Curator = other.Curator;
            EventLevel = other.EventLevel;
            ConferenceReport.Imitate(other.ConferenceReport);
        }

        #endregion // Implementation of IImitator<Conference>
    }
}