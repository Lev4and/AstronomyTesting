using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFRolesRepository : IRolesRepository
    {
        private AstronomyTestingContext _context;

        public EFRolesRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public int GetRoleIdByRoleName(string roleName)
        {
            if(roleName != null ? roleName.Length == 0 : true)
            {
                throw new ArgumentNullException("roleName", "Должность пользователя не может быть пустым или длиной 0 символов.");
            }

            return _context.Roles.SingleOrDefault(r => r.Name == roleName).Id;
        }

        public IQueryable<Role> GetRoles()
        {
            return _context.Roles.AsQueryable().AsNoTracking();
        }
    }
}
