using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom.MarkupExtensions
{
    /// <summary>
    /// Provides <see cref="ICommand"/> object by its name.
    /// </summary>
    /// <remarks>
    /// Command name must be written in format prefix:CommandClass.(CommandClass.)*Command
    /// Where: 
    ///     prefix - XAML namespace prefix.
    ///     CommandClass - Static class containing public static property of type <see cref="ICommand"/>.
    ///     (Every next command class is considered to be nested class.)
    ///     Command - Command property name.
    /// </remarks>
    public class Command : MarkupExtension
    {
        private readonly string name;

        /// <summary>
        /// Retrieves namespace prefix from markup command name.
        /// </summary>
        private string NamespacePrefix
        {
            get
            {
                return name.Split(':').First();
            }
        }

        /// <summary>
        /// Retrieves type name from markup command name.
        /// </summary>
        private string TypeName
        {
            get
            {
                const string pattern = @"(?<=:)(\w+\.)*\w+(?=\.\w+$)";
                var path = Regex.Match(name, pattern).Value;

                return path.Replace('.', '+');
            }
        }

        /// <summary>
        /// Retrieves command property name from markup command name.
        /// </summary>
        private string CommandName
        {
            get
            {
                return name.Split('.').Last();
            }
        }

        /// <summary>
        /// Creates new Command class.
        /// </summary>
        /// <param name="name">Name of command to provide.</param>
        public Command(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            if (name.Split(':').Length > 2)
            {
                throw new ArgumentException();
            }

            this.name = name;
        }

        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension. 
        /// </summary>
        /// <returns>
        /// The object value to set on the property where the extension is applied. 
        /// </returns>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var resolver = serviceProvider.GetService<IXamlNamespaceResolver>();
            var xamlNamespace = resolver.GetNamespace(NamespacePrefix);
            var assemblyName = GetAssemblyName(xamlNamespace);
            var fullTypeName = GetNamespace(xamlNamespace) + '.' + TypeName;
            
            var type = Type.GetType(fullTypeName + ',' + assemblyName);
            var accessor = type.GetField(CommandName);
            var command = accessor.GetValue(null);

            if (!(command is ICommand))
            {
                throw new ArgumentException();
            }

            return command;
        }

        /// <summary>
        /// Retrieves assembly name from xaml namespace.
        /// </summary>
        /// <param name="xamlNamespace">Information about assembly and namespace got from xaml namespace prefix.</param>
        /// <returns>Assembly name.</returns>
        private static string GetAssemblyName(string xamlNamespace)
        {
            const string pattern = @"(?<=;assembly=)(\w+(?:\.\w+)*)$";
            var match = Regex.Match(xamlNamespace, pattern);
            
            return match.Value;
        }

        /// <summary>
        /// Retrieves namespace name from xaml namespace.
        /// </summary>
        /// <param name="xamlNamespace">Information about assembly and namespace got from xaml namespace prefix.</param>
        /// <returns>Namespace name.</returns>
        private static string GetNamespace(string xamlNamespace)
        {
            const string pattern = @"(?<=clr-namespace:)(\w+(?:\.\w+)*)(?=;assembly)";
            var match = Regex.Match(xamlNamespace, pattern);

            return match.Value;
        }
    }
}
