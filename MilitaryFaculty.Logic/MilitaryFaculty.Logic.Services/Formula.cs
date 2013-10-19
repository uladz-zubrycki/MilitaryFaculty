using System;
using System.Linq;
using ELW.Library.Math;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;

namespace MilitaryFaculty.Logic.Services
{
    public class Formula //: IFormula
    {
        private readonly string funcStr;
        private readonly CompiledExpression compiledExpression;
        private readonly string[] variables;

        public Formula(string expression, params string[] variables)
        {
            
            if (expression == null) throw new ArgumentNullException();
            if (String.IsNullOrEmpty(expression.Trim())) throw new ArgumentException();
            if (variables == null) throw new ArgumentNullException("variables");
            

            funcStr = expression.Replace(" ", "");
            if (funcStr == String.Empty)
            {
                throw new ArgumentException("expression");
            }

            funcStr = funcStr.ToLower();

            var preparedExpression = ToolsHelper.Parser.Parse(funcStr);
            compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
            compiledExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);

            this.variables = variables;
        }

        public double Calculate(params double[] values)
        {
            
            if (values == null) throw new ArgumentNullException();
            if (values.Length != variables.Length) throw new ArgumentException();
            

            var localVariables = Enumerable.Range(0, variables.Length)
                                      .Select(i => new VariableValue(values[i], variables[i]))
                                      .ToList();

            return ToolsHelper.Calculator.Calculate(compiledExpression, localVariables);
        }
    }
}
