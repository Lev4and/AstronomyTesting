using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFStudentsRepository : IStudentsRepository
    {
        private AstronomyTestingContext _context;

        public EFStudentsRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddStudent(int userId, int groupId)
        {
            if (!ContainsStudent(userId))
            {
                _context.Students.Add(new Student
                {
                    UserId = userId,
                    GroupId = groupId
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsStudent(int userId)
        {
            return _context.Students.SingleOrDefault(s => s.UserId == userId) != null;
        }

        public Student GetStudentByUserId(int userId)
        {
            return _context.Students.SingleOrDefault(s => s.UserId == userId);
        }
    }
}
