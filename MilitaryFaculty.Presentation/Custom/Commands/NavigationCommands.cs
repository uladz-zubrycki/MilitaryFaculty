using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class NavigationCommands
    {
        public static readonly RoutedCommand BrowseForward = new RoutedCommand();
        public static readonly RoutedCommand BrowseBack = new RoutedCommand();
                                             
        public static readonly RoutedCommand BrowseProfessorAdd = new RoutedCommand();
                                             
        public static readonly RoutedCommand BrowseConferenceAdd = new RoutedCommand();
        public static readonly RoutedCommand BrowseConferenceDetails = new RoutedCommand();
                                             
        public static readonly RoutedCommand BrowsePublicationAdd = new RoutedCommand();
        public static readonly RoutedCommand BrowsePublicationDetails = new RoutedCommand();
    }
}