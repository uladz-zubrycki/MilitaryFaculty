using System;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom
{
    public abstract class EntityCommandModule<T> : ICommandModule
        where T : class, IUniqueEntity
    {
        private readonly IRepository<T> _repository;

        protected abstract RoutedCommand AddCommand { get; }
        protected abstract RoutedCommand SaveCommand { get; }
        protected abstract RoutedCommand RemoveCommand { get; }

        protected EntityCommandModule(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        public void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand<T>(AddCommand,
                                   OnAddEntity,
                                   CanAddEntity);

            commands.AddCommand<T>(SaveCommand,
                                   OnSaveEntity,
                                   CanSaveEntity);

            commands.AddCommand<T>(RemoveCommand,
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

        private bool CanSaveEntity(T entity)
        {
            return true;
        }

        private void OnSaveEntity(T entity)
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