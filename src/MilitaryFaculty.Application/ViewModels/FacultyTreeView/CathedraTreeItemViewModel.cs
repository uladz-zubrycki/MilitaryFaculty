using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.TreeView;

namespace MilitaryFaculty.Application.ViewModels
{
    public class CathedraTreeItemViewModel : TreeItemViewModel<Domain.Cathedra>
    {
        public CathedraTreeItemViewModel(Domain.Cathedra cathedra,
                                         TreeViewModel owner,
                                         ITreeItemViewModel parent,
                                         IRepository<Domain.Professor> professorRepository)
            : base(cathedra, owner, parent, true)
        {
            if (professorRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            professorRepository.EntityCreated += OnProfessorCreated;
            professorRepository.EntityDeleted += OnProfessorDeleted;

            InitCommands();
        }

        public override string Title
        {
            get { return Model.Name; }
        }

        protected FacultyTreeViewModel FacultyTree
        {
            get { return Owner as FacultyTreeViewModel; }
        }

        protected override IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            var result = Model.Professors
                              .Select(ProfessorTreeItemViewModel.FromModel(Owner, this));

            return result;
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateAddProffessorCommand()
                              });
        }

        private ImagedCommandViewModel CreateAddProffessorCommand()
        {
            const string tooltip = "Добавить преподавателя";
            const string imageSource = @"..\Content\add-user.png";

            return new ImagedCommandViewModel(Browse.ProfessorAdd,
                                              Model,
                                              tooltip,
                                              imageSource);
        }

        private void OnProfessorCreated(object sender, ModifiedEntityEventArgs<Domain.Professor> e)
        {
            var professor = e.ModifiedEntity;

            if (professor.Cathedra.Equals(Model))
            {
                Children.Add(new ProfessorTreeItemViewModel(professor, Owner, this));
            }
        }

        private void OnProfessorDeleted(object sender, ModifiedEntityEventArgs<Domain.Professor> e)
        {
            var professor = e.ModifiedEntity;

            if (professor.Cathedra.Equals(Model))
            {
                Children.RemoveSingle(c => c.Model.Equals(professor));
            }
        }

        public static CathedraTreeItemViewModel FromModel(Domain.Cathedra model,
                                                          TreeViewModel owner,
                                                          IRepository<Domain.Professor> professorRepository)
        {
            return new CathedraTreeItemViewModel(cathedra: model,
                                                 owner: owner,
                                                 parent: null,
                                                 professorRepository: professorRepository);
        }

        public static Func<Domain.Cathedra, CathedraTreeItemViewModel> FromModel(TreeViewModel owner,
                                                                          IRepository<Domain.Professor> professorRepository)
        {
            return cathedra => FromModel(cathedra, owner, professorRepository);
        }
    }
}