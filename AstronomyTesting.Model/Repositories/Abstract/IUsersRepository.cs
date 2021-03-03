using AstronomyTesting.Model.Entities;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IUsersRepository
    {
        bool ContainsUser(string fullName, string password);

        User GetUserByFullNameAndPassword(string fullName, string password);

        IQueryable<User> GetUsers();
    }
}
