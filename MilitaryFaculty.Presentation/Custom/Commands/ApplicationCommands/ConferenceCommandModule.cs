using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ConferenceCommandModule : BaseCommandModule<Conference>
    {
        protected override RoutedCommand AddCommand
        {
            get { return Do.Conference.Add; }
        }

        protected override RoutedCommand UpdateCommand
        {
            get { return Do.Conference.Update; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.Conference.Remove; }
        }

        public ConferenceCommandModule(IRepository<Conference> repository) 
            : base(repository)
        {
             // Empty
        }

        protected override string GetRemovalMessage()
        {
            return "Вы действительно хотите удалить конференцию? Все данные будут утеряны.";
        }
    }
}