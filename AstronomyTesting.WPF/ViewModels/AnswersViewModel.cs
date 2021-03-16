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
    public class AnswersViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;

        private AnswersRestClient _answersClient;

        public static int? TestId { get; set; } = null;

        public static int? QuestionId { get; set; } = null;

        public object SelectedAnswerId { get; set; } = null;

        public ObservableCollection<dynamic> Answers { get; set; } = new ObservableCollection<dynamic>();

        public AnswersViewModel(PageService pageService, UserService userService, AnswersRestClient answersClient)
        {
            _pageService = pageService;
            _userService = userService;

            _answersClient = answersClient;

            if(QuestionId != null)
            {
                Answers = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_answersClient.GetAnswersByQuestionId((int)QuestionId)));
            }
        }

        public ICommand Add => new DelegateCommand(() =>
        {
            AddOrEditAnswerViewModel.IsAdding = true;

            AddOrEditAnswerViewModel.TestId = TestId;
            AddOrEditAnswerViewModel.AnswerId = null;
            AddOrEditAnswerViewModel.QuestionId = QuestionId;

            _pageService.ChangePage(new AddOrEditAnswer());
        }, () => { return _userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false; });

        public ICommand Change => new DelegateCommand(() =>
        {
            AddOrEditAnswerViewModel.IsAdding = false;

            AddOrEditAnswerViewModel.TestId = TestId;
            AddOrEditAnswerViewModel.AnswerId = Convert.ToInt32(SelectedAnswerId);
            AddOrEditAnswerViewModel.QuestionId = QuestionId;

            _pageService.ChangePage(new AddOrEditAnswer());
        }, () => { return SelectedAnswerId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _answersClient.RemoveAnswerById(Convert.ToInt32(SelectedAnswerId)).Wait();

                Answers.Clear();
                Answers = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_answersClient.GetAnswersByQuestionId((int)QuestionId)));
            }
        }, () => { return SelectedAnswerId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Back => new DelegateCommand(() =>
        {
            QuestionsViewModel.TestId = null;
            QuestionsViewModel.TestId = TestId;

            _pageService.ChangePage(new Questions());
        });
    }
}
