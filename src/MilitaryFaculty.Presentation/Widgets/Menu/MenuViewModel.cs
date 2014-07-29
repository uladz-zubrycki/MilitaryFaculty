using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MilitaryFaculty.Presentation.Annotations;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.ViewModels;

namespace MilitaryFaculty.Presentation.Widgets.Menu
{
    public class MenuViewModel: ViewModel
    {
        [UsedImplicitly] private bool _isOpen;

        public MenuViewModel(IEnumerable<MenuItemViewModel> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            Items = items.ToList();
            ShowCommand = new SimpleCommand(Show);
        }

        public ICommand ShowCommand { get; set; }

        public IList<MenuItemViewModel> Items { get; private set; }

        public bool IsOpen
        {
            get { return _isOpen; }
            set { SetValue("IsOpen", () => _isOpen, value); }
        }

        private void Show()
        {
            IsOpen = true;
        }
    }
}
