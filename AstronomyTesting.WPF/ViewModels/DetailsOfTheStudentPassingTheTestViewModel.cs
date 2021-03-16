using AstronomyTesting.Response.Converters;
using AstronomyTesting.Response.RestClients;
using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class DetailsOfTheStudentPassingTheTestViewModel : BindableBase
    {
        private PageService _pageService;

        private DetailsOfTheTestRestClient _detailsOfTheClient;

        public static int? TestId { get; set; } = null;

        public static int? StudentId { get; set; } = null;


        public ObservableCollection<dynamic> DetailsOfTheStudentPassingTheTest { get; set; } = new ObservableCollection<dynamic>();

        public DetailsOfTheStudentPassingTheTestViewModel(PageService pageService, DetailsOfTheTestRestClient detailsOfTheClient)
        {
            _pageService = pageService;

            _detailsOfTheClient = detailsOfTheClient;

            if (StudentId != null)
            {
                DetailsOfTheStudentPassingTheTest.Clear();
                DetailsOfTheStudentPassingTheTest = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_detailsOfTheClient.GetDetailsOfTheStudentPassingTheTest(Convert.ToInt32(TestId), Convert.ToInt32(StudentId))));
            }
        }

        public ICommand Back => new DelegateCommand(() =>
        {
            DetailsOfTheTestViewModel.TestId = null;
            DetailsOfTheTestViewModel.TestId = TestId;

            _pageService.ChangePage(new DetailsOfTheTest());
        });
    }
}
