using System;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public abstract class BaseCommandModule<T> : ICommandContainerModule
        where T : class, IUniqueEntity
    {
        #region Class Fields
        
        private readonly IRepository<T> repository;

        #endregion // Class Fields

        #region Class Properties

        protected abstract RoutedCommand AddCommand { get; }
        protected abstract RoutedCommand UpdateCommand { get; }
        protected abstract RoutedCommand RemoveCommand { get; }

        #endregion // Class Properties

        #region Class Constructors
        
        protected BaseCommandModule(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.repository = repository;
        }
        #endregion // Class Constructors


        #region Class Public Methods

        public void RegisterModule(CommandContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("sink");
            }

            container.RegisterCommand<T>(AddCommand,
                                         OnAddEntity,
                                         CanAddEntity);

            container.RegisterCommand<T>(UpdateCommand,
                                         OnUpdateEntity,
                                         CanUpdateEntity);

            container.RegisterCommand<T>(RemoveCommand,
                                         OnRemoveEntity);
        }

        #endregion // Class Public Methods

        #region Class Protected Methods

        protected abstract string GetRemovalMessage();

        #endregion // Class Protected Methods

        #region Class Private Methods

        private bool CanAddEntity(T entity)
        {
            return true;
        }

        private void OnAddEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Create(entity);
            Browse.Back.Execute(null, null);
        }

        private bool CanUpdateEntity(T entity)
        {
            return true;
        }

        private void OnUpdateEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            repository.Update(entity);
        }

        private void OnRemoveEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var message = GetRemovalMessage();
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                repository.Delete(entity.Id);
            }
        }

        #endregion // Class Private Methods
    }
}