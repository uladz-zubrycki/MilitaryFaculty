using System;
using System.ComponentModel;
using MilitaryFaculty.Extensions;

namespace MilitaryFaculty.Domain
{
    public class ScientificRequest : UniqueEntity, IImitator<ScientificRequest>
    {
        private ScientificRequestType _scientificType { get; set; }
        private ScientificRequestResponce _scientificResponce { get; set; }

        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime Date { get; set; }

        public ScientificRequestType ScientificType
        {
            get { return _scientificType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _scientificType = value;
            }
        }

        public ScientificRequestResponce ScientificResponce
        {
            get { return _scientificResponce; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                _scientificResponce = value;
            }
        }

        public ScientificRequest()
        {
            Id = Guid.Empty;
            Name = String.Empty;
            Date = DateTime.Now;
            _scientificType = ScientificRequestType.Invention;
            _scientificResponce = ScientificRequestResponce.Positive;
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
}
