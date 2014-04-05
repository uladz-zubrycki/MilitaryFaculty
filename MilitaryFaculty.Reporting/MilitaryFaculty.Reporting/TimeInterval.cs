using System;

namespace MilitaryFaculty.Reporting
{
    public class TimeInterval
    {
        public readonly DateTime From;
        public readonly DateTime To;

        public TimeInterval(DateTime from, DateTime to)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (from > to)
            {
                throw new ArgumentException("Start date is later than end date.");
            }

            From = from;
            To = to;
        }
    }
}
