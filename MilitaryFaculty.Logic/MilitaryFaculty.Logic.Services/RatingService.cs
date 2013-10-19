using System;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Logic.Services
{
    public class RatingService
    {
        private readonly IRepository<Conference> conferenceRepository;

        public RatingService(IRepository<Conference> conferenceRepository)
        {
            if (conferenceRepository == null)
            {
                throw new ArgumentNullException("conferenceRepository");
            }

            this.conferenceRepository = conferenceRepository;
        }

        public int CalculateQualityOfOrganization(Professor professor)
        {
            if (professor == null)
            {
                throw new ArgumentNullException("professor");
            }

            return 0;
        }

        public int CalculateResearch(Professor professor)
        {
            return 0;
        }

        public int CalculateApprobation(Professor professor)
        {
            return 0;
        }

        public int CalculateTrainingAndCertification(Professor professor)
        {
            return 0;
        }
    }
}

