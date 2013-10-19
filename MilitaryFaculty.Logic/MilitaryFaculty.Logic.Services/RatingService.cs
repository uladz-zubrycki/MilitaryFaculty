//using System;
//using System.Linq;
//using MilitaryFaculty.DataAccess.Contract;
//using MilitaryFaculty.Domain;

//namespace MilitaryFaculty.Logic.Services
//{
//    public class RatingService // : IRatingService
//    {
//        private readonly IRepository<Conference> conferenceRepository;

//        public RatingService(IRepository<Conference> conferenceRepository)
//        {
            
//            if (conferenceRepository == null)
//            {
//                throw new ArgumentNullException("conferenceRepository");
//            }
            
            
//            this.conferenceRepository = conferenceRepository;
//        }

//        public int CalculateQualityOfOrganization(Professor professor)
//        {
            

//            if (professor == null)
//            {
//                throw new ArgumentNullException("professor");
//            }

            

//            double curRating = 0;
//            const double maxRating = 248;

//            var formula1 = new Formula(FormulaNames.Formula122);
//            var formula121 = new Formula(FormulaNames.Formula121);
//            var formula122 = new Formula(FormulaNames.Formula121);
//            var formula123 = new Formula(FormulaNames.Formula121);
//            var formula3 = new Formula(FormulaNames.Formula122);

//            //TODO: NPA

//            #region FirstSection

//            curRating += formula1.Calculate(0, 0, 0, 0);

//            #endregion //FirstSection

//            #region SecondSection

//            var conferences = conferenceRepository.AsQueryable()
//                                                  .Where(x => x.Curator.Id == professor.Id)
//                                                  .ToList();

//            double value = 0;
//            foreach (var conference in conferences)
//            {
//                var cr = conference.ConferenceReport;
//                value += formula122.Calculate((int) cr.OrganizationCorrectness,
//                                              (int) cr.ReportMaterials,
//                                              (int) cr.ResultsUsage,
//                                              (int) cr.ThemeActuality)/2;
//            }

//            int internationalConferencesCount = conferences.Count(x => x.ConferenceType == ConferenceType.International);
//            int republicConferencesCount = conferences.Count(x => x.ConferenceType == ConferenceType.Republican);
//            int universityConferencesCount = conferences.Count(x => x.ConferenceType == ConferenceType.University);
//            int republicSeminarsCount = 0;
//            int universitySeminarsCount = 0;
//            var value2 = formula123.Calculate(internationalConferencesCount, republicConferencesCount,
//                                              universityConferencesCount, republicSeminarsCount, universitySeminarsCount);

//            int count = internationalConferencesCount +
//                        republicConferencesCount +
//                        universityConferencesCount +
//                        republicSeminarsCount +
//                        universitySeminarsCount;
//            value = formula121.Calculate(value, value2, count);

//            curRating += value;

//            #endregion //SecondSection

//            //TODO: add new properties to Professor

//            #region ThirdSection

//            curRating += formula3.Calculate(0, 0, 0, 0);

//            #endregion //ThirdSection

//            #region FourthSection

//            curRating += formula3.Calculate(0, 0, 0, 0);

//            #endregion //FourthSection

//            #region FivthSection

//            curRating += formula3.Calculate(0, 0, 0, 0);

//            #endregion // FivthSection

//            return Convert.ToInt32(curRating/maxRating*100);
//        }

//        public int CalculateResearch(Professor professor)
//        {
//            return 0;
//        }

//        public int CalculateApprobation(Professor professor)
//        {
//            return 0;
//        }

//        public int CalculateTrainingAndCertification(Professor professor)
//        {
//            return 0;
//        }
//    }
//}

