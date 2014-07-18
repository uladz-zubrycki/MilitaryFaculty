using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Exhibition : UniqueEntity, IImitator<Exhibition>
    {
        public static int NameMaxLength = 50;

        private AwardType _awardType;
        private EventLevel _eventLevel;

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Professor Participant { get; set; }

        public AwardType AwardType
        {
            get { return _awardType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _awardType = value;
            }
        }

        public EventLevel EventLevel
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

        public Exhibition()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public Exhibition(Exhibition other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(Exhibition other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            AwardType = other.AwardType;
            Date = other.Date;
            Name = other.Name;
            Participant = other.Participant;
        }
    }
}