using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class PublicationCommandModule : BaseCommandModule<Publication>
    {
        protected override RoutedCommand AddCommand
        {
            get { return Do.Publication.Add; }
        }

        protected override RoutedCommand UpdateCommand
        {
            get { return Do.Publication.Update; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.Publication.Remove; }
        }

        public PublicationCommandModule(IRepository<Publication> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage()
        {
            return "Вы действительно хотите удалить публикацию? Все данные будут утеряны.";
        }
    }
}