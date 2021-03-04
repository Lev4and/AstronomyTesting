using AstronomyTesting.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstronomyTesting.Model.Repositories.Abstract
{
    public interface IRolesRepository
    {
        public int GetRoleIdByRoleName(string roleName);

        public IQueryable<Role> GetRoles();
    }
}
