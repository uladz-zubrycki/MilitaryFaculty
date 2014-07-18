using System.Windows.Input;

namespace MilitaryFaculty.Application.Custom
{
    public static class Do
    {
        public static RoutedCommand ReportGenerate = new RoutedCommand();

        public static readonly RoutedCommand BookAdd = new RoutedCommand();
        public static readonly RoutedCommand BookSave = new RoutedCommand();
        public static readonly RoutedCommand BookRemove = new RoutedCommand();

        public static readonly RoutedCommand ConferenceAdd = new RoutedCommand();
        public static readonly RoutedCommand ConferenceSave = new RoutedCommand();
        public static readonly RoutedCommand ConferenceRemove = new RoutedCommand();

        public static readonly RoutedCommand ExhibitionAdd = new RoutedCommand();
        public static readonly RoutedCommand ExhibitionSave = new RoutedCommand();
        public static readonly RoutedCommand ExhibitionRemove = new RoutedCommand();

        public static readonly RoutedCommand ProfessorAdd = new RoutedCommand();
        public static readonly RoutedCommand ProfessorSave = new RoutedCommand();
        public static readonly RoutedCommand ProfessorRemove = new RoutedCommand();

        public static readonly RoutedCommand PublicationAdd = new RoutedCommand();
        public static readonly RoutedCommand PublicationSave = new RoutedCommand();
        public static readonly RoutedCommand PublicationRemove = new RoutedCommand();
    }
}