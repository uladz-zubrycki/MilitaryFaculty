using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class PublicationInfoViewModel : ViewModel<Publication>
    {
        #region Class Properties

        public override string Title
        {
            get
            {
                return "Основная информация";
            }
        }

        public string Name
        {
            get { return Model.Name; }
            set
            {
                SetModelProperty(m => m.Name, value);
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
               SetModelProperty(m => m.PagesCount, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationInfoViewModel(Publication model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
          
            var editCommands = new EditableViewBehaviour<Publication>(Do.Publication.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}