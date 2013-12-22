using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ELW.Library.Math;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using MilitaryFaculty.Reporting.Data;

namespace MilitaryFaculty.Reporting
{
    public class Characteristic
    {
        private readonly ICollection<string> arguments;
        private readonly CompiledExpression compiledExpression;
        private readonly ReportDataProvider reportDataProvider;

        public Characteristic(FormulaInfo formulaInfo, ReportDataProvider reportDataProvider)
        {
            if (formulaInfo == null)
            {
                throw new ArgumentNullException("formulaInfo");
            }
            if (reportDataProvider == null)
            {
                throw new ArgumentNullException("ReportDataProvider");
            }

            this.reportDataProvider = reportDataProvider;

            var formulaCore = formulaInfo.Coefficients
                                         .Aggregate(formulaInfo.Expression,
                                                    (current, coeff) =>
                                                    current.Replace(coeff.Key,
                                                                    coeff.Value.ToString(CultureInfo.InvariantCulture)));

            arguments = formulaInfo.Arguments;

            var preparedExpression = ToolsHelper.Parser.Parse(formulaCore);
            compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            compiledExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
        }

        public double Evaluate()
        {
            var variables = arguments.Select(CreateVariable)
                                          .ToList();

            return ToolsHelper.Calculator.Calculate(compiledExpression, variables);
        }

        private VariableValue CreateVariable(string arg)
        {
            return new VariableValue(reportDataProvider.GetValue(arg), arg);
        }
    }
}