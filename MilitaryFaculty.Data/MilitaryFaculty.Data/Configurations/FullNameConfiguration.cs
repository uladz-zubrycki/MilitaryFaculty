using System.Data.Entity.ModelConfiguration;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Data
{
    internal class FullNameConfiguration : ComplexTypeConfiguration<FullName>
    {
        public FullNameConfiguration()
        {
            Property(m => m.FirstName).IsRequired()
                                      .HasMaxLength(FullName.FirstNameMaxLength);
            Property(m => m.MiddleName).IsRequired()
                                       .HasMaxLength(FullName.MiddleNameMaxLength);
            Property(m => m.LastName).IsRequired()
                                     .HasMaxLength(FullName.LastNameMaxLength);
        }
    }
}