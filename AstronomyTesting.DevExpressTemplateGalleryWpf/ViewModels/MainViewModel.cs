using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        protected MainViewModel()
        {

        }

        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel());
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}