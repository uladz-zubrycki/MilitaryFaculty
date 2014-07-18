using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class FullName : IImitator<FullName>
    {
        public const int FirstNameMaxLength = 50;
        public const int MiddleNameMaxLength = 50;
        public const int LastNameMaxLength = 50;

        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }

        public FullName()
        {
            FirstName = String.Empty;
            MiddleName = String.Empty;
            LastName = String.Empty;
        }

        public FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public FullName(FullName other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(FullName other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            FirstName = other.FirstName;
            MiddleName = other.MiddleName;
            LastName = other.LastName;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}