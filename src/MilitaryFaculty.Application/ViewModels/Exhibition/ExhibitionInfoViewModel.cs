using System;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
using MilitaryFaculty.Presentation.ViewBehaviours;
using MilitaryFaculty.Presentation.ViewModels.Entity;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ExhibitionInfoViewModel : EntityViewModel<Exhibition>
    {
        public ExhibitionInfoViewModel(Exhibition model)
            : base(model)
        {
            this.Editable(Do.ExhibitionSave);
        }

        public override string Title
        {
            get { return "Базовая информация"; }
        }

        [TextProperty(Label = "Название:")]
        public string Name
        {
            get { return Model.Name; }
            set { SetModelProperty(m => m.Name, value); }
        }

        [DateProperty(Label = "Дата проведения:")]
        public DateTime Date
        {
            get { return Model.Date; }
            set { SetModelProperty(m => m.Date, value); }
        }

        [EnumProperty(Label = "Уровень мероприятия:")]
        public EventLevel EventLevel
        {
            get { return Model.EventLevel; }
            set { SetModelProperty(m => m.EventLevel, value); }
        }

        [EnumProperty(Label = "Награда:")]
        public AwardType AwardType
        {
            get { return Model.AwardType; }
            set { SetModelProperty(m => m.AwardType, value); }
        }
    }
}