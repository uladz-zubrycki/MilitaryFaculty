using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationInfoViewModel : ViewModel<Publication>
    {
        #region Class Properties

        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (value == Name)
                {
                    return;
                }

                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public string PagesCountString
        {
            get { return String.Format("{0} стр.", PagesCount); }
        }

        public int PagesCount
        {
            get { return Model.PagesCount; }

            set
            {
                if (value == PagesCount)
                {
                    return;
                }

                Model.PagesCount = value;
                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationInfoViewModel(Publication model)
            : this(model, EditableViewMode.Display)
        {
            // Empty
        }

        public PublicationInfoViewModel(Publication model, EditableViewMode mode)
            : base(model)
        {
            Title = "Основная информация";

            var editCommands = new EditableViewBehaviour<Publication>(Do.Publication.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}