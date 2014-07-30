using System;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public abstract class EntityCommandModule<T> : ICommandModule
        where T : class, IUniqueEntity
    {
        protected readonly IRepository<T> Repository;

        protected EntityCommandModule(IRepository<T> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("_scienceRankRepository");
            }

            Repository = repository;
        }

        public void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("commands");
            }

            if (SupportsAddCommand)
            {
                commands.AddCommand<T>(GetAddCommand(), OnAddEntity, CanAddEntity);
            }

            if (SupportsSaveCommand)
            {
                commands.AddCommand<T>(GetSaveCommand(), OnSaveEntity, CanSaveEntity);
            }

            if (SupportsRemoveCommand)
            {
                commands.AddCommand<T>(GetRemoveCommand(), OnRemoveEntity, CanRemoveEntity);
            }
        }

        protected virtual bool SupportsAddCommand { get { return true; } }
        protected virtual bool SupportsSaveCommand { get { return true; } }
        protected virtual bool SupportsRemoveCommand { get { return true; } }

        protected abstract string GetRemovalMessage(T entity);

        protected virtual RoutedCommand GetAddCommand()
        {
            return GlobalCommands.Add<T>();
        }

        protected virtual RoutedCommand GetSaveCommand()
        {
            return GlobalCommands.Save<T>();
        }

        protected virtual RoutedCommand GetRemoveCommand()
        {
            return GlobalCommands.Remove<T>();
        }

        protected virtual bool CanAddEntity(T entity)
        {
            //TODO add validation
            return true;
        }

        protected virtual bool CanSaveEntity(T entity)
        {
            //TODO add validation
            return true;
        }

        protected virtual bool CanRemoveEntity(T entity)
        {
            //TODO add validation
            return true;
        }

        protected virtual void AddEntity(T entity)
        {
            //TODO add normal validation
            try
            {
                Repository.Create(entity);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка. Проверьте введённые данные.", "Ошибка");
            }
        }

        protected virtual void SaveEntity(T entity)
        {
            //TODO add normal validation
            try
            {
                Repository.Update(entity);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка. Проверьте введённые данные.", "Ошибка");
            }
        }

        protected virtual void RemoveEntity(T entity)
        {
            //TODO add normal validation
            try
            {
                Repository.Delete(entity.Id);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка. Проверьте введённые данные.", "Ошибка");
            }
        }

        private void OnAddEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            AddEntity(entity);
            GlobalCommands.BrowseBack.Execute(null, null);
        }

        private void OnSaveEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            SaveEntity(entity);
        }

        private void OnRemoveEntity(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            const string title = "Подтверждение удаления";
            var message = GetRemovalMessage(entity);

            var userInput = MessageBox.Show(message,
                                            title,
                                            MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                RemoveEntity(entity);
            }
        }
    }
}