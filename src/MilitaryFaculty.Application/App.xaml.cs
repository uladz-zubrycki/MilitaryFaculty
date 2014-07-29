using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using MilitaryFaculty.Application.AppStartup;
using MilitaryFaculty.Common;
using MilitaryFaculty.Domain.Base;

namespace MilitaryFaculty.Application
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            InitializeGlobalCommands();
            
            var container = InjectionConfig.Register(new ContainerBuilder());
         
            using (var scope = container.BeginLifetimeScope())
            {
                var view = new MainWindow { DataContext = scope.Resolve<MainViewModel>() };
                view.Show();    
            }
        }

        private static void InitializeGlobalCommands()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(asm => asm.GetTypes());
            var entityTypes = types.Where(type => type.IsSubclassOf(typeof(UniqueEntity)));
            entityTypes.ForEach(GlobalCommands.CreateCommandsForEntity);
        }

        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            OnException(e.Exception);
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception)e.ExceptionObject;
            OnException(exception);
        }

        private static void OnException(Exception exception)
        {
            const string title = "Ошибка";

            MessageBox.Show(exception.Message + exception.StackTrace,
                            title,
                            MessageBoxButton.OKCancel);
        }
    }
}