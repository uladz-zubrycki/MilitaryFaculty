using System.Data.Entity.ModelConfiguration;

namespace MilitaryFaculty.Data.Configurations
{
    internal class FullName : ComplexTypeConfiguration<Domain.FullName>
    {
        public FullName()
        {
            Property(m => m.FirstName).IsRequired().HasMaxLength(Domain.FullName.FirstNameMaxLength);
            Property(m => m.MiddleName).IsRequired().HasMaxLength(Domain.FullName.MiddleNameMaxLength);
            Property(m => m.LastName).IsRequired().HasMaxLength(Domain.FullName.LastNameMaxLength);
        }
    }
}