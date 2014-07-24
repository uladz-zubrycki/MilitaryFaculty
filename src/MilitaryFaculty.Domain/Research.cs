using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Research: UniqueEntity
    {
        public string Name { get; set; }
        public Professor Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Professor> Maintaners { get; set; }

        public Research()
        {
            CreatedAt = DateTime.Now;
            Maintaners = new List<Professor>();
        }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}