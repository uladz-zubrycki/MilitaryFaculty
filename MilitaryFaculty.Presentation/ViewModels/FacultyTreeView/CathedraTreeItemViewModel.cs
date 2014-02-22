using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class CathedraTreeItemViewModel : TreeItemViewModel<Cathedra>
    {
        public override string Title
        {
            get { return Model.Name; }
        }

        protected FacultyTreeViewModel FacultyTree
        {
            get { return Owner as FacultyTreeViewModel; }
        }

        public CathedraTreeItemViewModel(Cathedra cathedra, TreeViewModel owner,
                                         ITreeItemViewModel parent,
                                         IRepository<Professor> professorRepository)
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

        public static Func<Cathedra, CathedraTreeItemViewModel> FromModel(TreeViewModel owner,
                                                                          IRepository<Professor> professorRepository)
        {
            return cathedra => new CathedraTreeItemViewModel(cathedra, owner, null, professorRepository);
        }

        protected override IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            var converter = ProfessorTreeItemViewModel.FromModel(Owner, this);

            return Model.Professors.Select(converter);
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

            return new ImagedCommandViewModel(Browse.Professor.Add,
                Model, tooltip, imageSource);
        }

        private void OnProfessorCreated(object sender, ModifiedEntityEventArgs<Professor> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var professor = e.ModifiedEntity;

            if (professor.Cathedra.Equals(Model))
            {
                Children.Add(new ProfessorTreeItemViewModel(professor, Owner, this));
            }
        }

        private void OnProfessorDeleted(object sender, ModifiedEntityEventArgs<Professor> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var professor = e.ModifiedEntity;

            if (professor.Cathedra.Equals(Model))
            {
                Children.RemoveSingle(c => c.Model.Equals(professor));
            }
        }
    }
}