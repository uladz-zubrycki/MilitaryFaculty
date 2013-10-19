using System.Data.Entity;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using MilitaryFaculty.Data;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = InjectionConfig.Init(new ContainerBuilder());
            Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());

            var view = new MainWindow
                {
                    DataContext = new MainViewModel(container),
                };

            view.Show();
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }
    }
}
