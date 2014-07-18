using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Application.ViewModels
{
    internal class AddPublicationViewModel : AddEntityViewModel<Publication>
    {
        public AddPublicationViewModel(Publication model)
            : base(model)
        {
            // EMpty
        }

        public override string Title
        {
            get { return "Добавить публикацию"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.PublicationAdd; }
        }

        protected override IEnumerable<ViewModel<Publication>> GetViewModels()
        {
            return new ViewModel<Publication>[]
                   {
                       new PublicationInfoViewModel(Model),
                       new PublicationExtraInfoViewModel(Model)
                   };
        }
    }
}