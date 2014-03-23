using System;
using System.Collections.Generic;
using MilitaryFaculty.Reporting.Data;
using MilitaryFaculty.Reporting.XmlDomain;

namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
    public class ReportFormulasGroup
    {
        public string Name { get; private set; }
        public ICollection<ReportFormulaInfo> FormulasInfo { get; private set; }

        public ReportFormulasGroup(XReportTableGroup xmlGroup, 
                                   IFormulaProvider formulaProvider,
                                   ReportDataProvider reportDataProvider)
        {
            if (xmlGroup == null)
            {
                throw new ArgumentNullException("xmlGroup");
            }
            if (formulaProvider == null)
            {
                throw new ArgumentNullException("formulaProvider");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("reportDataProvider");
            }

            Name = xmlGroup.Name;
            FormulasInfo = new List<ReportFormulaInfo>();

            foreach (var formulaId in xmlGroup.Formulas)
            {
                var formulaInfo = formulaProvider.GetFormula(formulaId);
                var characteristic = new Characteristic(formulaInfo, reportDataProvider);
                var value = NormalizeValue(characteristic.Evaluate());

                //TODO: Round or Integral part?
                var reportFormula = new ReportFormulaInfo(formulaInfo.Name, 
                                                          Convert.ToInt32(value),     
                                                          Convert.ToInt32(formulaInfo.MaxValue));
                FormulasInfo.Add(reportFormula);
            }
        }

        private static double NormalizeValue(double value)
        {
            return double.IsInfinity(value) ? 0 : value;
        }
    }
}
