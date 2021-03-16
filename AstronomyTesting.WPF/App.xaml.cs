using AstronomyTesting.Response.Service;
using AstronomyTesting.WPF.Properties;
using AstronomyTesting.WPF.ViewModels.Locators;
using System.Windows;

namespace AstronomyTesting.WPF
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigRestClients.Protocol = Settings.Default.Protocol;
            ConfigRestClients.Domain = Settings.Default.Domain;
            ConfigRestClients.Port = Settings.Default.Port;

            HttpStatusCodeMessageService.OnShowHttpStatusCodeMessage += (se) => { MessageBox.Show(se.ErrorMessage, "Ошибка"); };

            ViewModelLocator.Init();

            base.OnStartup(e);
        }
    }
}
