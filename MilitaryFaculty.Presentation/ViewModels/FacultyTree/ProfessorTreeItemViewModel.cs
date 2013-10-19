using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorTreeItemViewModel : TreeItemViewModel<Professor>
    {
        #region Type Static Members

        public static Func<Professor, ProfessorTreeItemViewModel> FromModel(TreeViewModel owner, ITreeItemViewModel parent)
        {
            return professor => new ProfessorTreeItemViewModel(professor, owner, parent);
        }  

        #endregion // Type Static Members

        #region Class Constructors

        public ProfessorTreeItemViewModel(Professor professor, TreeViewModel owner,
                                          ITreeItemViewModel parent)
            : base(professor, owner, parent, false)
        {
            Title = professor.FullName.ToString();

            Commands.Add(CreateRemoveProfessorCommand());
        }

        private ImagedCommandViewModel CreateRemoveProfessorCommand()
        {
            const string tooltip = "Удалить преподавателя";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(GlobalAppCommands.RemoveProfessor,
                                              Model, tooltip, imageSource);
        }

        #endregion // Constructors

        #region Class Protected Methods

        protected override IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            throw new NotSupportedException();
        }

        #endregion //Class Private Methods
    }
}