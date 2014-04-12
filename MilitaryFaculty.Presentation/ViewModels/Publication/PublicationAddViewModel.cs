using System.Collections.Generic;
using System.Windows.Input;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    internal class AddPublicationViewModel : AddEntityViewModel<Publication>
    {
        public override string Title
        {
            get { return "Добавить публикацию"; }
        }

        public override ICommand AddCommand
        {
            get { return Do.Publication.Add; }
        }

        public AddPublicationViewModel(Publication model)
            : base(model)
        {
            // EMpty
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