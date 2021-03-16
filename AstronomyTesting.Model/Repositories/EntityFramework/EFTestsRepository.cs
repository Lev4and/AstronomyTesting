using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFTestsRepository : ITestsRepository
    {
        private AstronomyTestingContext _context;

        public EFTestsRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddTest(string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions)
        {
            if (!ContainsTest(name))
            {
                _context.Tests.Add(new Test
                {
                    Name = name,
                    Description = description,
                    DateOfCreating = DateTime.Now,
                    StartDate = startDate,
                    ExpirationDate = expirationDate,
                    Duration = duration,
                    MaximumNumberOfQuestions = maximumNumberOfQuestions
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsTest(string name)
        {
            return _context.Tests.SingleOrDefault(t => t.Name == name) != null;
        }

        public Test GetTestById(int id)
        {
            return _context.Tests.SingleOrDefault(t => t.Id == id);
        }

        public IQueryable<Test> GetTests()
        {
            return _context.Tests.AsNoTracking();
        }

        public void RemoveTestById(int id)
        {
            _context.Tests.Remove(GetTestById(id));
            _context.SaveChanges();
        }

        public bool UpdateTestById(int id, string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions)
        {
            var test = GetTestById(id);

            if (test.Name != name ? !ContainsTest(name) : true)
            {
                test.Name = name;
                test.Description = description;
                test.StartDate = startDate;
                test.ExpirationDate = expirationDate;
                test.Duration = duration;
                test.MaximumNumberOfQuestions = maximumNumberOfQuestions;

                _context.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
