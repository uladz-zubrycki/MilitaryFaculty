using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Exhibition : UniqueEntity, IImitator<Exhibition>
    {
        #region Type Static Members

        public static int NameMaxLength = 50;

        #endregion // Type Static Members

        #region Class Fields

        private AwardType awardType;
        private EventLevel eventLevel;

        #endregion // Class Fields

        #region Class Properties

        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Professor Participant { get; set; }

        public AwardType AwardType
        {
            get { return awardType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                awardType = value;
            }
        }

        public EventLevel EventLevel
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

        #endregion // Class Constructors

        #region Class Public Methods

        public void Imitate(Exhibition other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            this.AwardType = other.AwardType;
            this.Date = other.Date;
            this.Name = other.Name;
            this.Participant = other.Participant;
        }

        #endregion // Class Public Methods
    }
}