using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ELW.Library.Math;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Logic
{
    public class Characteristic
    {
        #region Class Fields

        private readonly string formulaCore;
        private readonly CompiledExpression compiledExpression;
        private readonly ICollection<string> arguments;
        private readonly Professor professor;

        #endregion // Class Fields

        #region Class Constructors

        public Characteristic(FormulaInfo formulaInfo)
        {
            professor = null;

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

        public Characteristic(FormulaInfo formulaInfo, Professor professor) : this(formulaInfo)
        {
            this.professor = professor;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public double Evaluate()
        {
            var dataProvider = professor == null ? new DataProvider(new Cathedra()) : new DataProvider(professor);

            var localVariables = arguments.Select(arg => new VariableValue(dataProvider.GetValue(arg), arg)).ToList();

            return ToolsHelper.Calculator.Calculate(compiledExpression, localVariables);
        }

        #endregion // Class Public Methods
    }
}