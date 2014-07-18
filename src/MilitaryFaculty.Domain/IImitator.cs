namespace MilitaryFaculty.Domain
{
    public interface IImitator<in T>
    {
        void Imitate(T other);
    }
}