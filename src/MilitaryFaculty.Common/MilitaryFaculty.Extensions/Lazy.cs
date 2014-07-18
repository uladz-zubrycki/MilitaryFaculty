using System;

namespace MilitaryFaculty.Extensions
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
