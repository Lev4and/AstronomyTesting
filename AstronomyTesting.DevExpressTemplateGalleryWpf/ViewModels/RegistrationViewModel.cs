using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public virtual string RepeatPassword { get; set; }

        public INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        protected RegistrationViewModel()
        {
            Login = "";
            Password = "";
            RepeatPassword = "";
        }

        public static RegistrationViewModel Create()
        {
            return ViewModelSource.Create(() => new RegistrationViewModel());
        }

        public bool CanRegistration()
        {
            if (Login != null && Password != null && RepeatPassword != null)
            {
                return Login.Length > 0 && Password.Length > 0 && RepeatPassword.Length > 0 && Password == RepeatPassword;
            }

            return false;
        }

        public void Registration()
        {

        }

        public void Back()
        {
            NavigationService.Navigate("MainView");
        }
    }
}
