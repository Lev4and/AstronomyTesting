using AstronomyTesting.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IStudentsRepository
    {
        bool AddStudent(int userId, int groupId);

        bool ContainsStudent(int userId);

        Student GetStudentByUserId(int userId);
    }
}
