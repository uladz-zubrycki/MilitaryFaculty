using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Logic.DataProviders;

namespace MilitaryFaculty.Logic
{
    public class DataModule
    {
        private readonly IDictionary<string, Func<double>> evaluators;

        public DataModule()
        {
            evaluators = new Dictionary<string, Func<double>>();
        }

        public double GetValue(string key)
        {
            return evaluators[key]();
        }

        public void RegisterProviders(IEnumerable<IDataProvider> providers)
        {
            if (providers == null)
            {
                throw new ArgumentNullException("providers");
            }

            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }
        }

        public void RegisterProvider(IDataProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            var type = provider.GetType();
            var methods = type.GetMethods()
                              .Where(IsEvaluator)
                              .ToList();

            foreach (var info in methods)
            {
                var key = GetArgumentName(info);
                var value = GetMethod(info, provider);

                if (evaluators.ContainsKey(key))
                {
                    throw new InvalidOperationException(String.Format("Formula argument duplicate {0}", key));
                }

                evaluators[key] = value;
            }
        }

        private static string GetArgumentName(MethodInfo info)
        {
            var attr = info.GetCustomAttribute<FormulaArgumentAttribute>();

            return attr.Name;
        }

        private static Func<double> GetMethod(MethodInfo info, IDataProvider provider)
        {
            return () => (double)info.Invoke(provider, null);
        }

        private static bool IsEvaluator(MethodInfo info)
        {
            return info.HasAttribute<FormulaArgumentAttribute>() &&
                   info.ReturnType == typeof (double);
        }
    }
}
