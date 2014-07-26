﻿using System;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Domain
{
    // ReSharper disable DoNotCallOverridableMethodsInConstructor
    // Properties are virtual only for EntityFramework
    public class Dissertation : UniqueEntity
    {
        public Dissertation()
        {
            CreatedAt = DateTime.Now;
        }

        public virtual string Name { get; set; }
        public virtual Professor Author { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual AcademicRank TargetAcademicRank { get; set; }
    }
    // ReSharper restore DoNotCallOverridableMethodsInConstructor
}