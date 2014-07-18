using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class ListItemViewModel<T> : ViewModel<T>
        where T : class
    {
        public abstract string PrimaryInfo { get; }
        public abstract string SecondaryInfo { get; }
        public ViewModel TooltipViewModel { get; protected set; }

        protected ListItemViewModel(T model) :
            base(model)
        {
            //Empty
        }
    }
}