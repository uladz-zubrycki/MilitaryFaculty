using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class ApplicationCommands
    {
        public static readonly RoutedCommand AddProfessor = new RoutedCommand();
        public static readonly RoutedCommand UpdateProfessor = new RoutedCommand();
        public static readonly RoutedCommand RemoveProfessor = new RoutedCommand();

        public static readonly RoutedCommand AddConference = new RoutedCommand();
        public static readonly RoutedCommand UpdateConference = new RoutedCommand();
        public static readonly RoutedCommand RemoveConference = new RoutedCommand();

        public static readonly RoutedCommand AddPublication = new RoutedCommand();
        public static readonly RoutedCommand UpdatePublication = new RoutedCommand();
        public static readonly RoutedCommand RemovePublication = new RoutedCommand();
    }
}
