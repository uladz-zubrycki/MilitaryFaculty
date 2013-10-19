using VNO.DataAccess;
using VNO.Domain;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Cathedra("Second Cathedra");
            var cr = new CathedraRepository();

            cr.Create(c);
        }
    }
}
