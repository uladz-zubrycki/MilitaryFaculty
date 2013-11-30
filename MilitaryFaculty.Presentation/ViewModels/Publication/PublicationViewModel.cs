using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationViewModel : ComplexViewModel<Publication>
    {
        public override string Title
        {
            get
            {
                return "Информация о публикации";
            }
        }

        public PublicationViewModel(Publication model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            ViewModels.AddRange(new ViewModel<Publication>[]
                                {
                                    new PublicationInfoViewModel(Model, mode),
                                    new PublicationExtraInfoViewModel(Model, mode)
                                });
        }
    }
}