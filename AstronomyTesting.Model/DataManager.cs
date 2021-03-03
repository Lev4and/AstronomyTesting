using AstronomyTesting.Model.Repositories.Abstract;

namespace AstronomyTesting.Model
{
    public class DataManager
    {
        public IUsersRepository Users { get; set; }

        public DataManager(IUsersRepository usersRepository)
        {
            Users = usersRepository;
        }
    }
}
