using AstronomyTesting.Model.Repositories.Abstract;

namespace AstronomyTesting.Model
{
    public class DataManager
    {
        public IUsersRepository Users { get; set; }

        public IRolesRepository Roles { get; set; }

        public ITestsRepository Tests { get; set; }

        public IGroupsRepository Groups { get; set; }

        public IAnswersRepository Answers { get; set; }

        public IStudentsRepository Students { get; set; }

        public IQuestionsRepository Questions { get; set; }

        public IStudentAnswersRepository StudentAnswers { get; set; }

        public IDetailsOfTheTestRepository DetailsOfTheTest { get; set; }

        public DataManager(IDetailsOfTheTestRepository detailsOfTheTestRepository, IStudentAnswersRepository studentAnswersRepository, IAnswersRepository answersRepository, IQuestionsRepository questionsRepository, ITestsRepository testsRepository, IRolesRepository rolesRepository, IGroupsRepository groupsRepository, IStudentsRepository studentsRepository, IUsersRepository usersRepository)
        {
            Users = usersRepository;
            Roles = rolesRepository;
            Tests = testsRepository;
            Groups = groupsRepository;
            Answers = answersRepository;
            Students = studentsRepository;
            Questions = questionsRepository;
            StudentAnswers = studentAnswersRepository;
            DetailsOfTheTest = detailsOfTheTestRepository;
        }
    }
}
