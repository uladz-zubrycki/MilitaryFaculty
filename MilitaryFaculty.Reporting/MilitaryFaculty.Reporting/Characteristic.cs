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
        private readonly ICollection<string> _arguments;
        private readonly CompiledExpression _compiledExpression;
        private readonly ReportDataProvider _reportDataProvider;

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

            _reportDataProvider = reportDataProvider;

            var formulaCore = formulaInfo.Coefficients
                                         .Aggregate(formulaInfo.Expression,
                                             (current, coeff) =>
                                                 current.Replace(coeff.Key,
                                                     coeff.Value.ToString(CultureInfo.InvariantCulture)));

            _arguments = formulaInfo.Arguments;

            var preparedExpression = ToolsHelper.Parser.Parse(formulaCore);
            _compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            _compiledExpression = ToolsHelper.Optimizer.Optimize(_compiledExpression);
        }

        public double Evaluate()
        {
            var variables = _arguments.Select(CreateVariable)
                                      .ToList();

            return ToolsHelper.Calculator.Calculate(_compiledExpression, variables);
        }

        private VariableValue CreateVariable(string arg)
        {
            return new VariableValue(_reportDataProvider.GetValue(arg), arg);
        }
    }
}