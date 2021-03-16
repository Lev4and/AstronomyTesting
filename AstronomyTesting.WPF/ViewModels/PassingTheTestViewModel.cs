using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class PassingTheTestViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;

        private TestsRestClient _testsClient;
        private AnswersRestClient _answersClient;
        private StudentsRestClient _studentsClient;
        private QuestionsRestClient _questionsClient;
        private StudentAnswersRestClient _studentAnswersClient;

        public static int? TestId { get; set; } = null;


        public object SelectedAnswer { get; set; } = null;

        public int NumberActiveQuestion { get; set; } = 0;

        public int CountQuestions { get; set; } = 0;

        public dynamic Test { get; set; } = null;

        public dynamic ActiveQuestion { get; set; } = null;

        public TimeSpan? TimeLeft { get; set; } = null;

        public ObservableCollection<dynamic> Answers { get; set; } = new ObservableCollection<dynamic>();

        public ObservableCollection<dynamic> Questions { get; set; } = new ObservableCollection<dynamic>();

        public PassingTheTestViewModel(PageService pageService, UserService userService, TestsRestClient testsClient, AnswersRestClient answersClient, StudentsRestClient studentsClient, QuestionsRestClient questionsClient, StudentAnswersRestClient studentAnswersClient)
        {
            _pageService = pageService;
            _userService = userService;

            _testsClient = testsClient;
            _answersClient = answersClient;
            _studentsClient = studentsClient;
            _questionsClient = questionsClient;
            _studentAnswersClient = studentAnswersClient;

            if (TestId != null)
            {
                Test = HttpResponseMessageConverter.GetResult<dynamic>(_testsClient.GetTestById((int)TestId));

                InitTimer();
                FormQuestions();

                ActiveQuestion = Questions[0];

                Answers.Clear();
                Answers = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_answersClient.GetAnswersByQuestionId(Convert.ToInt32(ActiveQuestion.id))));
            }
        }

        public int GetCountCorrectedAnswers()
        {
            var userId = Convert.ToInt32(_userService.UserInformation.id);
            var studentId = Convert.ToInt32(HttpResponseMessageConverter.GetResult<dynamic>(_studentsClient.StudentByUserId(userId)).id);
            var studentAnswers = HttpResponseMessageConverter.GetResult<List<dynamic>>(_studentAnswersClient.GetStudentAnswers(studentId, Convert.ToInt32(TestId)));
            var countCorrectedAnswers = 0;

            foreach(var studentAnswer in studentAnswers)
            {
                if (Convert.ToBoolean(studentAnswer.answer.isCorrect))
                {
                    countCorrectedAnswers += 1;
                }
            }

            return countCorrectedAnswers;

        }

        public void InitTimer()
        {
            if ((int?)Test.duration != null)
            {
                TimeLeft = new TimeSpan(0, 0, (int)Test.duration, 0);

                Tick();
            }
        }

        public void FormQuestions()
        {
            var random = new Random();
            var questions = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_questionsClient.GetQuestionsByTestId((int)TestId)));

            FormCounters(questions.Count);

            for(int i = 0; i < CountQuestions; i++)
            {
                while (true)
                {
                    var item = questions[random.Next(0, questions.Count)];

                    if (!Questions.Contains(item))
                    {
                        Questions.Add(item);

                        break;
                    }
                }
            }
        }

        public void FormCounters(int questionsCount)
        {
            CountQuestions = Convert.ToInt32(Test.maximumNumberOfQuestions) < questionsCount ? Convert.ToInt32(Test.maximumNumberOfQuestions) : questionsCount;
            NumberActiveQuestion = 1;
        }

        public void QuestionChanged()
        {
            var userId = Convert.ToInt32(_userService.UserInformation.id);
            var studentId = Convert.ToInt32(HttpResponseMessageConverter.GetResult<dynamic>(_studentsClient.StudentByUserId(userId)).id);
            var studentAnswer = HttpResponseMessageConverter.GetResult<dynamic>(_studentAnswersClient.GetStudentAnswerByQuestionId(studentId, Convert.ToInt32(ActiveQuestion.id)));

            if (SelectedAnswer != null)
            {
                if (studentAnswer == null)
                {
                    _studentAnswersClient.AddStudentAnswer(studentId, Convert.ToInt32(SelectedAnswer));
                }
                else
                {
                    _studentAnswersClient.RemoveStudentAnswer(studentId, Convert.ToInt32(studentAnswer.answerId));
                    _studentAnswersClient.AddStudentAnswer(studentId, Convert.ToInt32(SelectedAnswer));
                }
            }

            ActiveQuestion = Questions[NumberActiveQuestion - 1];
            Answers = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_answersClient.GetAnswersByQuestionId(Convert.ToInt32(ActiveQuestion.id))));

            studentAnswer = HttpResponseMessageConverter.GetResult<dynamic>(_studentAnswersClient.GetStudentAnswerByQuestionId(studentId, Convert.ToInt32(ActiveQuestion.id)));

            SelectedAnswer = null;
            SelectedAnswer = (studentAnswer != null ? ((object)(studentAnswer.answer.id)) : null);
        }

        public async Task Tick()
        {
            while (TimeLeft.Value.Ticks > 0)
            {
                await Task.Delay(1000);

                TimeLeft = TimeLeft.Value.Subtract(new TimeSpan(0, 0, 0, 1));
            }

            QuestionChanged();

            var countCorrectedAnswers = GetCountCorrectedAnswers();

            MessageBox.Show($"Время вышло. Ваш результат {countCorrectedAnswers} / {Questions.Count} ({((double)((double)countCorrectedAnswers / (double)Questions.Count) * 100).ToString("F2")} %)");

            _pageService.ChangePage(new Tests());
        }

        public ICommand PreviousQuestion => new DelegateCommand(() =>
        {
            NumberActiveQuestion -= 1;

            QuestionChanged();
        }, () => { return NumberActiveQuestion - 1 >= 1; });

        public ICommand NextQuestion => new DelegateCommand(() =>
        {
            NumberActiveQuestion += 1;

            QuestionChanged();
        }, () => { return NumberActiveQuestion + 1 <= CountQuestions; });

        public ICommand Back => new DelegateCommand(() =>
        {
            if (MessageBox.Show("Вы действительно хотите завершить тестирование?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                QuestionChanged();

                var countCorrectedAnswers = GetCountCorrectedAnswers();

                MessageBox.Show($"Тестирование завершено. Ваш результат {countCorrectedAnswers} / {Questions.Count} ({((double)((double)countCorrectedAnswers / (double)Questions.Count) * 100).ToString("F2")} %)");

                _pageService.ChangePage(new Tests());
            }    
        });
    }
}
