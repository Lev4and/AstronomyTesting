using AstronomyTesting.Model.Entities;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IUsersRepository
    {
        bool AddUser(int roleId, string fullName, string password);

        bool ContainsUser(string fullName, string password);

        User GetUserByFullNameAndPassword(string fullName, string password);

        IQueryable<User> GetUsers();
    }
}
