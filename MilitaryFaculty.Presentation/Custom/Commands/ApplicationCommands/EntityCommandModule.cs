using System;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain.Contract;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.Custom
{
    public abstract class EntityCommandModule<T> : ICommandContainerModule
        where T : class, IUniqueEntity
    {
        private readonly IRepository<T> _repository;

        protected abstract RoutedCommand AddCommand { get; }
        protected abstract RoutedCommand UpdateCommand { get; }
        protected abstract RoutedCommand RemoveCommand { get; }

        protected EntityCommandModule(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

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

        protected abstract string GetRemovalMessage();

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

            _repository.Create(entity);
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

            _repository.Update(entity);
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
                _repository.Delete(entity.Id);
            }
        }
    }
}