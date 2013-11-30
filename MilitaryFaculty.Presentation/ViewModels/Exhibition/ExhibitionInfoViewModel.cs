using System;
using System.Collections.Generic;
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

        public override string Title
        {
            get
            {
                return "Базовая информация";
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
            var editCommands = new EditableViewBehaviour<Exhibition>(Do.Conference.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}