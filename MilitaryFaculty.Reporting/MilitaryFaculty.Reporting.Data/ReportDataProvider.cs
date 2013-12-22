using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data
{
    public class ReportDataProvider
    {
        private readonly IDictionary<string, Func<double>> evaluators;

        public ReportDataProvider(IEnumerable<IDataProvider> providers)
        {
            if (providers == null)
            {
                throw new ArgumentNullException("providers");
            }

            evaluators = new Dictionary<string, Func<double>>();

            foreach (var provider in providers)
            {
                RegisterDataProvider(provider);
            }
        }

        public double GetValue(string key)
        {
            var evaluator = evaluators[key];

            return evaluator();
        }

        private void RegisterDataProvider(IDataProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            var type = provider.GetType();
            var methods = type.GetMethods()
                              .Where(IsEvaluator)
                              .ToList();

            foreach (var method in methods)
            {
                var key = GetArgumentName(method);
                var evaluator = CreateEvaluator(method, provider);

                if (evaluators.ContainsKey(key))
                {
                    throw new InvalidOperationException(String.Format("Formula argument duplicate {0}", key));
                }

                evaluators[key] = evaluator;
            }
        }

        private static string GetArgumentName(MethodInfo info)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            var attr = info.GetCustomAttribute<FormulaArgumentAttribute>();

            return attr.Name;
        }

        private static Func<double> CreateEvaluator(MethodInfo info, IDataProvider provider)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            return () => (double)info.Invoke(provider, null);
        }

        private static bool IsEvaluator(MethodInfo info)
        {
            return info.HasAttribute<FormulaArgumentAttribute>() &&
                   info.ReturnType == typeof (double);
        }
    }
}
