using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Application.Custom
{
    public class PublicationCommandModule : EntityCommandModule<Publication>
    {
        public PublicationCommandModule(IRepository<Publication> repository)
            : base(repository)
        {
            // Empty
        }

        protected override RoutedCommand AddCommand
        {
            get { return Do.PublicationAdd; }
        }

        protected override RoutedCommand SaveCommand
        {
            get { return Do.PublicationSave; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.PublicationRemove; }
        }

        protected override string GetRemovalMessage()
        {
            return ("Вы действительно хотите удалить публикацию? " +
                    "Все данные будут утеряны.");
        }
    }
}