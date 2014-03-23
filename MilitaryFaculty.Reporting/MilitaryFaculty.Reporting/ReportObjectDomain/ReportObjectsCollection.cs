using System;
using System.Collections.Generic;

namespace MilitaryFaculty.Reporting.ReportObjectDomain
{
    public class ReportObjectsCollection
    {
        public ICollection<ReportObject> ReportObjects { get; private set; }

        //TODO: Implement this class for MultipleInstancesExcelService
        public ReportObjectsCollection()
        {
            ReportObjects = new List<ReportObject>();
        }

        public ReportObjectsCollection(ICollection<ReportObject> reportObjects)
        {
            if (reportObjects == null)
            {
                throw new ArgumentNullException("reportObjects");
            }

            ReportObjects = reportObjects;
        }
    }
}
