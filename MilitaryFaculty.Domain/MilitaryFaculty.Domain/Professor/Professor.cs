using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Professor : UniqueEntity, IImitator<Professor>
    {
        #region Class Fields

        private MilitaryRank militaryRank;
        private JobPosition jobPosition;
        private AcademicRank academicRank;
        private AcademicDegree academicDegree;

        #endregion // Class Fields

        #region Class Properties

        public virtual FullName FullName { get; set; }
        public virtual Cathedra Cathedra { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }
        public virtual ICollection<Exhibition> Exhibitions { get; set; } 

        public virtual MilitaryRank MilitaryRank
        {
            get { return militaryRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                militaryRank = value;
            }
        }

        public virtual AcademicRank AcademicRank
        {
            get { return academicRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                academicRank = value;
            }
        }

        public virtual AcademicDegree AcademicDegree
        {
            get { return academicDegree; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }
                
                academicDegree = value;
            }
        }

        public virtual JobPosition JobPosition
        {
            get { return jobPosition; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                jobPosition = value;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public Professor()
        {
            Id = Guid.Empty;
            militaryRank = MilitaryRank.JuniorLieutenant;
            academicDegree = AcademicDegree.None;
            academicRank = AcademicRank.None;
            jobPosition = JobPosition.Teacher;

            FullName = new FullName();
            Cathedra = null;
            Conferences = new Collection<Conference>();
            Publications = new Collection<Publication>();
            Exhibitions = new Collection<Exhibition>();

        }

        public Professor(Professor other)
            : this()
        {
            Imitate(other);
        }

        #endregion // Class Constructors

        #region Implementation of IImitator<Professor>

        public void Imitate(Professor other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            AcademicDegree = other.AcademicDegree;
            AcademicRank = other.AcademicRank;
            JobPosition = other.JobPosition;
            MilitaryRank = other.MilitaryRank;
            FullName.Imitate(other.FullName);
            Cathedra = other.Cathedra;
        }

        #endregion // Implementation of IImitator<Professor>
    }
}