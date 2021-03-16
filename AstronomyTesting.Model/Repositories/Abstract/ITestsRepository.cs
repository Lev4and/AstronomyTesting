using AstronomyTesting.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface ITestsRepository
    {
        bool ContainsTest(string name);

        bool AddTest(string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions);

        bool UpdateTestById(int id, string name, string description, DateTime startDate, DateTime expirationDate, int? duration, int maximumNumberOfQuestions);

        Test GetTestById(int id);

        IQueryable<Test> GetTests();

        void RemoveTestById(int id);
    }
}
