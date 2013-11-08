using System.Linq;
using MilitaryFaculty.Logic.XmlFormulasDomain;
using MilitaryFaculty.Logic.XmlInfoDomain;

namespace MilitaryFaculty.Logic
{

    public class DataContainer
    {
        #region Class Fields

        private readonly string formulasPath;
        private readonly string infoPath;

        #endregion // Class Fields

        #region Class Public Methods

        public DataContainer(string formulasFileName, string infoFileName)
        {
            //AppDomain.CurrentDomain.BaseDirectory
            //var xmlDoc = XDocument.Load("FormulasFourthTable.xml");

            formulasPath = formulasFileName;
            infoPath = infoFileName;
        }

        public void GenerateExcelSheet(/*excel sheet*/)
        {
            var tableInfo = TableInfo.Deserialize(infoPath);
            var tableFormulas = TableFormulas.Deserialize(formulasPath);

            foreach (var part in tableInfo.TableParts)
            {
                //TODO:Excel addition
                foreach (var id in part.Identifiers)
                {
                    //TODO:Excel addition
                    var formulaInfo = tableFormulas.Formulas.First(f => f.Id == id).ToFormulaInfo();
                    var characteristic = new Characteristic(formulaInfo);
                    characteristic.Evaluate();
                }
            }
        }

        #endregion // Class Public Methods
    }
}
