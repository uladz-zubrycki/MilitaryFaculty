using System;
using System.Windows;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorApplicationCommands : ICommandContainerModule
    {
        #region Class Fields

        private readonly IRepository<Professor> professorRepository;

        #endregion // Class Fields

        #region Class Constructors

        public ProfessorApplicationCommands(IRepository<Professor> professorRepository)
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
            container.RegisterCommand<Professor>(ApplicationCommands.AddProfessor,
                                                 OnAddProfessor,
                                                 CanAddProfessor);

            container.RegisterCommand<Professor>(ApplicationCommands.UpdateProfessor,
                                                 OnUpdateProfessor,
                                                 CanUpdateProfessor);

            container.RegisterCommand<Professor>(ApplicationCommands.RemoveProfessor,
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
            NavigationCommands.BrowseBack.Execute(null, null);
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