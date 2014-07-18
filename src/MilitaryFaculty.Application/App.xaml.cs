using System.Threading;
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
            ShowWindow();
        }

        private static void ShowWindow()
        {
            var container = InjectionConfig.Register(new ContainerBuilder());

            var view = new MainWindow
                       {
                           DataContext = new MainViewModel(container),
                       };

            view.Show();
        }

        private static void WaitForSplashScreen()
        {
            Thread.SpinWait(10000);
        }
    }
}