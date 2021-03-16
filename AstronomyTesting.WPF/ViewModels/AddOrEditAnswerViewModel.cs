using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class AddOrEditAnswerViewModel : BindableBase
    {
        private PageService _pageService;

        private AnswersRestClient _answersClient;

        public static bool IsAdding { get; set; } = true;

        public static int? TestId { get; set; } = null;

        public static int? QuestionId { get; set; } = null;

        public static int? AnswerId { get; set; } = null;

        
        public bool? IsСorrect { get; set; } = null;

        public string Name { get; set; } = "";

        public AddOrEditAnswerViewModel(PageService pageService, AnswersRestClient answersClient)
        {
            _pageService = pageService;

            _answersClient = answersClient;

            if (!IsAdding)
            {
                if (AnswerId != null)
                {
                    var answer = HttpResponseMessageConverter.GetResult<dynamic>(_answersClient.GetAnswerById((int)AnswerId));

                    
                    IsСorrect = (bool?)answer.isCorrect;
                    Name = answer.name;
                }
            }
        }

        public ICommand Save => new DelegateCommand(() =>
        {
            if (IsAdding)
            {
                if (HttpResponseMessageConverter.GetResult<bool>(_answersClient.AddAnswer((int)QuestionId, Name, (bool)IsСorrect)))
                {
                    AnswersViewModel.TestId = null;
                    AnswersViewModel.TestId = TestId;

                    AnswersViewModel.QuestionId = null;
                    AnswersViewModel.QuestionId = QuestionId;

                    _pageService.ChangePage(new Answers());
                }
                else
                {
                    MessageBox.Show("Ответ с такими данными уже существует.", "Ошибка");
                }
            }
            else
            {
                if (HttpResponseMessageConverter.GetResult<bool>(_answersClient.UpdateAnswerById((int)AnswerId, (int)QuestionId, Name, (bool)IsСorrect)))
                {
                    AnswersViewModel.TestId = null;
                    AnswersViewModel.TestId = TestId;

                    AnswersViewModel.QuestionId = null;
                    AnswersViewModel.QuestionId = QuestionId;

                    _pageService.ChangePage(new Answers());
                }
                else
                {
                    MessageBox.Show("Ответ с такими данными уже существует.", "Ошибка");
                }
            }

        }, () => { return IsСorrect != null && Name.Length > 0; });

        public ICommand Back => new DelegateCommand(() =>
        {
            AnswersViewModel.TestId = null;
            AnswersViewModel.TestId = TestId;

            AnswersViewModel.QuestionId = null;
            AnswersViewModel.QuestionId = QuestionId;

            _pageService.ChangePage(new Answers());
        });
    }
}
