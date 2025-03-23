using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }

}
