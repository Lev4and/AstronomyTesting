using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Commands;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class RegistrationViewModel : BindableBase
    {
        private PageService _pageService;

        private RolesRestClient _rolesClient;
        private GroupsRestClient _groupsClient;
        private AccountsRestClient _accountsClient;
        private StudentsRestClient _studentsClient;

        public string FullName { get; set; } = "";

        public string Password { get; set; } = "";

        public string RepeatPassword { get; set; } = "";

        public object SelectedGroupId { get; set; } = null;

        public List<dynamic> Groups { get; set; } = new List<dynamic>();

        public RegistrationViewModel(PageService pageService, RolesRestClient rolesClient, GroupsRestClient groupsClient, AccountsRestClient accountsClient, StudentsRestClient studentsClient)
        {
            _pageService = pageService;

            _rolesClient = rolesClient;
            _groupsClient = groupsClient;
            _accountsClient = accountsClient;
            _studentsClient = studentsClient;

            Groups = HttpResponseMessageConverter.GetResult<List<dynamic>>(_groupsClient.GetGroups());
        }

        public ICommand PasswordChanged => new Command((obj) =>
        {
            if (obj != null)
            {
                Password = (obj as PasswordBox).Password;
            }
        });

        public ICommand RepeatPasswordChanged => new Command((obj) =>
        {
            if (obj != null)
            {
                RepeatPassword = (obj as PasswordBox).Password;
            }
        });

        public ICommand Registration => new DelegateCommand(() =>
        {
            var roleId = HttpResponseMessageConverter.GetResult<int>(_rolesClient.GetRoleIdByRoleName("Студент"));

            if (HttpResponseMessageConverter.GetResult<bool>(_accountsClient.AddUser(roleId, FullName, Password)))
            {
                var user = HttpResponseMessageConverter.GetResult<dynamic>(_accountsClient.GetUserByFullNameAndPassword(FullName, Password));

                _studentsClient.AddStudent(Convert.ToInt32(user.id), Convert.ToInt32(SelectedGroupId));

                _pageService.ChangePage(new Authorization());
            }
            else
            {
                MessageBox.Show("Пользователь с такими данными уже существует.", "Ошибка");
            }
        }, () => { return SelectedGroupId != null && FullName != null && Password != null && RepeatPassword != null ? FullName.Length > 0 && Password.Length > 0 && RepeatPassword.Length > 0 && Password == RepeatPassword : false; });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Main());
        });
    }
}
