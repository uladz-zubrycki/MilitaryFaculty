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

        private AwardType award;

        #endregion // Class Fields

        #region Class Properties

        public string Name { get; set; }
        public DateTime Date { get;set; }
        public Professor Participant { get; set; }

        public AwardType Award
        {
            get { return award; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                award = value;
            }
        }

        #endregion // Class Properties

        #region Class Public Methods

        public void Imitate(Exhibition other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            this.Award = other.Award;
            this.Date = other.Date;
            this.Name = other.Name;
            this.Participant = other.Participant;
        }
        
        #endregion // Class Public Methods
    }
}
