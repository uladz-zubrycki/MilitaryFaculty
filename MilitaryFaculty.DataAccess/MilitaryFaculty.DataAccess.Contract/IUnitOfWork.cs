using System;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.DataAccess.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Cathedra> CathedraRepository { get; }
        IRepository<Conference> ConferenceRepository { get; }
        IRepository<Professor> ProfessorRepository { get; }

        void SaveChanges();
    }
}
