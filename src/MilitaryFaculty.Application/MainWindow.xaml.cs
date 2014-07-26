using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.AppStartup
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeGlobalCommands();
        }

        private void InitializeGlobalCommands()
        {
            Func<RoutedCommand, CommandBinding> createBinding =
                cmd => new RoutedBinding {Command = cmd};

            var globalCommands = GlobalCommands.GetCommands();
            var bindings = globalCommands.Select(createBinding)
                                         .ToList();

            CommandBindings.AddRange(bindings);
        }
    }
}