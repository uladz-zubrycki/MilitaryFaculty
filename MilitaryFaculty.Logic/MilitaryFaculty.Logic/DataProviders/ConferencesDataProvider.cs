using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class ConferencesDataProvider : IDataProvider
    {
        #region Class Fields

        private readonly IConferenceRepository conferenceRepository;

        #endregion // Class Fields

        #region Class Constructors

        public ConferencesDataProvider(IConferenceRepository conferenceRepository)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            this.conferenceRepository = conferenceRepository;
        }

        #endregion // Class Constructors

        /// <summary>
        /// Выступление ППС на вузовской конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfProfsCount")]
        public double UniversityConferenceProfessorsCount()
        {
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.University);
        }

        /// <summary>
        /// Выступление курсантов, студентов на вузовской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("UnConfStudsCount")]
        public double UniversityConferenceStudentsCount()
        {
            //TODO: Выступления курсантов
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.University);
        }

        /// <summary>
        /// Выступление ППС на республиканской конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfProfsCount")]
        public double RepublicanConferenceProfessorsCount()
        {
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.Republican);
        }

        /// <summary>
        /// Выступление курсантов, студентов на республиканской конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("ReConfStudsCount")]
        public double RepublicanConferenceStudentsCount()
        {
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.Republican);
        }

        /// <summary>
        /// Выступление ППС на международной конферекции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfProfsCount")]
        public double InternationalConferenceProfessorsCount()
        {
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.International);
        }

        /// <summary>
        /// Выступление курсантов, студентов на международной конференции
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("InConfStudsCount")]
        public double InternationalConferenceStudentsCount()
        {
            return conferenceRepository.Count(c => c.ConferenceType == EventLevel.International);
        }
    }
}
