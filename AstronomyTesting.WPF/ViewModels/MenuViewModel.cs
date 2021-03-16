using AstronomyTesting.WPF.Services;
using AstronomyTesting.WPF.Views.Pages;
using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace AstronomyTesting.WPF.ViewModels
{
    public class MenuViewModel : BindableBase
    {
        private PageService _pageService;
        private UserService _userService;


        public MenuViewModel(PageService pageService, UserService userService)
        {
            _pageService = pageService;
            _userService = userService;
        }

        public ICommand Tests => new DelegateCommand(() =>
        {
            _pageService.ChangePage(new Tests());
        });

        public ICommand Exit => new DelegateCommand(() =>
        {
            Application.Current.Shutdown();
        });
    }
}
