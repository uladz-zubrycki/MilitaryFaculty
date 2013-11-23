using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xaml;

namespace MilitaryFaculty.Presentation.Custom.MarkupExtensions
{
    public class Command : MarkupExtension
    {
        private readonly string name;

        private string NamespacePrefix
        {
            get
            {
                return name.Split(':').First();
            }
        }

        private string TypeName
        {
            get
            {
                const string pattern = @"(?<=:)(\w+\.)*\w+(?=\.\w+$)";
                var path = Regex.Match(name, pattern).Value;

                return path.Replace('.', '+');
            }
        }

        private string CommandName
        {
            get
            {
                return name.Split('.').Last();
            }
        }

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
            var resolver = GetService<IXamlNamespaceResolver>(serviceProvider);
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

        private string GetAssemblyName(string xamlNamespace)
        {
            const string pattern = @"(?<=;assembly=)(\w+(?:\.\w+)*)$";
            var match = Regex.Match(xamlNamespace, pattern);
            
            return match.Value;
        }

        private string GetNamespace(string xamlNamespace)
        {
            const string pattern = @"(?<=clr-namespace:)(\w+(?:\.\w+)*)(?=;assembly)";
            var match = Regex.Match(xamlNamespace, pattern);

            return match.Value;
        }

        private T GetService<T>(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            return (T) serviceProvider.GetService(typeof (T));
        }
    }
}
