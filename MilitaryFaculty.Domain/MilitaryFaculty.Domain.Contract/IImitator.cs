namespace MilitaryFaculty.Domain.Contract
{
    public interface IImitator<in T>
    {
        void Imitate(T other);
    }
}