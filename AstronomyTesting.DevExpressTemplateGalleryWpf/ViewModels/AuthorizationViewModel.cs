using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private RolesRestClient _rolesClient;
        private AccountRestClient _accountsClient;
        private StudentsRestClient _studentsClient;

        public virtual string FullName { get; set; }

        public virtual string Password { get; set; }

        public INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        protected AuthorizationViewModel()
        {
            _rolesClient = new RolesRestClient();

            _accountsClient = new AccountRestClient();
            _studentsClient = new StudentsRestClient();

            FullName = "";
            Password = "";
        }

        public static AuthorizationViewModel Create()
        {
            return ViewModelSource.Create(() => new AuthorizationViewModel());
        }

        public bool CanAuthorization()
        {
            if(FullName != null && Password != null)
            {
                return FullName.Length > 0 && Password.Length > 0;
            }

            return false;
        }

        public void Authorization()
        {
            if (HttpResponseMessageConverter.GetResult<bool>(_accountsClient.ContainsUser(FullName, Password)))
            {
                MessageBox.Show("Успех.");
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка");
            }
        }

        public void Back()
        {
            NavigationService.Navigate("MainView");
        }
    }
}
