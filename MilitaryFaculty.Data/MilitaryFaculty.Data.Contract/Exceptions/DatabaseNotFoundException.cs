using System;
using System.Runtime.Serialization;

namespace MilitaryFaculty.Data.Contract
{
    [Serializable]
    public class DatabaseNotFoundException : Exception
    {
        public DatabaseNotFoundException()
        {
            // Empty
        }

        public DatabaseNotFoundException(string message) 
            : base(message)
        {
            // Empty
        }

        public DatabaseNotFoundException(string message, Exception inner) 
            : base(message, inner)
        {
            // Empty
        }

        protected DatabaseNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            // Empty
        }
    }
}