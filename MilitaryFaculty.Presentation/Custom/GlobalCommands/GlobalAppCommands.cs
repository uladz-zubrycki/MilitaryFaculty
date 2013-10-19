using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class GlobalAppCommands
    {
        public static readonly RoutedCommand AddProfessor = new RoutedCommand();
        public static readonly RoutedCommand UpdateProfessor = new RoutedCommand();
        public static readonly RoutedCommand RemoveProfessor = new RoutedCommand();

        public static readonly RoutedCommand AddConference = new RoutedCommand();
        public static readonly RoutedCommand UpdateConference = new RoutedCommand();
        public static readonly RoutedCommand RemoveConference = new RoutedCommand();

        public static readonly RoutedCommand AddBook = new RoutedCommand();
        public static readonly RoutedCommand UpdateBook = new RoutedCommand();
        public static readonly RoutedCommand RemoveBook = new RoutedCommand();
    }
}
