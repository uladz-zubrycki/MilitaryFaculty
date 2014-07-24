using System;
using MilitaryFaculty.Application.ViewModels;
using MilitaryFaculty.Data;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Commands;

namespace MilitaryFaculty.Application.Handlers
{
    public class EfficiencyProposalHandlers : EntityCommandModule<EfficiencyProposal>
    {
        public EfficiencyProposalHandlers(IRepository<EfficiencyProposal> repository)
            : base(repository)
        {
            // Empty
        }

        protected override string GetRemovalMessage(EfficiencyProposal publication)
        {
            return ("Вы действительно хотите удалить рационализаторское предложение? " +
                    "Все данные будут утеряны.");
        }
    }

    public class EfficiencyProposalNavigation : NavigationCommandModule
    {
        public EfficiencyProposalNavigation(MainViewModel viewModel)
            : base(viewModel)
        {
            // Empty
        }

        public override void LoadModule(RoutedCommands commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException("sink");
            }

            commands.AddCommand<Professor>(GlobalCommands.BrowseAdd<EfficiencyProposal>(),
                                           OnBrowseEfficiencyProposalAdd);

            commands.AddCommand<EfficiencyProposal>(GlobalCommands.BrowseDetails<EfficiencyProposal>(),
                                                    OnBrowseEfficiencyProposalDetails);
        }

        private void OnBrowseEfficiencyProposalAdd(Professor author)
        {
            if (author == null)
            {
                throw new ArgumentNullException("author");
            }

            var model = new EfficiencyProposal { Author = author };
            ViewModel.WorkWindow = new EfficiencyProposalView.Add(model);
        }

        private void OnBrowseEfficiencyProposalDetails(EfficiencyProposal efficiencyProposal)
        {
            if (efficiencyProposal == null)
            {
                throw new ArgumentNullException("efficiencyProposal");
            }

            ViewModel.WorkWindow = new EfficiencyProposalView.Root(efficiencyProposal);
        }
    }
}