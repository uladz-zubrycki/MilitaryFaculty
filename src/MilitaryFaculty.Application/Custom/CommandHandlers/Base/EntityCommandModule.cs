﻿using System;
using System.Windows;
using System.Windows.Input;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain.Base;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom.CommandHandlers
{
    public abstract class EntityCommandModule<T> : ICommandModule
        where T : class, IUniqueEntity
    {
        private readonly IRepository<T> _repository;

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
            return true;
        }

        protected virtual bool CanSaveEntity(T entity)
        {
            return true;
        }

        protected virtual bool CanRemoveEntity(T entity)
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
            GlobalCommands.BrowseBack.Execute(null, null);
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

            var message = GetRemovalMessage(entity);
            const string title = "Подтверждение удаления";

            var userInput = MessageBox.Show(message, title, MessageBoxButton.OKCancel);

            if (userInput != MessageBoxResult.Cancel)
            {
                _repository.Delete(entity.Id);
            }
        }
    }
}