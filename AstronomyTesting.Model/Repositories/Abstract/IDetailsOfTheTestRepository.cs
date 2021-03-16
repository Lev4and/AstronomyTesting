using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IDetailsOfTheTestRepository
    {
        IQueryable<dynamic> GetDetailsOfTheTest(int testId);

        IQueryable<dynamic> GetDetailsOfTheStudentPassingTheTest(int testId, int studentId);
    }
}
