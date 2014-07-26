using System;
using System.Windows.Input;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.Command;

namespace MilitaryFaculty.Presentation.ViewBehaviours
{
    public class HasCommandViewBehaviour<TModel>: IViewBehaviour<TModel> 
        where TModel : class
    {
        private readonly ICommand _command;
        private readonly string _tooltip;
        private readonly string _imageSource;

        public HasCommandViewBehaviour(ICommand command, string tooltip, string imageSource)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (String.IsNullOrEmpty(tooltip))
            {
                throw new ArgumentNullException("tooltip");
            }

            if (String.IsNullOrEmpty(imageSource))
            {
                throw new ArgumentNullException("imageSource");
            }

            _command = command;
            _tooltip = tooltip;
            _imageSource = imageSource;
        }

        public void Inject(ViewModel<TModel> viewModel)
        {
            var commandViewModel = new ImagedCommandViewModel(_command,
                                                              viewModel.Model,
                                                              _tooltip,
                                                              _imageSource);
            viewModel.Commands.Add(commandViewModel);
        }
    }
}
