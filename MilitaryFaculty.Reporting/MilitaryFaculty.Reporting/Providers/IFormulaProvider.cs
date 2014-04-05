namespace MilitaryFaculty.Reporting.Providers
{
    public interface IFormulaProvider
    {
        FormulaInfo GetFormula(string id);
    }
}