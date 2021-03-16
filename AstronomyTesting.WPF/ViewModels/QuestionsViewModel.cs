using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class QuestionsViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;

        private QuestionsRestClient _questionsClient;

        public static int? TestId { get; set; } = null;

        public object SelectedQuestionId { get; set; } = null;

        public ObservableCollection<dynamic> Questions { get; set; } = new ObservableCollection<dynamic>();

        public QuestionsViewModel(PageService pageService, UserService userService, QuestionsRestClient questionsClient)
        {
            _pageService = pageService;
            _userService = userService;

            _questionsClient = questionsClient;

            if(TestId != null)
            {
                Questions = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_questionsClient.GetQuestionsByTestId((int)TestId)));
            }
        }

        public ICommand Add => new DelegateCommand(() =>
        {
            AddOrEditQuestionViewModel.IsAdding = true;

            AddOrEditQuestionViewModel.TestId = TestId;
            AddOrEditQuestionViewModel.QuestionId = null;

            _pageService.ChangePage(new AddOrEditQuestion());
        }, () => { return _userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false; });

        public ICommand Change => new DelegateCommand(() =>
        {
            AddOrEditQuestionViewModel.IsAdding = false;

            AddOrEditQuestionViewModel.TestId = TestId;
            AddOrEditQuestionViewModel.QuestionId = Convert.ToInt32(SelectedQuestionId);

            _pageService.ChangePage(new AddOrEditQuestion());
        }, () => { return SelectedQuestionId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _questionsClient.RemoveQuestionById(Convert.ToInt32(SelectedQuestionId)).Wait();

                Questions.Clear();
                Questions = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_questionsClient.GetQuestionsByTestId((int)TestId)));
            }
        }, () => { return SelectedQuestionId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Details => new DelegateCommand(() =>
        {
            AnswersViewModel.TestId = null;
            AnswersViewModel.TestId = Convert.ToInt32(TestId);

            AnswersViewModel.QuestionId = null;
            AnswersViewModel.QuestionId = Convert.ToInt32(SelectedQuestionId);

            _pageService.ChangePage(new Answers());
        }, () => { return SelectedQuestionId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Tests());
        });
    }
}
