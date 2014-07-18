using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class Professor : UniqueEntity, IImitator<Professor>
    {
        private AcademicDegree _academicDegree;
        private AcademicRank _academicRank;
        private JobPosition _jobPosition;
        private MilitaryRank _militaryRank;

        public virtual FullName FullName { get; set; }
        public virtual Cathedra Cathedra { get; set; }

        public virtual DateTime EnrollDate { get; set; }
        public virtual DateTime DismissalDate { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }
        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public virtual ICollection<ScientificResearch> ScientificResearches { get; set; }
        public virtual ICollection<ScientificRequest> ScientificRequests { get; set; }
        public virtual ICollection<ScientificExpertise> ScientificExpertises { get; set; }
        public virtual ICollection<ImprovementSuggestion> ImprovementSuggestions { get; set; }
        public virtual ICollection<Synopsis> Synopses { get; set; }
        public virtual ICollection<AcademicDegreeChanging> AcademicDegreeChangings { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }

        public virtual MilitaryRank MilitaryRank
        {
            get { return _militaryRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _militaryRank = value;
            }
        }

        public virtual AcademicRank AcademicRank
        {
            get { return _academicRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _academicRank = value;
            }
        }

        public virtual AcademicDegree AcademicDegree
        {
            get { return _academicDegree; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _academicDegree = value;
            }
        }

        public virtual JobPosition JobPosition
        {
            get { return _jobPosition; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _jobPosition = value;
            }
        }

        public Professor()
        {
            Id = Guid.Empty;
            _militaryRank = MilitaryRank.JuniorLieutenant;
            _academicDegree = AcademicDegree.None;
            _academicRank = AcademicRank.None;
            _jobPosition = JobPosition.Teacher;

            EnrollDate = DateTime.Now;
            DismissalDate = DateTime.MaxValue;

            FullName = new FullName();
            Cathedra = null;
            Conferences = new Collection<Conference>();
            Publications = new Collection<Publication>();
            Exhibitions = new Collection<Exhibition>();
            Books = new Collection<Book>();

            ScientificResearches = new Collection<ScientificResearch.ScientificResearch>();
            ScientificRequests = new Collection<ScientificRequest.ScientificRequest>();
            ScientificExpertises = new Collection<ScientificExpertise.ScientificExpertise>();
            ImprovementSuggestions = new Collection<ImprovementSuggestion.ImprovementSuggestion>();
            Synopses = new Collection<Synopsis.Synopsis>();
            AcademicDegreeChangings = new Collection<AcademicDegreeChanging>();
            Participations = new Collection<Participation>();
        }

        public Professor(Professor other)
            : this()
        {
            Imitate(other);
        }

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

            EnrollDate = other.EnrollDate;
            DismissalDate = other.DismissalDate;
        }
    }
}