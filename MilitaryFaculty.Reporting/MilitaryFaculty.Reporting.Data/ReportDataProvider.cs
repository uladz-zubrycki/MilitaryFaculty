using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Reporting.Data
{
	public class ReportDataProvider
	{
		private readonly IDictionary<string, Func<double>> _evaluators;

		public ReportDataProvider(IEnumerable<IDataProvider> providers)
		{
			if (providers == null)
			{
				throw new ArgumentNullException("providers");
			}

			_evaluators = new Dictionary<string, Func<double>>();

			foreach (IDataProvider provider in providers)
			{
				RegisterDataProvider(provider);
			}
		}

		public double GetValue(string key)
		{
			Func<double> evaluator = _evaluators[key];

			return evaluator();
		}

		private void RegisterDataProvider(IDataProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}

			Type type = provider.GetType();
			List<MethodInfo> methods = type.GetMethods()
										   .Where(IsEvaluator)
										   .ToList();

			foreach (MethodInfo method in methods)
			{
				string key = GetArgumentName(method);
				Func<double> evaluator = CreateEvaluator(method, provider);

				if (_evaluators.ContainsKey(key))
				{
					throw new InvalidOperationException(String.Format("Formula argument duplicate {0}", key));
				}

				_evaluators[key] = evaluator;
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
				   info.ReturnType == typeof(double);
		}
	}
}