namespace MilitaryFaculty.Domain.Base
{
    public interface IImitator<in T>
    {
        void Imitate(T other);
    }
}