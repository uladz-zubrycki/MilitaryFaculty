using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Core.Widgets.TreeView;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorTreeItemViewModel : TreeItemViewModel<Professor>
    {
        public override string Title
        {
            get { return Model.FullName.ToString(); }
        }

        public ProfessorTreeItemViewModel(Professor professor, TreeViewModel owner,
                                          ITreeItemViewModel parent)
            : base(professor, owner, parent, false)
        {
            Commands.Add(CreateRemoveProfessorCommand());
        }

        public static Func<Professor, ProfessorTreeItemViewModel> FromModel(TreeViewModel owner,
                                                                            ITreeItemViewModel parent)
        {
            return professor => new ProfessorTreeItemViewModel(professor, owner, parent);
        }

        private ImagedCommandViewModel CreateRemoveProfessorCommand()
        {
            const string tooltip = "Удалить преподавателя";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.Professor.Remove,
                Model, tooltip, imageSource);
        }

        protected override IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            throw new NotSupportedException();
        }
    }
}