using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Custom
{
    public class ProfessorNavigationModule : BaseNavigationModule
    {
        public ProfessorNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void RegisterModule(CommandContainer container)
        {
            container.RegisterCommand<Cathedra>(Browse.Professor.Add,
                                                OnBrowseProfessorAdd);
        }

        private void OnBrowseProfessorAdd(Cathedra cathedra)
        {
            if (cathedra == null)
            {
                throw new ArgumentNullException("cathedra");
            }

            var model = new Professor {Cathedra = cathedra};
            ViewModel.WorkWindow = new AddProfessorViewModel(model);
        }
    }
}