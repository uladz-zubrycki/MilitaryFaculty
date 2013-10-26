using System;
using System.Collections.Generic;
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

        #endregion // Class Fields

        #region Class Constructors

        public Characteristic(FormulaInfo formulaInfo)
        {
            formulaCore = formulaInfo.Formula;

            foreach (var coeff in formulaInfo.Coefficients)
            {
                formulaCore = formulaCore.Replace(coeff.Key, coeff.Value.ToString());
            }

            arguments = formulaInfo.Arguments;

            var preparedExpression = ToolsHelper.Parser.Parse(formulaCore);
            compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            compiledExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
        }

        #endregion // Class Constructors

        #region Class Public Methods
        
        public int Evaluate()
        {
            var localVariables = arguments.Select(arg => new VariableValue(DataProvider.GetValue(arg), arg)).ToList();

            return Convert.ToInt32(ToolsHelper.Calculator.Calculate(compiledExpression, localVariables));
        }

        #endregion // Class Public Methods
    }
}