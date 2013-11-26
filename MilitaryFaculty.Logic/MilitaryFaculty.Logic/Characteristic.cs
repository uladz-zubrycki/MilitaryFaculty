using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ELW.Library.Math;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;

namespace MilitaryFaculty.Logic
{
    public class Characteristic
    {
        #region Class Fields

        private readonly string formulaCore;
        private readonly CompiledExpression compiledExpression;
        private readonly ICollection<string> arguments;
        private readonly DataModule dataModule;

        #endregion // Class Fields

        #region Class Constructors

        public Characteristic(FormulaInfo formulaInfo, DataModule dataModule)
        {
            if (formulaInfo == null)
            {
                throw new ArgumentNullException("formulaInfo");
            }
            if (dataModule == null)
            {
                throw new ArgumentNullException("dataModule");
            }

            this.dataModule = dataModule;

            formulaCore = formulaInfo.Expression;

            foreach (var coeff in formulaInfo.Coefficients)
            {
                formulaCore = formulaCore.Replace(coeff.Key, coeff.Value.ToString(CultureInfo.InvariantCulture));
            }

            arguments = formulaInfo.Arguments;

            var preparedExpression = ToolsHelper.Parser.Parse(formulaCore);
            compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            compiledExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public double Evaluate()
        {
            var localVariables = arguments.Select(arg => new VariableValue(dataModule.GetValue(arg), arg)).ToList();

            return ToolsHelper.Calculator.Calculate(compiledExpression, localVariables);
        }

        #endregion // Class Public Methods
    }
}