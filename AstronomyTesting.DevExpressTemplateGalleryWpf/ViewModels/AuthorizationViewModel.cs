using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf.ViewModels
{
    public class AuthorizationViewModel : ViewModelBase
    {
        public virtual string Login { get; set; }

        public virtual string Password { get; set; }

        public INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }

        protected AuthorizationViewModel()
        {
            Login = "";
            Password = "";
        }

        public static AuthorizationViewModel Create()
        {
            return ViewModelSource.Create(() => new AuthorizationViewModel());
        }

        public bool CanAuthorization()
        {
            if(Login != null && Password != null)
            {
                return Login.Length > 0 && Password.Length > 0;
            }

            return false;
        }

        public void Authorization()
        {

        }

        public void Back()
        {
            NavigationService.Navigate("MainView");
        }
    }
}
