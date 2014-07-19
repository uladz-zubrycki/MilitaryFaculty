using System;
using System.Collections.Generic;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum HistoricalWorkOrganization
    {
        None,
        Custom,
        Full,
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum MilitaryScientificSocietyOrganization
    {
        None,
        Custom,
        Full,
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum ProfsOrganization
    {
        None,
        Custom,
        Full,
    }

    /// <summary>
    ///     University subdepartment.
    /// </summary>
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Cathedra : UniqueEntity, IImitator<Cathedra>
    {
        public const int NameMaxLength = 50;

        public virtual string Name { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }
        public virtual int DevelopmentOfPlanningDocuments { get; set; }
        public virtual int ResearchTopics { get; set; }
        public virtual int RequirementsOfLegalActs { get; set; }

        //TODO: Max valudation values
        //international conferences
        public virtual int LevelOfOrganizationIc { get; set; }
        //republican conference
        public virtual int LevelOfOrganizationRc { get; set; }
        //university conference
        public virtual int LevelOfOrganizationUc { get; set; }
        //republican research semenar
        public virtual int LevelOfOrganizationRrs { get; set; }
        //university research semenar
        public virtual int LevelOfOrganizationUrs { get; set; }

        //Показатели, характеризующие качество организации научной работы
        public virtual int CustSwOrg { get; set; }
        //Показатели, характеризующие проведение научных исследований
        public virtual int CustSrOrg { get; set; }
        //Показатели, характеризующие апробацию результатов научных исследований
        public virtual int CustArsrOrg { get; set; }
        //Показатели, характеризующие подготовку и аттестацию научных работников высшей квалификации
        public virtual int CustTcSpHq { get; set; }

        public virtual HistoricalWorkOrganization HistoricalWorkOrganization { get; set; }
        public virtual MilitaryScientificSocietyOrganization MilitaryScientificSupportState { get; set; }
        public virtual ProfsOrganization ProfsOrganization { get; set; }

        public Cathedra()
        {
            Name = String.Empty;
        }

        public Cathedra(string name)
            : this()
        {
            Name = name;
        }

        public Cathedra(Cathedra other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(Cathedra other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;

            MilitaryScientificSupportState = other.MilitaryScientificSupportState;
            HistoricalWorkOrganization = other.HistoricalWorkOrganization;
            ProfsOrganization = other.ProfsOrganization;
            LevelOfOrganizationIc = other.LevelOfOrganizationIc;
            LevelOfOrganizationRc = other.LevelOfOrganizationRc;
            LevelOfOrganizationRrs = other.LevelOfOrganizationRrs;
            LevelOfOrganizationUc = other.LevelOfOrganizationUc;
            LevelOfOrganizationUrs = other.LevelOfOrganizationUrs;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}