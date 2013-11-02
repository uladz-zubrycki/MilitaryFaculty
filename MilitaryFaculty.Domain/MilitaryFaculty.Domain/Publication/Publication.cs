using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Publication : UniqueEntity, IImitator<Publication>
    {
        #region Class Fields

        private PublicationType publicationType;

        #endregion // Class Fields

        #region Class Properties

        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }

        public virtual PublicationType PublicationType
        {
            get { return publicationType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                publicationType = value;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public Publication()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            PagesCount = 0;
            publicationType = PublicationType.Monograph;
        }

        public Publication(Publication other)
        {
            Imitate(other);
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void Imitate(Publication other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            PagesCount = other.PagesCount;
            Author = other.Author;
            PublicationType = other.PublicationType;
        }

        #endregion // Class Public Methods
    }
}