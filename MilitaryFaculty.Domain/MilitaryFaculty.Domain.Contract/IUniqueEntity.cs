using System;

namespace MilitaryFaculty.Domain.Contract
{
    public interface IUniqueEntity : IEquatable<IUniqueEntity>
    {
        Guid Id { get; }
    }
}