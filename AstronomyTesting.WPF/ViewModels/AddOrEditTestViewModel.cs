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
    public class AddOrEditTestViewModel : BindableBase
    {
        private PageService _pageService;

        private TestsRestClient _testsClient;

        public static bool IsAdding { get; set; } = true;

        public static int? TestId { get; set; } = null;


        public int? Duration { get; set; } = 20;

        public int MaximumNumberOfQuestions { get; set; } = 15;

        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime StartTime { get; set; } = DateTime.Now;

        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddDays(1);

        public DateTime ExpirationTime { get; set; } = DateTime.Now.AddDays(1);

        public AddOrEditTestViewModel(PageService pageService, TestsRestClient testsClient)
        {
            _pageService = pageService;

            _testsClient = testsClient;

            if (!IsAdding)
            {
                if(TestId != null)
                {
                    var test = HttpResponseMessageConverter.GetResult<dynamic>(_testsClient.GetTestById((int)TestId));

                    Duration = test.duration;
                    MaximumNumberOfQuestions = test.maximumNumberOfQuestions;

                    Name = test.name;
                    Description = test.description;

                    StartDate = test.startDate;
                    StartTime = test.startDate;
                    ExpirationDate = test.expirationDate;
                    ExpirationTime = test.expirationDate;
                }
            }
        }

        public ICommand Save => new DelegateCommand(() =>
        {
            if (IsAdding)
            {
                if(HttpResponseMessageConverter.GetResult<bool>(_testsClient.AddTest(Name, Description, StartDate.Date.Add(StartTime.TimeOfDay), ExpirationDate.Date.Add(ExpirationTime.TimeOfDay), Duration, MaximumNumberOfQuestions)))
                {
                    _pageService.ChangePage(new Tests());
                }
                else
                {
                    MessageBox.Show("Тест с такими данными уже существует.", "Ошибка");
                }
            }
            else
            {
                if (HttpResponseMessageConverter.GetResult<bool>(_testsClient.UpdateTestById((int)TestId, Name, Description, StartDate.Date.Add(StartTime.TimeOfDay), ExpirationDate.Date.Add(ExpirationTime.TimeOfDay), Duration, MaximumNumberOfQuestions)))
                {
                    _pageService.ChangePage(new Tests());
                }
                else
                {
                    MessageBox.Show("Тест с такими данными уже существует.", "Ошибка");
                }
            }

        }, () => { return Name.Length > 0 && Duration >= 5 && MaximumNumberOfQuestions > 0 && StartDate != ExpirationDate && ExpirationDate > StartDate; });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Tests());
        });
    }
}
