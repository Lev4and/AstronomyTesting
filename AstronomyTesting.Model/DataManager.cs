using AstronomyTesting.Model.Repositories.Abstract;

namespace AstronomyTesting.Model
{
    public class DataManager
    {
        public IGroupsRepository Groups { get; set; }

        public IRolesRepository Roles { get; set; }

        public IStudentsRepository Students { get; set; }

        public IUsersRepository Users { get; set; }

        public DataManager(IRolesRepository rolesRepository, IGroupsRepository groupsRepository, IStudentsRepository studentsRepository, IUsersRepository usersRepository)
        {
            Groups = groupsRepository;
            Roles = rolesRepository;
            Students = studentsRepository;
            Users = usersRepository;
        }
    }
}
