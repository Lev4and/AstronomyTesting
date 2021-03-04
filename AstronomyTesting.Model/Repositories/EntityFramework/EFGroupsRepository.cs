using AstronomyTesting.Model.Entities;
using AstronomyTesting.Model.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AstronomyTesting.Model.Repositories.EntityFramework
{
    public class EFGroupsRepository : IGroupsRepository
    {
        private AstronomyTestingContext _context;

        public EFGroupsRepository(AstronomyTestingContext context)
        {
            _context = context;
        }

        public IQueryable<Group> GetGroups()
        {
            return _context.Groups.AsQueryable().AsNoTracking();
        }
    }
}
