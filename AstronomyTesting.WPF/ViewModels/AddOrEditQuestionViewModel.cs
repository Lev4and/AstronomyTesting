using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class AddOrEditQuestionViewModel : BindableBase
    {
        private PageService _pageService;

        private QuestionsRestClient _questionsClient;


        public static bool IsAdding { get; set; } = true;

        public static int? TestId { get; set; } = null;

        public static int? QuestionId { get; set; } = null;


        public string Name { get; set; } = "";

        public byte[] Photo { get; set; }

        public AddOrEditQuestionViewModel(PageService pageService, QuestionsRestClient questionsClient)
        {
            _pageService = pageService;

            _questionsClient = questionsClient;

            if (!IsAdding)
            {
                if (QuestionId != null)
                {
                    var question = HttpResponseMessageConverter.GetResult<dynamic>(_questionsClient.GetQuestionById((int)QuestionId));

                    Name = question.name;
                    Photo = Convert.FromBase64String(Convert.ToString(question.photo));
                }
            }
        }

        public ICommand Save => new DelegateCommand(() =>
        {
            if (IsAdding)
            {
                if (HttpResponseMessageConverter.GetResult<bool>(_questionsClient.AddQuestion((int)TestId, Name, Photo)))
                {
                    QuestionsViewModel.TestId = null;
                    QuestionsViewModel.TestId = TestId;

                    _pageService.ChangePage(new Questions());
                }
                else
                {
                    MessageBox.Show("Вопрос с такими данными уже существует.", "Ошибка");
                }
            }
            else
            {
                if (HttpResponseMessageConverter.GetResult<bool>(_questionsClient.UpdateQuestionById((int)QuestionId, (int)TestId, Name, Photo)))
                {
                    QuestionsViewModel.TestId = null;
                    QuestionsViewModel.TestId = TestId;

                    _pageService.ChangePage(new Questions());
                }
                else
                {
                    MessageBox.Show("Вопрос с такими данными уже существует.", "Ошибка");
                }
            }

        }, () => { return Name.Length > 0; });

        public ICommand Back => new DelegateCommand(() =>
        {
            QuestionsViewModel.TestId = null;
            QuestionsViewModel.TestId = TestId;

            _pageService.ChangePage(new Questions());
        });
    }
}
