using System;
using System.Linq;
using System.Windows;
using Autofac;
using MilitaryFaculty.Application.ViewModels;
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
    }
}