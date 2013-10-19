using System;
using MilitaryFaculty.Domain.Contract;

namespace MilitaryFaculty.Domain
{
    public class FullName : IImitator<FullName>
    {
        #region Class Constants

        public const int FirstNameMaxLength = 50;
        public const int MiddleNameMaxLength = 50;
        public const int LastNameMaxLength = 50;

        #endregion // Class Constants

        #region Class Fields

        #endregion //Class Fields

        #region Class Properties

        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }

        #endregion // Class Properties

        #region Class Constructors

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

        #endregion // Class Constructors

        #region Class Public Methods

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

        #endregion // Class Public Methods
    }
}