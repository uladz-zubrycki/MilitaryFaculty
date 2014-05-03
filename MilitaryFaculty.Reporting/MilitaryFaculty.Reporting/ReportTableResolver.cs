using System;
using System.Collections.Generic;
using MilitaryFaculty.Reporting.Providers;

namespace MilitaryFaculty.Reporting
{
    public class ReportTableResolver : IReportTableResolver
    {
        private readonly Dictionary<Type, IReportTableProvider> _tableProviders; 

        public ReportTableResolver()
        {
            _tableProviders = new Dictionary<Type, IReportTableProvider>();
        }

        public IReportTableProvider GetTableProvider(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (!_tableProviders.ContainsKey(type))
            {
                throw new ArgumentException("type");
            }

            return _tableProviders[type];
        }

        public void RegisterTableProvider(Type type, IReportTableProvider tableProvider)
        {
            if (tableProvider == null)
            {
                throw new ArgumentNullException("tableProvider");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (_tableProviders.ContainsKey(type))
            {
                throw new ArgumentException("type");
            }

            _tableProviders[type] = tableProvider;
        }
    }
}
