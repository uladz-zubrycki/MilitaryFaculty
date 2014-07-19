using System;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Domain.Resources;

namespace MilitaryFaculty.Domain
{
    [LocalizedEnum(typeof(EnumStrings))]
    public enum ScientificRequestResponce
    {
        Negative,
        Positive,
    }

    [LocalizedEnum(typeof(EnumStrings))]
    public enum ScientificRequestType
    {
        Invention,
        UtilityModel
    }

    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class ScientificRequest : UniqueEntity, IImitator<ScientificRequest>
    {
        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime Date { get; set; }

        public virtual ScientificRequestType ScientificType { get; set; }
        public virtual ScientificRequestResponce ScientificResponce { get; set; }
      
        public ScientificRequest()
        {
            Name = String.Empty;
            Date = DateTime.Now;
        }

        public ScientificRequest(ScientificRequest other)
            : this()
        {
            Imitate(other);
        }

        public void Imitate(ScientificRequest other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }

            Id = other.Id;
            Name = other.Name;
            Author = other.Author;
            Date = other.Date;
            ScientificType = other.ScientificType;
            ScientificResponce = other.ScientificResponce;
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}
