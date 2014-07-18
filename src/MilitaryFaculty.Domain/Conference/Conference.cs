using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Conference : UniqueEntity, IImitator<Conference>
    {
        public const int NameMaxLength = 200;

        private EventLevel _eventLevel;

        public virtual string Name { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Professor Curator { get; set; }
        public virtual ConferenceReport ConferenceReport { get; set; }

        public virtual EventLevel EventLevel
        {
            get { return _eventLevel; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _eventLevel = value;
            }
        }

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
    }
}