using System;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class FullName 
    {
        public const int FirstNameMaxLength = 50;
        public const int MiddleNameMaxLength = 50;
        public const int LastNameMaxLength = 50;

        public FullName(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public FullName()
        {
        }

        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}