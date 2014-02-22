using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ConferenceInfoViewModel : EntityViewModel<Conference>
    {
        public override string Title
        {
            get { return "Базовая информация"; }
        }

        [EnumProperty(Label = "Уровень мероприятия:")]
        public EventLevel ConferenceType
        {
            get { return Model.EventLevel; }
            set { SetModelProperty(m => m.EventLevel, value); }
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

        public ConferenceInfoViewModel(Conference model)
            : base(model)
        {
            var editCommands = new EditableViewBehaviour<Conference>(Do.Conference.Update, Model);
            editCommands.Inject(this);
        }
    }
}