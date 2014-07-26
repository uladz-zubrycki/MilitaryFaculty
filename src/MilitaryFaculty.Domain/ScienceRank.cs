using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public class ScienceRankMetricDefinition: UniqueEntity
    {
        public ScienceRankMetricDefinition(string name, int maxValue)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            MaxValue = maxValue;
        }

        private ScienceRankMetricDefinition()
        {
            
        }

        public string Name { get; private set; }
        public int? MaxValue { get; private set; }
    }

    public class ScienceRankMetric : UniqueEntity
    {
        public int Value { get; set; }
        public ScienceRankMetricDefinition Definition { get; set; }
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    public class ScienceRank : UniqueEntity
    {
        public ScienceRank()
        {
            CreatedAt = DateTime.Now;
            Metrics = new List<ScienceRankMetric>();
        }

        public virtual DateTime CreatedAt { get; set; }
        public Cathedra Cathedra { get; set; }
        public virtual ICollection<ScienceRankMetric> Metrics { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}
