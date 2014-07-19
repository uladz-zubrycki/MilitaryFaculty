using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    public class AcademicDegreeChanging : UniqueEntity, IImitator<AcademicDegreeChanging>
    {
        public DateTime Date { get; set; }
        public Professor Target { get; set; }
        public AcademicDegree ResultedDegree { get; set; }
    
        public AcademicDegreeChanging()
        {
            Date = DateTime.Now;
            ResultedDegree = AcademicDegree.None;
        }

        public AcademicDegreeChanging(AcademicDegreeChanging other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(AcademicDegreeChanging other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Date = other.Date;
            Target = other.Target;
        }
    }
}