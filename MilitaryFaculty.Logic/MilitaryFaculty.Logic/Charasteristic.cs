using System;

namespace MilitaryFaculty.Logic
{
    public abstract class Charasteristic
    {
        #region Class Fields

        private string name;

        #endregion // Class Fields

        #region Class Properties

        public string Name
        {
            get { return name; }
            
            protected set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException();
                }

                name = value;
            }
        }

        #endregion // Class Properties

        #region Class Abstract Methods

        public abstract int Evaluate();

        #endregion // Class Abstract Methods
    }
}