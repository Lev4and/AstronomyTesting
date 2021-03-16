using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Commands;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class AuthorizationViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;

        private AccountsRestClient _accountsClient;

        public string FullName { get; set; } = "";

        public string Password { get; set; } = "";

        public AuthorizationViewModel(PageService pageService, UserService userService, AccountsRestClient accountsClient)
        {
            _pageService = pageService;
            _userService = userService;

            _accountsClient = accountsClient;
        }

        public ICommand PasswordChanged => new Command((obj) =>
        {
            if (obj != null)
            {
                Password = (obj as PasswordBox).Password;
            }
        });

        public ICommand Authorization => new DelegateCommand(() =>
        {
            if (HttpResponseMessageConverter.GetResult<bool>(_accountsClient.ContainsUser(FullName, Password)))
            {
                _userService.UserInformation = null;
                _userService.UserInformation = HttpResponseMessageConverter.GetResult<dynamic>(_accountsClient.GetUserByFullNameAndPassword(FullName, Password));
                _pageService.ChangePage(new AstronomyTesting.WPF.Views.Pages.Menu());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка");
            }
        }, () => { return FullName != null && Password != null ? FullName.Length > 0 && Password.Length > 0 : false; });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Main());
        });
    }
}
