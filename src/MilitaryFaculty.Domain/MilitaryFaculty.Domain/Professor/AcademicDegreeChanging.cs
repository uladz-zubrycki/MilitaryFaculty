using System;
using System.ComponentModel;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class AcademicDegreeChanging : UniqueEntity, IImitator<AcademicDegreeChanging>
    {
        private AcademicDegree _resultedDegree;

        public DateTime Date { get; set; }
        public Professor Target { get; set; }

        public AcademicDegree ResultedDegree
        {
            get { return _resultedDegree; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _resultedDegree = value;
            }
        }

        public AcademicDegreeChanging()
        {
            Date = DateTime.Now;
            _resultedDegree = AcademicDegree.None;
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