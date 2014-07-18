using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Application.Custom
{
    public class ExhibitionCommandModule : EntityCommandModule<Exhibition>
    {
        public ExhibitionCommandModule(IRepository<Exhibition> repository)
            : base(repository)
        {
            // Empty
        }

        protected override RoutedCommand AddCommand
        {
            get { return Do.ExhibitionAdd; }
        }

        protected override RoutedCommand SaveCommand
        {
            get { return Do.ExhibitionSave; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.ExhibitionRemove; }
        }

        protected override string GetRemovalMessage()
        {
            return ("Вы действительно хотите удалить информацию об участии в выставке научных работ? " +
                    "Все данные будут утеряны.");
        }
    }
}