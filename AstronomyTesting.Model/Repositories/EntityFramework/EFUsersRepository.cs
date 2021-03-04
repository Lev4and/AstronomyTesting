using System;
using System.Linq;
using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFUsersRepository : IUsersRepository
    {
        private AstronomyTestingContext _context;

        public EFUsersRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public bool AddUser(int roleId, string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                throw new ArgumentNullException("fullName", "Полное имя пользователя не может быть пустым или длиной 0 символов.");
            }

            if (password != null ? password.Length == 0 : true)
            {
                throw new ArgumentNullException("password", "Пароль не может быть пустым или длиной 0 символов.");
            }

            if (!ContainsUser(fullName, password))
            {
                _context.Users.Add(new User
                {
                    RoleId = roleId,
                    FullName = fullName,
                    Password = password
                });
                _context.SaveChanges();

                return true;
            }

            return false;
        }

        public bool ContainsUser(string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                throw new ArgumentNullException("fullName", "Полное имя пользователя не может быть пустым или длиной 0 символов.");
            }

            if(password != null ? password.Length == 0 : true)
            {
                throw new ArgumentNullException("password", "Пароль не может быть пустым или длиной 0 символов.");
            }

            return _context.Users.SingleOrDefault(u => u.FullName == fullName && u.Password == password) != null;
        }

        public User GetUserByFullNameAndPassword(string fullName, string password)
        {
            if (fullName != null ? fullName.Length == 0 : true)
            {
                throw new ArgumentNullException("fullName", "Полное имя пользователя не может быть пустым или длиной 0 символов.");
            }

            if (password != null ? password.Length == 0 : true)
            {
                throw new ArgumentNullException("password", "Пароль не может быть пустым или длиной 0 символов.");
            }

            return _context.Users.Include(u => u.Role).FirstOrDefault(u => u.FullName == fullName && u.Password == password);
        }

        public IQueryable<User> GetUsers()
        {
            return _context.Users.Include(u => u.Role).AsNoTracking();
        }
    }
}
