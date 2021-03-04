using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        protected MainViewModel()
        {

        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public void Authorization()
        {
            NavigationService.Navigate("AuthorizationView");
        }

        public void Registration()
        {
            NavigationService.Navigate("RegistrationView");
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}