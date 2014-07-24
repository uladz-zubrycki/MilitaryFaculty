using System.Linq;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Reporting.Data.DataProviders
{
    public class ScientificExpertisesDataProvider : DataProvider<ScientificExpertise>
    {
        public ScientificExpertisesDataProvider(IRepository<ScientificExpertise> professorRepository)
            : base(professorRepository)
        {
        }

        public override void SetFacultyModificator(TimeInterval interval)
        {
            QueryModificator = scientificExpertise =>
                scientificExpertise.CreatedAt >= interval.From
                && scientificExpertise.CreatedAt <= interval.To;
        }

        public override void SetCathedraModificator(Cathedra cathedra, TimeInterval interval)
        {
            QueryModificator = scientificExpertise =>
                scientificExpertise.Author.Cathedra.Id == cathedra.Id
                && scientificExpertise.CreatedAt >= interval.From
                && scientificExpertise.CreatedAt <= interval.To;
        }

        public override void SetProfessorModificator(Professor professor, TimeInterval interval)
        {
            QueryModificator = scientificExpertise =>
                scientificExpertise.Author.Id == professor.Id
                && scientificExpertise.CreatedAt >= interval.From
                && scientificExpertise.CreatedAt <= interval.To;
        }

        /// <summary>
        ///     Количество ППС, участвующих в работе по проведению научной экспертизы
        /// </summary>
        /// <returns></returns>
        [FormulaArgument("SeProfsCount")]
        public double ScientificExperticeProfessorsCount()
        {
            return QueryableCollection.Select(se => se.Author.Id)
                                      .Distinct()
                                      .Count();
        }
    }
}