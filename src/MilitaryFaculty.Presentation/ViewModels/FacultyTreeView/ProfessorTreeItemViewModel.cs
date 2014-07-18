using System;
using System.Collections.Generic;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.ViewModels;
using MilitaryFaculty.Presentation.Core.Widgets.TreeView;
using MilitaryFaculty.Presentation.Custom;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class ProfessorTreeItemViewModel : TreeItemViewModel<Professor>
    {
        public ProfessorTreeItemViewModel(Professor professor,
                                          TreeViewModel owner,
                                          ITreeItemViewModel parent)
            : base(professor, owner, parent, false)
        {
            Commands.Add(CreateRemoveProfessorCommand());
        }

        public override string Title
        {
            get { return Model.FullName.ToString(); }
        }

        private ImagedCommandViewModel CreateRemoveProfessorCommand()
        {
            const string tooltip = "Удалить преподавателя";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(Do.ProfessorRemove,
                                              Model, tooltip, imageSource);
        }

        protected override IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            throw new NotSupportedException();
        }

        public static ProfessorTreeItemViewModel FromModel(Professor model,
                                                           TreeViewModel owner,
                                                           ITreeItemViewModel parent)
        {
            return new ProfessorTreeItemViewModel(model, owner, parent);
        }

        public static Func<Professor, ProfessorTreeItemViewModel> FromModel(TreeViewModel owner,
                                                                            ITreeItemViewModel parent)
        {
            return professor => FromModel(professor, owner, parent);
        }
    }
}