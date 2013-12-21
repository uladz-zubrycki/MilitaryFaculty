using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ExhibitionCommandModule : BaseCommandModule<Exhibition>
    {
        protected override RoutedCommand AddCommand
        {
            get { return Do.Exhibition.Add; }
        }

        protected override RoutedCommand UpdateCommand
        {
            get { return Do.Exhibition.Update; }
        }

        protected override RoutedCommand RemoveCommand
        {
            get { return Do.Exhibition.Remove; }
        }

        public ExhibitionCommandModule(IRepository<Exhibition> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage()
        {
            return "Вы действительно хотите удалить информацию об участии в выставке научных работ? " +
                   "Все данные будут утеряны.";
        }
    }
}