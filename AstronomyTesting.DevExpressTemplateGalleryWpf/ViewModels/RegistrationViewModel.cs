using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private RolesRestClient _rolesClient;
        private GroupsRestClient _groupsClient;
        private AccountRestClient _accountsClient;
        private StudentsRestClient _studentsClient;

        public virtual object SelectedGroupId { get; set; }

        public virtual string FullName { get; set; }

        public virtual string Password { get; set; }

        public virtual string RepeatPassword { get; set; }

        public virtual List<dynamic> Groups { get; set; }

        public INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        protected RegistrationViewModel()
        {
            _rolesClient = new RolesRestClient();
            _groupsClient = new GroupsRestClient();
            _accountsClient = new AccountRestClient();
            _studentsClient = new StudentsRestClient();

            SelectedGroupId = null;

            FullName = "";
            Password = "";
            RepeatPassword = "";

            Groups = HttpResponseMessageConverter.GetResult<List<dynamic>>(_groupsClient.GetGroups());
        }

        public static RegistrationViewModel Create()
        {
            return ViewModelSource.Create(() => new RegistrationViewModel());
        }

        public bool CanRegistration()
        {
            if (SelectedGroupId != null && FullName != null && Password != null && RepeatPassword != null)
            {
                return FullName.Length > 0 && Password.Length > 0 && RepeatPassword.Length > 0 && Password == RepeatPassword;
            }

            return false;
        }

        public void Registration()
        {
            var roleId = HttpResponseMessageConverter.GetResult<int>(_rolesClient.GetRoleIdByRoleName("Студент"));
            
            if(HttpResponseMessageConverter.GetResult<bool>(_accountsClient.AddUser(roleId, FullName, Password)))
            {
                var user = HttpResponseMessageConverter.GetResult<dynamic>(_accountsClient.GetUserByFullNameAndPassword(FullName, Password));
                var userId = Convert.ToInt32(user.id);

                _studentsClient.AddStudent(userId, Convert.ToInt32(SelectedGroupId));

                NavigationService.Navigate("AuthorizationView");
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными уже существует.", "Ошибка");
            }
        }

        public void Back()
        {
            NavigationService.Navigate("MainView");
        }
    }
}
