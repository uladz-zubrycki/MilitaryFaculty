using System;
using System.Collections.Generic;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    /// <summary>
    ///     University subdepartment.
    /// </summary>
    public class Cathedra : UniqueEntity, IImitator<Cathedra>
    {
        private HistoricalWorkOrganization _historicalWorkOrganization;
        private MilitaryScientificSocietyOrganization _militaryScientificSocietyOrganization;
        private ProfsOrganization _profsOrganization;

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

        public virtual HistoricalWorkOrganization HistoricalWorkOrganization
        {
            get { return _historicalWorkOrganization; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _historicalWorkOrganization = value;
            }
        }

        public virtual MilitaryScientificSocietyOrganization MilitaryScientificSupportState
        {
            get { return _militaryScientificSocietyOrganization; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _militaryScientificSocietyOrganization = value;
            }
        }

        public virtual ProfsOrganization ProfsOrganization
        {
            get { return _profsOrganization; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _profsOrganization = value;
            }
        }

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

            _militaryScientificSocietyOrganization = other.MilitaryScientificSupportState;
            _historicalWorkOrganization = other.HistoricalWorkOrganization;
            _profsOrganization = other.ProfsOrganization;

            LevelOfOrganizationIc = other.LevelOfOrganizationIc;
            LevelOfOrganizationRc = other.LevelOfOrganizationRc;
            LevelOfOrganizationRrs = other.LevelOfOrganizationRrs;
            LevelOfOrganizationUc = other.LevelOfOrganizationUc;
            LevelOfOrganizationUrs = other.LevelOfOrganizationUrs;
        }
    }
}