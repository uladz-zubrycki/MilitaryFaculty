using System.Windows;
using Autofac;
using MilitaryFaculty.Application.ViewModels;

namespace MilitaryFaculty.Application
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = InjectionConfig.Register(new ContainerBuilder());

            var view = new MainWindow
                       {
                           DataContext = new MainViewModel(container),
                       };

            view.Show();
        }
    }
}