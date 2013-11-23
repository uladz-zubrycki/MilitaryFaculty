using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class Browse
    {
        public static readonly RoutedCommand Forward = new RoutedCommand();
        public static readonly RoutedCommand Back = new RoutedCommand();

        public static class Professor
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
        }

        public static class Conference
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Details = new RoutedCommand();
        }

        public static class Publication
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Details = new RoutedCommand();
        }

        public static class Exhibition
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Details = new RoutedCommand();
        }
    }
}