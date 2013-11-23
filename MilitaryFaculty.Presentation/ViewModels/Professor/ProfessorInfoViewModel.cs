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
    public class ProfessorInfoViewModel : ViewModel<Professor>
    {
        #region Class Properties

        public string FullName
        {
            get { return Model.FullName.ToString(); }
        }

        public string MilitaryRankString
        {
            get { return Model.MilitaryRank.GetName(); }
        }

        public IEnumerable<Tuple<MilitaryRank, string>> MilitaryRankList
        {
            get
            {
                return Enum.GetValues(typeof (MilitaryRank))
                           .Cast<MilitaryRank>()
                           .Select(val => new Tuple<MilitaryRank, string>(val, val.GetName()));
            }
        }

        public string FirstName
        {
            get { return Model.FullName.FirstName; }
            set
            {
                if (value == FirstName)
                {
                    return;
                }


                Model.FullName.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string MiddleName
        {
            get { return Model.FullName.MiddleName; }
            set
            {
                if (value == MiddleName)
                {
                    return;
                }

                Model.FullName.MiddleName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return Model.FullName.LastName; }
            set
            {
                if (value == LastName)
                {
                    return;
                }

                Model.FullName.LastName = value;
                OnPropertyChanged();
            }
        }

        public MilitaryRank MilitaryRank
        {
            get { return Model.MilitaryRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == MilitaryRank)
                {
                    return;
                }

                Model.MilitaryRank = value;
                OnPropertyChanged();
                OnPropertyChanged("MilitaryRankString");
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorInfoViewModel(Professor model)
            : this(model, EditableViewMode.Display)
        {
            // Empty
        }

        public ProfessorInfoViewModel(Professor model, EditableViewMode mode)
            : base(model)
        {
            Title = "Базовая информация";

            var editCommands = new EditableViewBehaviour<Professor>(Do.Professor.Update, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}