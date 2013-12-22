namespace MilitaryFaculty.Reporting
{
    public interface IFormulaProvider
    {
        FormulaInfo GetFormula(string id);
    }
}