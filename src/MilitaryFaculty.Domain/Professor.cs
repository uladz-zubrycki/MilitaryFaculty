using System;
using System.Collections.Generic;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum JobPosition
    {
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

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Professor : UniqueEntity
    {
        public virtual FullName FullName { get; set; }
        public virtual Cathedra Cathedra { get; set; }
        public virtual DateTime EnrollmentDate { get; set; }
        public virtual DateTime? DismissalDate { get; set; }
        public virtual MilitaryRank MilitaryRank { get; set; }
        public virtual JobPosition JobPosition { get; set; }
        public virtual AcademicRank AcademicRank { get; set; }
        public virtual AcademicDegree AcademicDegree { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Conference> Conferences { get; set; }
        public virtual ICollection<CouncilParticipation> CouncilsParticipations { get; set; }
        public virtual ICollection<Dissertation> Dissertations { get; set; }
        public virtual ICollection<EfficiencyProposal> EfficiencyProposals { get; set; }
        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<InventiveApplication> InventiveApplications { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Research> Researches { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<ScientificExpertise> ScientificExpertises { get; set; }

        public Professor()
        {
            FullName = new FullName();
            EnrollmentDate = DateTime.Now;

            Books = new List<Book>();
            Conferences = new List<Conference>();
            CouncilsParticipations = new List<CouncilParticipation>();
            Dissertations = new List<Dissertation>();
            EfficiencyProposals = new List<EfficiencyProposal>();
            Exhibitions = new List<Exhibition>();
            InventiveApplications = new List<InventiveApplication>();
            Publications = new List<Publication>();
            Researches = new List<Research>();
            Reviews = new List<Review>();
            ScientificExpertises = new List<ScientificExpertise>();
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}