using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.Menu;

namespace MilitaryFaculty.Application.ViewModels
{
    internal static class CathedraView
    {
        internal class Root : EntityRootViewModel<Cathedra>
        {
            private readonly IRepository<ScienceRank> _scienceRankRepository;

            public Root(Cathedra model, IRepository<ScienceRank> scienceRankRepository)
                : base(model)
            {
                if (scienceRankRepository == null)
                {
                    throw new ArgumentNullException("scienceRankRepository");
                }

                _scienceRankRepository = scienceRankRepository;

                HeaderViewModel = new Header(model);
            }

            protected override IEnumerable<ViewModel<Cathedra>> GetViewModels()
            {
                return new ViewModel<Cathedra>[]
                       {
                           new ScienceRanks(Model, _scienceRankRepository),
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

            public override string Title
            {
                get { return Model.Name; }
            }

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

        internal class ScienceRanks : ViewModel<Cathedra>
        {
            private readonly Lazy<ObservableCollection<ScienceRankView.ListItem>> _items;

            public ScienceRanks(Cathedra model, IRepository<ScienceRank> cathedraRankRepository)
                : base(model)
            {
                if (cathedraRankRepository == null)
                {
                    throw new ArgumentNullException("conferenceRepository");
                }

                _items = Lazy.Create(InitializeItems);

                cathedraRankRepository.EntityCreated += OnCathedraRankCreated;
                cathedraRankRepository.EntityDeleted += OnCathedraRankDeleted;

                this.Addable(GlobalCommands.BrowseAdd<ScienceRank>());
            }

            public override string Title
            {
                get { return "Проведение научной работы"; }
            }

            public ObservableCollection<ScienceRankView.ListItem> Items
            {
                get { return _items.Value; }
            }

            private ObservableCollection<ScienceRankView.ListItem> InitializeItems()
            {
                var cathedraRanks = Model.ScienceRanks
                                         .Select(ScienceRankView.ListItem.FromModel)
                                         .ToList();

                var result = new ObservableCollection<ScienceRankView.ListItem>(cathedraRanks);

                return result;
            }

            private void OnCathedraRankCreated(object sender, ModifiedEntityEventArgs<ScienceRank> e)
            {
                var cathedraRank = e.ModifiedEntity;

                if (cathedraRank.Cathedra.Equals(Model))
                {
                    Items.Add(new ScienceRankView.ListItem(cathedraRank));
                }
            }

            private void OnCathedraRankDeleted(object sender, ModifiedEntityEventArgs<ScienceRank> e)
            {
                var cathedraRank = e.ModifiedEntity;

                if (cathedraRank.Cathedra.Equals(Model))
                {
                    Items.RemoveSingle(c => c.Model.Equals(cathedraRank));
                }
            }
        }
    }
}