using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionRootViewModel : EntityRootViewModel<Exhibition>
    {
        public override string Title
        {
            get { return "Информация о выставке"; }
        }

        public ExhibitionRootViewModel(Exhibition model)
            : base(model)
        {
            //HeaderViewModel = new ExhibitionHeaderViewModel();
        }
     
        protected override IEnumerable<ViewModel<Exhibition>> GetViewModels()
        {
            return new[]
                   {
                       new ExhibitionInfoViewModel(Model)
                   };
        }
    }
}