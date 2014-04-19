using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
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