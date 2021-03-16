using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private PageService _pageService;

        public MainViewModel(PageService pageService)
        {
            _pageService = pageService;
        }

        public ICommand Authorization => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Authorization());
        });

        public ICommand Registration => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Registration());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
