using AstronomyTesting.DevExpressTemplateGalleryWpf.Properties;
using AstronomyTesting.Response.Service;
using System.Windows;

namespace AstronomyTesting.DevExpressTemplateGalleryWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigRestClients.Protocol = Settings.Default.Protocol;
            ConfigRestClients.Domain = Settings.Default.Domain;
            ConfigRestClients.Port = Settings.Default.Port;

            HttpStatusCodeMessageService.OnShowHttpStatusCodeMessage += (se) => { MessageBox.Show(se.ErrorMessage, "Ошибка"); };
        }
    }
}
