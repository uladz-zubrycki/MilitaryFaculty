using System;
using System.Collections.Generic;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.TreeView;

namespace MilitaryFaculty.Application.ViewModels
{
    public class ProfessorTreeItemViewModel : TreeItemViewModel<Domain.Professor>
    {
        public ProfessorTreeItemViewModel(Domain.Professor professor,
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

        public static ProfessorTreeItemViewModel FromModel(Domain.Professor model,
                                                           TreeViewModel owner,
                                                           ITreeItemViewModel parent)
        {
            return new ProfessorTreeItemViewModel(model, owner, parent);
        }

        public static Func<Domain.Professor, ProfessorTreeItemViewModel> FromModel(TreeViewModel owner,
                                                                            ITreeItemViewModel parent)
        {
            return professor => FromModel(professor, owner, parent);
        }
    }
}