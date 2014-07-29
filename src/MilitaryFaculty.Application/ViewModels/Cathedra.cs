using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MilitaryFaculty.Application.Custom;
using MilitaryFaculty.Application.ViewModels.Base;
using MilitaryFaculty.Application.Views.Entity;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Attributes;
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
                           new ExtraInfo(Model), 
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

        internal class ExtraInfo : EntityViewModel<Cathedra>
        {
            private readonly ICollection<Person> _persons;  

            public ExtraInfo(Cathedra model) : base(model)
            {
                _persons = model.Professors.ToList();
            }

            public override string Title
            {
                get { return "Информация о кафедре"; }
            }

            [IntProperty(Label = "Количество студентов")]
            public int StudentsCount
            {
                get { return CountOf(p => p.JobPosition < JobPosition.Teacher); }
            }

            [IntProperty(Label = "Количество преподавателей")]
            public int TeachersCount
            {
                get { return CountOf(p => p.JobPosition >= JobPosition.Teacher); }
            }

            [IntProperty(Label = "Количество доцентов")]
            public int DocentsCount
            {
                get { return CountOf(p => p.AcademicDegree >= AcademicDegree.Docent); }
            }

            [IntProperty(Label = "Количество профессоров")]
            public int ProfessorsCount
            {
                get { return CountOf(p => p.AcademicDegree >= AcademicDegree.Professor); }
            }

            [IntProperty(Label = "Количество кандидатов наук")]
            public int CandidatsCount
            {
                get { return CountOf(p => p.AcademicRank >= AcademicRank.CandidateOfScience); }
            }

            [IntProperty(Label = "Количество докторов наук")]
            public int DoctorsCount
            {
                get { return Model.Professors.Count(p => p.AcademicRank >= AcademicRank.DoctorOfScience); }
            }

            private int CountOf(Func<Person, bool> predicate)
            {
                return _persons.Count(predicate);
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
                    throw new ArgumentNullException("cathedraRankRepository");
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

            public int RanksCount { get { return Items.Count; } } 

            private ObservableCollection<ScienceRankView.ListItem> InitializeItems()
            {
                var cathedraRanks = Model.ScienceRanks
                                         .Select(ScienceRankView.ListItem.FromModel)
                                         .ToList();

                var result = new ObservableCollection<ScienceRankView.ListItem>(cathedraRanks);

                result.CollectionChanged += (sender, args) =>  OnPropertyChanged("RanksCount");

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