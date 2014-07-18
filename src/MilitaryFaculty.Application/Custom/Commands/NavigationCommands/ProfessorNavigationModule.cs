using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Custom
{
    public class ProfessorNavigationModule : BaseNavigationModule
    {
        public ProfessorNavigationModule(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            commands.AddCommand<Cathedra>(Browse.ProfessorAdd,
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