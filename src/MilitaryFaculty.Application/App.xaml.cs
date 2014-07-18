using System.Windows;
using Autofac;
using MilitaryFaculty.Application.ViewModels;

namespace MilitaryFaculty.Application
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            WaitForSplashScreen();
            StartApplication();
        }

        private static void WaitForSplashScreen()
        {
#if !DEBUG
            Thread.SpinWait(500);
#endif
        }

        private static void StartApplication()
        {
            var container = InjectionConfig.Register(new ContainerBuilder());
            var view = new MainWindow { DataContext = new MainViewModel(container) };
            
            view.Show();
        }
    }
}