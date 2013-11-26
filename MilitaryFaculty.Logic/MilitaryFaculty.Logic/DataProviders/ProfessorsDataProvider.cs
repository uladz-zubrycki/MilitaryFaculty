using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Logic.DataProviders
{
    public class ProfessorsDataProvider : IDataProvider
    {
        public ProfessorsDataProvider()
        {
            /*TODO: Repository argument*/
        }

        #region Class Public Argument Methods

        [FormulaArgument("ProfsCount")]
        public double ProfessorsCount()
        {
            return 2;
        }

        [FormulaArgument("DocsCount")]
        public double DoctoralCandidatesCount()
        {
            return 1;
        }

        [FormulaArgument("AdjCountFt")]
        public double AdjunctsCountFullTime()
        {
            return 2;
        }

        [FormulaArgument("AdjCountExt")]
        public double AdjunctsCountExtramural()
        {
            return 3;
        }

        [FormulaArgument("CandDoctCount")]
        public double CandidateDoctorsCount()
        {
            return 4;
        }

        [FormulaArgument("ApplDoctCount")]
        public double ApplicantForDoctorsCount()
        {
            return 5;
        }

        [FormulaArgument("ApplCandCount")]
        public double ApplicantForCandidatsCount()
        {
            return 5;
        }

        [FormulaArgument("DocThesisesCount")]
        public double DoctorThesisesCount()
        {
            return 1;
        }

        [FormulaArgument("CandThesisesCount")]
        public double CandidateThesisesCount()
        {
            return 2;
        }

        [FormulaArgument("DocAssignCount")]
        public double DoctorsAssignedCount()
        {
            return 3;
        }

        [FormulaArgument("DocentsAssignCount")]
        public double DocentsAssignedCount()
        {
            return 4;
        }

        [FormulaArgument("ProfPostsCount")]
        public double ProfessorPostsCount()
        {
            return 4;
        }

        [FormulaArgument("ProfPostsSubsCount")]
        public double ProfessorPostsSubstitutionCount()
        {
            return 5;
        }

        [FormulaArgument("DocentPostsCount")]
        public double DocentPostsCount()
        {
            return 1;
        }

        [FormulaArgument("DocentPostsSubsCount")]
        public double DocentPostsSubstitutionCount()
        {
            return 2;
        }

        [FormulaArgument("DosCount")]
        public double DoctorsOfScienceCount()
        {
            return 3;
        }

        [FormulaArgument("CosCount")]
        public double CandidatsOfScienceCount()
        {
            return 4;
        }

        [FormulaArgument("DocEoCount")]
        public double DoctorsOfScienceExpertOpinionsCount()
        {
            return 5;
        }

        [FormulaArgument("CandEoCount")]
        public double CandidatsExpertOpinionsCount()
        {
            return 1;
        }

        [FormulaArgument("DosCosCount")]
        public double DoctorsOfScienceAndCandidatsCount()
        {
            return 2;
        }

        [FormulaArgument("EssayReviewDosCount")]
        public double EssayReviewsByDosCount()
        {
            return 3;
        }

        [FormulaArgument("EssayReviewCanCount")]
        public double EssayReviewByCandidatsCount()
        {
            return 4;
        }

        [FormulaArgument("SaHqProfsCount")]
        public double ScientificAdviceHqProfsCount()
        {
            return 5;
        }

        [FormulaArgument("SaProfsCount")]
        public double ScientificAdviceProfsCount()
        {
            return 5;
        }

        [FormulaArgument("HqProfsCount")]
        public double HighQualificationProfsCount()
        {
            return 1;
        }

        [FormulaArgument("HacHqProfsCount")]
        public double HacHqProfsCount() //Higher Attestation Commission
        {
            return 2;
        }

        [FormulaArgument("DodHqProfsCount")]
        public double DodHqProfsCount() //Defence of dissertation counsil
        {
            return 3;
        }

        [FormulaArgument("RcHqProfsCount")]
        public double RcHqProfsCount() //research counsil
        {
            return 4;
        }

        [FormulaArgument("EbspHqProfsCount")]
        public double EbspHqProfsCount() //editorial boards of scientific publications
        {
            return 5;
        }

        [FormulaArgument("SoHqA40")]
        public double StaffingOfHighQualificationAbove40()
        {
            return 5;
        }

        [FormulaArgument("SoHqA20B40")]
        public double StaffingOfHighQualificationAbove20Below40()
        {
            return 1;
        }

        [FormulaArgument("SoHqB20")]
        public double StaffingOfHighQualificatioBelow20()
        {
            return 2;
        }

        [FormulaArgument("SoHqE0")]
        public double StaffingOfHighQualificatioEqual0()
        {
            return 3;
        }

        #endregion // Class Public Argument Methods
    }
}