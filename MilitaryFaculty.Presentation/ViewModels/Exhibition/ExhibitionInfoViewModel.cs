using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ExhibitionInfoViewModel : ViewModel<Exhibition>
    {
        #region Class Properties

        public string Name { get; set; }
        public DateTime Date { get; set; }

        public string AwardTypeString
        {
            get { return Model.AwardType.GetName(); }
        }

        public IEnumerable<Tuple<AwardType, string>> AwardTypeList
        {
            get
            {
                return Enum.GetValues(typeof(AwardType))
                           .Cast<AwardType>()
                           .Select(val => new Tuple<AwardType, string>(val, val.GetName()));
            }
        }

        public AwardType AwardType
        {
            get { return Model.AwardType; }
            set
            {
               SetModelProperty(m => m.AwardType, value);
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ExhibitionInfoViewModel(Exhibition model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            Title = "Базовая информация"; ;

            var editCommands = new EditableViewBehaviour<Exhibition>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}