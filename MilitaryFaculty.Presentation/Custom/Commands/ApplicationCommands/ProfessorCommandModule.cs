using System;
using System.Windows;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorCommandModule : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Professor> professorRepository;

        #endregion // Class Fields

        #region Class Constructors

        public ProfessorCommandModule(IRepository<Professor> professorRepository)
        {
            if (professorRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            this.professorRepository = professorRepository;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void RegisterModule(CommandContainer container)
        {
            container.RegisterCommand<Professor>(Do.Professor.Add,
                                                 OnAddProfessor,
                                                 CanAddProfessor);

            container.RegisterCommand<Professor>(Do.Professor.Update,
                                                 OnUpdateProfessor,
                                                 CanUpdateProfessor);

            container.RegisterCommand<Professor>(Do.Professor.Remove,
                                                 OnRemoveProfessor);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnAddProfessor(Professor professor)
        {
            if (professor == null)
            {
                throw new ArgumentNullException("professor");
            }

            professorRepository.Create(professor);
            Browse.Back.Execute(null, null);
        }

        private bool CanAddProfessor(Professor professor)
        {
            //TODO: Validation here
            return true;
        }

        private void OnUpdateProfessor(Professor professor)
        {
            if (professor == null)
            {
                throw new ArgumentNullException("professor");
            }

            professorRepository.Update(professor);
        }

        private bool CanUpdateProfessor(Professor professor)
        {
            //TODO: Validation here
            return true;
        }

        private void OnRemoveProfessor(Professor professor)
        {
            if (professor == null)
            {
                throw new ArgumentNullException("professor");
            }

            const string message = "Вы действительно хотите удалить преподавателя? Все данные будут утеряны.";
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                professorRepository.Delete(professor.Id);
            }
        }

        #endregion // Class Private Methods
    }
}