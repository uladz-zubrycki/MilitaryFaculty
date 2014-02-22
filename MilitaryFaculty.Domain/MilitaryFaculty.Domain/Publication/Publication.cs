using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Publication : UniqueEntity, IImitator<Publication>
    {
        private PublicationType _publicationType;

        public virtual Professor Author { get; set; }
        public virtual string Name { get; set; }
        public virtual int PagesCount { get; set; }

        public virtual PublicationType PublicationType
        {
            get { return _publicationType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _publicationType = value;
            }
        }

        public Publication()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            PagesCount = 0;
            PublicationType = PublicationType.Monograph;
        }

        public Publication(Publication other)
        {
            Imitate(other);
        }

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
    }
}