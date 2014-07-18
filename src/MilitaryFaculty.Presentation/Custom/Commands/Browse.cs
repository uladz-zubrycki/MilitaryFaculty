using System.Windows.Input;

namespace MilitaryFaculty.Presentation.Custom
{
    public static class Browse
    {
        public static readonly RoutedCommand Forward = new RoutedCommand();
        public static readonly RoutedCommand Back = new RoutedCommand();

        public static readonly RoutedCommand ProfessorAdd = new RoutedCommand();

        public static readonly RoutedCommand BookAdd = new RoutedCommand();
        public static readonly RoutedCommand BookDetails = new RoutedCommand();

        public static readonly RoutedCommand ConferenceAdd = new RoutedCommand();
        public static readonly RoutedCommand ConferenceDetails = new RoutedCommand();

        public static readonly RoutedCommand ExhibitionAdd = new RoutedCommand();
        public static readonly RoutedCommand ExhibitionDetails = new RoutedCommand();

        public static readonly RoutedCommand PublicationAdd = new RoutedCommand();
        public static readonly RoutedCommand PublicationDetails = new RoutedCommand();
    }
}