using System;

namespace MilitaryFaculty.Common
{
    public static class Lazy
    {
        public static Lazy<T> Create<T>(Func<T> valueFactory)
        {
            if (valueFactory == null)
            {
                throw new ArgumentNullException("valueFactory");
            }

            return new Lazy<T>(valueFactory);
        }
    }
}
