using System;
using System.Runtime.Serialization;

namespace MilitaryFaculty.Data.Contract
{
    [Serializable]
    public class RepositoryOperationException : Exception
    {
        #region Class Constructors

        public RepositoryOperationException()
        {
            // Empty
        }

        public RepositoryOperationException(string message)
            : base(message)
        {
            // Empty
        }

        public RepositoryOperationException(string message, Exception inner)
            : base(message, inner)
        {
            // Empty
        }

        protected RepositoryOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            // Empty
        }

        #endregion // Class Constructors
    }
}