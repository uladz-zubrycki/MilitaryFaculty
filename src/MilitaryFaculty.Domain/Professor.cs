using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum AcademicRank
    {
        None,
        CandidateOfScience,
        DoctorOfScience
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum JobPosition
    {
        Student,
        Aspirant,
        Doctorant,
        Teacher,
        SeniorProfessor,
        Docent,
        Professor,
        HeadOfCycle,
        HeadOfCathedra,
        HeadOfFaculty,
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum MilitaryRank
    {
        JuniorLieutenant,
        Lieutenant,
        SeniorLieutenant,
        Captain,
        Major,
        LieutenantColonel,
        Colonel,
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum AcademicDegree
    {
        None,
        Docent,
        Professor
    }

    public class Professor : UniqueEntity, IImitator<Professor>
    {
        public virtual FullName FullName { get; set; }
        public virtual Cathedra Cathedra { get; set; }

        public virtual MilitaryRank MilitaryRank { get; set; }
        public virtual AcademicRank AcademicRank { get; set; }
        public virtual AcademicDegree AcademicDegree { get; set; }
        public virtual JobPosition JobPosition { get; set; }

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
        public virtual ICollection<DissertationWork> Synopses { get; set; }
        public virtual ICollection<AcademicDegreeChanging> AcademicDegreeChangings { get; set; }
        public virtual ICollection<Participation> Participations { get; set; }

        // ReSharper disable DoNotCallOverridableMethodsInConstructor
        // Properties are virtual only for EntityFramework
        public Professor()
        {
            MilitaryRank = MilitaryRank.JuniorLieutenant;
            AcademicDegree = AcademicDegree.None;
            AcademicRank = AcademicRank.None;
            JobPosition = JobPosition.Teacher;

            EnrollDate = DateTime.Now;
            DismissalDate = DateTime.MaxValue;
            FullName = new FullName();
            
            Conferences = new Collection<Conference>();
            Publications = new Collection<Publication>();
            Exhibitions = new Collection<Exhibition>();
            Books = new Collection<Book>();
            ScientificResearches = new Collection<ScientificResearch>();
            ScientificRequests = new Collection<ScientificRequest>();
            ScientificExpertises = new Collection<ScientificExpertise>();
            ImprovementSuggestions = new Collection<ImprovementSuggestion>();
            Synopses = new Collection<DissertationWork>();
            AcademicDegreeChangings = new Collection<AcademicDegreeChanging>();
            Participations = new Collection<Participation>();
        }
        // ReSharper restore DoNotCallOverridableMethodsInConstructor

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