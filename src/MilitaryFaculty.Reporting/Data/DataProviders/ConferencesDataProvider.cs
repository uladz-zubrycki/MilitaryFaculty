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

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = conference =>
                conference.Curator.Id == professor.Id
                && conference.Date >= interval.From
                && conference.Date <= interval.To;
        }

        /// <summary>
        ///     Выступление ППС на вузовской конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfProfsCount")]
        public double UniversityConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.University);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на вузовской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfStudsCount")]
        public double UniversityConferenceStudentsCount()
        {
            //TODO: Выступления курсантов
            return CountOf(c => c.EventLevel == EventLevel.University);
        }

        /// <summary>
        ///     Выступление ППС на республиканской конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfProfsCount")]
        public double RepublicanConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.Republican);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на республиканской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfStudsCount")]
        public double RepublicanConferenceStudentsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.Republican);
        }

        /// <summary>
        ///     Выступление ППС на международной конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfProfsCount")]
        public double InternationalConferenceProfessorsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.International);
        }

        /// <summary>
        ///     Выступление курсантов, студентов на международной конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfStudsCount")]
        public double InternationalConferenceStudentsCount()
        {
            return CountOf(c => c.EventLevel == EventLevel.International);
        }
    }
}