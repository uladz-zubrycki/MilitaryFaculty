using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceInfoViewModel : EntityViewModel<Conference>
    {
        #region Class Properties

        public override string Title
        {
            get { return "Базовая информация"; }
        }

        [EnumProperty(Label = "Тип:")]
        public EventLevel ConferenceType
        {
            get { return Model.ConferenceType; }
            set { SetModelProperty(m => m.ConferenceType, value); }
        }

        [TextProperty(Label = "Название:")]
        public string Name
        {
            get { return Model.Name; }
            set { SetModelProperty(m => m.Name, value); }
        }

        [DateProperty(Label = "Дата проведения")]
        public DateTime Date
        {
            get { return Model.Date; }
            set { SetModelProperty(m => m.Date, value); }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ConferenceInfoViewModel(Conference model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this);
        }

        #endregion // Class Constructors
    }
}