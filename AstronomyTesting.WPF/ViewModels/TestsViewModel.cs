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
    public class TestsViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;

        private TestsRestClient _testsClient;
        private StudentsRestClient _studentsClient;
        private QuestionsRestClient _questionsClient;
        private StudentAnswersRestClient _studentAnswersClient;

        public object SelectedTestId { get; set; } = null;

        public ObservableCollection<dynamic> Tests { get; set; } = new ObservableCollection<dynamic>();


        public TestsViewModel(PageService pageService, UserService userService, TestsRestClient testsClient, StudentsRestClient studentsClient, QuestionsRestClient questionsRestClient, StudentAnswersRestClient studentAnswersClient)
        {
            _pageService = pageService;
            _userService = userService;

            _testsClient = testsClient;
            _studentsClient = studentsClient;
            _questionsClient = questionsRestClient;
            _studentAnswersClient = studentAnswersClient;

            Tests.Clear();
            Tests = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_testsClient.GetTests()));
        }

        public ICommand Add => new DelegateCommand(() =>
        {
            AddOrEditTestViewModel.IsAdding = true;
            AddOrEditTestViewModel.TestId = null;

            _pageService.ChangePage(new AddOrEditTest());
        }, () => { return _userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false; });

        public ICommand Change => new DelegateCommand(() =>
        {
            AddOrEditTestViewModel.IsAdding = false;
            AddOrEditTestViewModel.TestId = Convert.ToInt32(SelectedTestId);

            _pageService.ChangePage(new AddOrEditTest());
        }, () => { return SelectedTestId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Remove => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись из базы данных?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _testsClient.RemoveTestById(Convert.ToInt32(SelectedTestId)).Wait();

                Tests = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_testsClient.GetTests()));
            }
        }, () => { return SelectedTestId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Details => new DelegateCommand(() =>
        {
            QuestionsViewModel.TestId = null;
            QuestionsViewModel.TestId = Convert.ToInt32(SelectedTestId);

            _pageService.ChangePage(new Questions());
        }, () => { return SelectedTestId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Play => new DelegateCommand(() =>
        {
            var time = DateTime.Now;
            var test = HttpResponseMessageConverter.GetResult<dynamic>(_testsClient.GetTestById(Convert.ToInt32(SelectedTestId)));

            var questions = HttpResponseMessageConverter.GetResult<List<dynamic>>(_questionsClient.GetQuestionsByTestId(Convert.ToInt32(SelectedTestId)));

            var student = HttpResponseMessageConverter.GetResult<dynamic>(_studentsClient.StudentByUserId(Convert.ToInt32(_userService.UserInformation.id)));
            var studentAnswers = HttpResponseMessageConverter.GetResult<List<dynamic>>(_studentAnswersClient.GetStudentAnswers(Convert.ToInt32(student.id), Convert.ToInt32(SelectedTestId)));

            if (studentAnswers.Count == 0)
            {
                if(questions.Count > 0)
                {
                    if (time >= Convert.ToDateTime(test.startDate) && time <= Convert.ToDateTime(test.expirationDate))
                    {
                        if (MessageBox.Show("Вы действительно хотите пройти выбранный тест?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            PassingTheTestViewModel.TestId = null;
                            PassingTheTestViewModel.TestId = Convert.ToInt32(SelectedTestId);

                            _pageService.ChangePage(new PassingTheTest());
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Отказано в доступе, так как тестирование прекращено или не начато", "Ошибка");
                    }
                }
                else
                {
                    MessageBox.Show($"Отказано в доступе, так как тест не содержит вопросы", "Ошибка");
                }
            }
            else
            {
                var countCorrectedAnswers = 0;
                var countQuestions = questions.Count < Convert.ToInt32(test.maximumNumberOfQuestions) ? questions.Count : (Convert.ToInt32(studentAnswers.Count) < Convert.ToInt32(test.maximumNumberOfQuestions) ? Convert.ToInt32(test.maximumNumberOfQuestions) : Convert.ToInt32(studentAnswers.Count));

                foreach (var studentAnswer in studentAnswers)
                {
                    if (Convert.ToBoolean(studentAnswer.answer.isCorrect))
                    {
                        countCorrectedAnswers += 1;
                    }
                }

                MessageBox.Show($"Отказано в доступе, так как вы уже проходили данный тест (Результат: {countCorrectedAnswers} / {countQuestions} ({((double)((double)countCorrectedAnswers / (double)countQuestions) * 100).ToString("F2")} %))", "Ошибка");
            }
        }, () => { return SelectedTestId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Студент" : false); });

        public ICommand Statistics => new DelegateCommand(() =>
        {
            DetailsOfTheTestViewModel.TestId = null;
            DetailsOfTheTestViewModel.TestId = Convert.ToInt32(SelectedTestId);

            _pageService.ChangePage(new DetailsOfTheTest());
        }, () => { return SelectedTestId != null && (_userService.UserInformation != null ? _userService.UserInformation.role.name == "Администратор" : false); });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Menu());
        });
    }
}
