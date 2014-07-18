using System;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ConferenceInfoViewModel : EntityViewModel<Conference>
    {
        public ConferenceInfoViewModel(Conference model)
            : base(model)
        {
            this.Editable(Do.ConferenceSave);
        }

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
    }
}