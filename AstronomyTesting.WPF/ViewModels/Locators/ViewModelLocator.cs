using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AstronomyTesting.WPF.ViewModels.Locators
{
    public class ViewModelLocator
    {
        private static ServiceProvider _provider;

        public MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();

        public MenuViewModel MenuViewModel => _provider.GetRequiredService<MenuViewModel>();

        public TestsViewModel TestsViewModel => _provider.GetRequiredService<TestsViewModel>();

        public AnswersViewModel AnswersViewModel => _provider.GetRequiredService<AnswersViewModel>();

        public QuestionsViewModel QuestionsViewModel => _provider.GetRequiredService<QuestionsViewModel>();

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();

        public RegistrationViewModel RegistrationViewModel => _provider.GetRequiredService<RegistrationViewModel>();

        public AuthorizationViewModel AuthorizationViewModel => _provider.GetRequiredService<AuthorizationViewModel>();

        public AddOrEditTestViewModel AddOrEditTestViewModel => _provider.GetRequiredService<AddOrEditTestViewModel>();

        public PassingTheTestViewModel PassingTheTestViewModel => _provider.GetRequiredService<PassingTheTestViewModel>();

        public AddOrEditAnswerViewModel AddOrEditAnswerViewModel => _provider.GetRequiredService<AddOrEditAnswerViewModel>();

        public DetailsOfTheTestViewModel DetailsOfTheTestViewModel => _provider.GetRequiredService<DetailsOfTheTestViewModel>();

        public AddOrEditQuestionViewModel AddOrEditQuestionViewModel => _provider.GetRequiredService<AddOrEditQuestionViewModel>();

        public DetailsOfTheStudentPassingTheTestViewModel DetailsOfTheStudentPassingTheTestViewModel => _provider.GetRequiredService<DetailsOfTheStudentPassingTheTestViewModel>();

        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MenuViewModel>();
            services.AddTransient<TestsViewModel>();
            services.AddTransient<AnswersViewModel>();
            services.AddTransient<QuestionsViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<AuthorizationViewModel>();
            services.AddTransient<AddOrEditTestViewModel>();
            services.AddTransient<PassingTheTestViewModel>();
            services.AddTransient<AddOrEditAnswerViewModel>();
            services.AddTransient<DetailsOfTheTestViewModel>();
            services.AddTransient<AddOrEditQuestionViewModel>();
            services.AddTransient<DetailsOfTheStudentPassingTheTestViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<UserService>();

            services.AddSingleton<TestsRestClient>();
            services.AddSingleton<RolesRestClient>();
            services.AddSingleton<GroupsRestClient>();
            services.AddSingleton<AnswersRestClient>();
            services.AddSingleton<AccountsRestClient>();
            services.AddSingleton<StudentsRestClient>();
            services.AddSingleton<QuestionsRestClient>();
            services.AddTransient<StudentAnswersRestClient>();
            services.AddTransient<DetailsOfTheTestRestClient>();

            _provider = services.BuildServiceProvider();

            foreach (var item in services)
            {
                _provider.GetRequiredService(item.ServiceType);
            }
        }
    }
}
