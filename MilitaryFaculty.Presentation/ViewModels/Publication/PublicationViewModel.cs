using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationRootViewModel : EntityRootViewModel<Publication>
    {
        public override string Title
        {
            get { return "Информация о публикации"; }
        }

        public PublicationRootViewModel(Publication model)
            : base(model)
        {
            // Empty
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