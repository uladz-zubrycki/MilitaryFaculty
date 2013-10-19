﻿using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Infrastructure;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorNavCommandsModule : BaseNavCommandsModule
    {
        #region Class Constructors

        public ProfessorNavCommandsModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public override void RegisterModule(CommandContainer container)
        {
            container.RegisterCommand<Cathedra>(GlobalNavCommands.BrowseProfessorAdd,
                                                OnBrowseProfessorAdd);
        }

        #endregion // Class Public Methods

        #region Class Private Methods

        private void OnBrowseProfessorAdd(Cathedra cathedra)
        {
            if (cathedra == null)
            {
                throw new ArgumentNullException("cathedra");
            }

            var model = new Professor
                        {
                            Cathedra = cathedra,
                        };

            ViewModel.WorkWindow = new ProfessorAddViewModel(model);
        }

        #endregion // Class Private Methods
    }
}