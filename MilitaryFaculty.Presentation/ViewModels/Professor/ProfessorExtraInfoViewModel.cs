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
    public class ProfessorExtraInfoViewModel : ViewModel<Professor>
    {
        #region Class Properties

        public string JobPositionString
        {
            get { return Model.JobPosition.GetName(); }
        }

        public IEnumerable<Tuple<JobPosition, string>> JobPositionList
        {
            get
            {
                return Enum.GetValues(typeof (JobPosition))
                           .Cast<JobPosition>()
                           .Select(val => new Tuple<JobPosition, string>(val, val.GetName()));
            }
        }

        public JobPosition JobPosition
        {
            get { return Model.JobPosition; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == JobPosition)
                {
                    return;
                }

                Model.JobPosition = value;
                OnPropertyChanged();
                OnPropertyChanged("JobPositionString");
            }
        }

        public string AcademicDegreeString
        {
            get { return Model.AcademicDegree.GetName(); }
        }

        public IEnumerable<Tuple<AcademicDegree, string>> AcademicDegreeList
        {
            get
            {
                return Enum.GetValues(typeof (AcademicDegree))
                           .Cast<AcademicDegree>()
                           .Select(val => new Tuple<AcademicDegree, string>(val, val.GetName()));
            }
        }

        public AcademicDegree AcademicDegree
        {
            get { return Model.AcademicDegree; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == AcademicDegree)
                {
                    return;
                }

                Model.AcademicDegree = value;
                OnPropertyChanged();
                OnPropertyChanged("AcademicDegreeString");
            }
        }

        public string AcademicRankString
        {
            get { return Model.AcademicRank.GetName(); }
        }

        public IEnumerable<Tuple<AcademicRank, string>> AcademicRankList
        {
            get
            {
                return Enum.GetValues(typeof (AcademicRank))
                           .Cast<AcademicRank>()
                           .Select(val => new Tuple<AcademicRank, string>(val, val.GetName()));
            }
        }

        public AcademicRank AcademicRank
        {
            get { return Model.AcademicRank; }
            set
            {
                if (!value.IsDefined())
                {
                    throw new InvalidEnumArgumentException();
                }

                if (value == AcademicRank)
                {
                    return;
                }

                Model.AcademicRank = value;
                OnPropertyChanged();
                OnPropertyChanged("AcademicRankString");
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        public ProfessorExtraInfoViewModel(Professor model, EditableViewMode mode = EditableViewMode.Display)
            : base(model)
        {
            const string title = "Дополнительная информация";
            
            Title = title;

            var editCommands = new EditableViewBehaviour<Professor>(ApplicationCommands.UpdateProfessor, Model);
            editCommands.Inject(this, mode);
        }

        #endregion // Class Constructors
    }
}