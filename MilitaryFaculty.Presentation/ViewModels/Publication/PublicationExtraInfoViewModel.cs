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
    public class PublicationExtraInfoViewModel : ViewModel<Publication>
    {
        #region Class Properties

        public string PublicationTypeString
        {
            get { return Model.PublicationType.GetName(); }
        }

        public IEnumerable<Tuple<PublicationType, string>> PublicationTypeList
        {
            get
            {
                return Enum.GetValues(typeof(PublicationType))
                           .Cast<PublicationType>()
                           .Select(val => new Tuple<PublicationType, string>(val, val.GetName()));
            }
        }

        public PublicationType PublicationType
        {
            get { return Model.PublicationType; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }
                
                if (value == PublicationType)
                {
                    return;
                }

                Model.PublicationType = value;
                OnPropertyChanged();
                OnPropertyChanged("PublicationTypeString");
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public PublicationExtraInfoViewModel(Publication model)
            : this(model, EditableViewMode.Display)
        {
            // Empty
        }

        public PublicationExtraInfoViewModel(Publication model, EditableViewMode mode)
            : base(model)
        {
            const string title = "Дополнительная информация";

            Title = title;

            var editCommands = new EditableViewBehaviour<Publication>(ApplicationCommands.UpdatePublication, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}