using System.Collections.Generic;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.Menu;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class CathedraView
    {
        internal class Root : EntityRootViewModel<Cathedra>
        {
            public Root(Cathedra model) : base(model)
            {
                HeaderViewModel = new Header(model);
            }

            protected override IEnumerable<ViewModel<Cathedra>> GetViewModels()
            {
                return new[]
                       {
                           new MainInfo(Model)
                       };
            }
        }

        internal class Header: ViewModel<Cathedra>
        {
            public Header(Cathedra model) : base(model)
            {
                CathedraMenu = CreateCathedraMenu();
            }

            public MenuViewModel CathedraMenu { get; private set; }

            private MenuViewModel CreateCathedraMenu()
            {
                var menuItems =
                    new[]
                    {
                        new MenuItemViewModel("Создать отчёт", GlobalCommands.GenerateReport, Model),
                    };

                var menu = new MenuViewModel(menuItems);

                return menu;

            }
        }

        internal class MainInfo : ViewModel<Cathedra>
        {
            public MainInfo(Cathedra model): base(model) { }

            public override string Title
            {
                get { return Model.Name; }
            }
        }
    }
}