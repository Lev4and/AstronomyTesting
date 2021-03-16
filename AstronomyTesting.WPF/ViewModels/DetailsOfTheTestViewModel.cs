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
    public class DetailsOfTheTestViewModel : BindableBase
    {
        private PageService _pageService;

        private DetailsOfTheTestRestClient _detailsOfTheClient;

        public static int? TestId { get; set; } = null;


        public object SelectedStudentId { get; set; } = null;

        public ObservableCollection<dynamic> DetailsOfTheTest { get; set; } = new ObservableCollection<dynamic>();

        public DetailsOfTheTestViewModel(PageService pageService, DetailsOfTheTestRestClient detailsOfTheClient)
        {
            _pageService = pageService;

            _detailsOfTheClient = detailsOfTheClient;

            if(TestId != null)
            {
                DetailsOfTheTest.Clear();
                DetailsOfTheTest = new ObservableCollection<dynamic>(HttpResponseMessageConverter.GetResult<List<dynamic>>(_detailsOfTheClient.GetDetailsOfTheTest(Convert.ToInt32(TestId))));
            }
        }

        public ICommand Details => new DelegateCommand(() =>
        {
            DetailsOfTheStudentPassingTheTestViewModel.TestId = null;
            DetailsOfTheStudentPassingTheTestViewModel.TestId = TestId;

            DetailsOfTheStudentPassingTheTestViewModel.StudentId = null;
            DetailsOfTheStudentPassingTheTestViewModel.StudentId = Convert.ToInt32(SelectedStudentId);

            _pageService.ChangePage(new DetailsOfTheStudentPassingTheTest());
        }, () => { return SelectedStudentId != null; });

        public ICommand Back => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Tests());
        });
    }
}
