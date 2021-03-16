using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFDetailsOfTheTestRepository : IDetailsOfTheTestRepository
    {
        private AstronomyTestingContext _context;

        public EFDetailsOfTheTestRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public IQueryable<dynamic> GetDetailsOfTheStudentPassingTheTest(int testId, int studentId)
        {
            return _context.VDetailsOfTheStudentPassingTheTests.Where(d => d.TestId == testId && d.StudentId == studentId).AsNoTracking();
        }

        public IQueryable<dynamic> GetDetailsOfTheTest(int testId)
        {
            return _context.VDetailsOfTheTests.Where(d => d.TestId == testId).AsNoTracking();
        }
    }
}
