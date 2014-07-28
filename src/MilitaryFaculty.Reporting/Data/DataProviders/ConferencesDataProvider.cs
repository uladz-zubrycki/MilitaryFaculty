using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ConferencesDataProvider : DataProvider<Conference>
    {
        public ConferencesDataProvider(IRepository<Conference> repository)
            : base(repository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = conference =>
                conference.Date >= interval.From
                && conference.Date <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = conference =>
                conference.Curator.Cathedra.Id == cathedra.Id
                && conference.Date >= interval.From
                && conference.Date <= interval.To;
        }

        public override void SetPersonModificator(Person person, TimeInterval interval)
        {
            QueryModificator = conference =>
                conference.Curator.Id == person.Id
                && conference.Date >= interval.From
                && conference.Date <= interval.To;
        }

        /// <summary>
        ///     Выступление ППС на вузовской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfProfsCount")]
        public double UniversityConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.University
                                && c.Curator.JobPosition != JobPosition.Сadet);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на вузовской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfStudsCount")]
        public double UniversityConferenceStudentsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.University
                                && c.Curator.JobPosition == JobPosition.Сadet);
        }

        /// <summary>
        ///     Выступление ППС на республиканской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfProfsCount")]
        public double RepublicanConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.Republican
                                && c.Curator.JobPosition != JobPosition.Сadet);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на республиканской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfStudsCount")]
        public double RepublicanConferenceStudentsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.Republican
                                && c.Curator.JobPosition == JobPosition.Сadet);
        }

        /// <summary>
        ///     Выступление ППС на международной конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfProfsCount")]
        public double InternationalConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.International
                                && c.Curator.JobPosition != JobPosition.Сadet);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на международной конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfStudsCount")]
        public double InternationalConferenceStudentsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.International
                                && c.Curator.JobPosition != JobPosition.Сadet);
        }
    }
}