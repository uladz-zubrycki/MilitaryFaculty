using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class Do
    {
        public static class Professor
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Update = new RoutedCommand();
            public static readonly RoutedCommand Remove = new RoutedCommand();
        }

        public static class Conference
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Update = new RoutedCommand();
            public static readonly RoutedCommand Remove = new RoutedCommand();
        }

        public static class Publication
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Update = new RoutedCommand();
            public static readonly RoutedCommand Remove = new RoutedCommand();
        }

        public static class Exhibition
        {
            public static readonly RoutedCommand Add = new RoutedCommand();
            public static readonly RoutedCommand Update = new RoutedCommand();
            public static readonly RoutedCommand Remove = new RoutedCommand();
        }
    }
}
