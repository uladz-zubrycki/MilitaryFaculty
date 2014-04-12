using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Attributes;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;
using MilitaryFaculty.Presentation.Core.ViewModels.Entity;
using MilitaryFaculty.Presentation.Custom;

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
            // Empty
        }

        protected override IEnumerable<IViewBehaviour> GetBehaviours()
        {
            yield return new EditableViewBehaviour<Conference>(Do.Conference.Update);
        }
    }
}