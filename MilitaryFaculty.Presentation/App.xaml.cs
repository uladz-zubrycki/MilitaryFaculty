using System.Windows;
using Autofac;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<EntityContext>());

            var container = InjectionConfig.Register(new ContainerBuilder());

            var view = new MainWindow
                       {
                           DataContext = new MainViewModel(container),
                       };

            view.Show();
        }
    }
}