using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Application.Custom
{
    public class ConferenceCommandModule : EntityCommandModule<Conference>
    {
        public ConferenceCommandModule(IRepository<Conference> repository)
            : base(repository)
        {
            // Empty
        }

        protected override RoutedCommand AddCommand
        {
            get { return Do.ConferenceAdd; }
        }

        protected override RoutedCommand SaveCommand
        {
            get { return Do.ConferenceSave; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.ConferenceRemove; }
        }

        protected override string GetRemovalMessage()
        {
            return ("Вы действительно хотите удалить конференцию? " +
                    "Все данные будут утеряны.");
        }
    }
}