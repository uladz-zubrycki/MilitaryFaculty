using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.Commands;
using MilitaryFaculty.Presentation.Core.Widgets.TreeView;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class FacultyTreeViewModel : TreeViewModel
    {
        private readonly IRepository<Cathedra> _cathedraRepository;
        private readonly IRepository<Professor> _professorRepository;

        private ObservableCollection<CathedraTreeItemViewModel> _cathedras;

        private IEnumerator<ITreeItemViewModel> _searchEnumerator;
        private string _searchString;

        public ICommand SearchCommand { get; private set; }

        public override IEnumerable<ITreeItemViewModel> Items
        {
            get { return Cathedras; }
        }

        public ObservableCollection<CathedraTreeItemViewModel> Cathedras
        {
            get
            {
                if (_cathedras == null)
                {
                    InitCathedras();
                }

                return _cathedras;
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var withoutSpaces = value.MergeSpaces();
                withoutSpaces = withoutSpaces.TrimStart();

                if (withoutSpaces == _searchString)
                {
                    return;
                }

                _searchString = withoutSpaces;
                OnPropertyChanged("SearchString");

                _searchEnumerator = null;
            }
        }

        public FacultyTreeViewModel(IRepository<Professor> professorRepository,
                                    IRepository<Cathedra> cathedraRepository)
        {
            if (professorRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            if (cathedraRepository == null)
            {
                throw new ArgumentNullException("cathedraRepository");
            }

            _professorRepository = professorRepository;
            _cathedraRepository = cathedraRepository;

            cathedraRepository.EntityCreated += OnCathedraCreated;
            cathedraRepository.EntityDeleted += OnCathedraDeleted;

            SearchCommand = new Command(OnSearch, CanSearch);
        }

        protected void OnSearch()
        {
            if (_searchEnumerator == null || !_searchEnumerator.MoveNext())
            {
                _searchEnumerator = GetSearchEnumerator();
            }

            var cur = _searchEnumerator.Current;

            if (cur != null)
            {
                cur.IsSelected = true;
            }
        }

        protected bool CanSearch()
        {
            return SearchString != null && !String.IsNullOrWhiteSpace(SearchString);
        }

        protected IEnumerator<ITreeItemViewModel> GetSearchEnumerator()
        {
            var searchCriteria = GetSearchCriteria();

            var enumerator = Find(searchCriteria).GetEnumerator();
            enumerator.MoveNext();

            return enumerator;
        }

        private void InitCathedras()
        {
            var converter = CathedraTreeItemViewModel.FromModel(this,
                _professorRepository);

            var items = _cathedraRepository.Table
                                           .Select(converter)
                                           .ToList();

            _cathedras = new ObservableCollection<CathedraTreeItemViewModel>(items);
        }

        private Func<ITreeItemViewModel, bool> GetSearchCriteria()
        {
            Func<ITreeItemViewModel, bool> criteria = item =>
                                                      {
                                                          var title = item.Title;
                                                          var match = Regex.Match(title, SearchString,
                                                              RegexOptions.IgnoreCase);

                                                          return match.Success;
                                                      };

            return criteria;
        }

        private void OnCathedraCreated(object sender, ModifiedEntityEventArgs<Cathedra> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var cathedra = e.ModifiedEntity;
            var converter = CathedraTreeItemViewModel.FromModel(this,
                _professorRepository);

            Cathedras.Add(converter(cathedra));
        }

        private void OnCathedraDeleted(object sender, ModifiedEntityEventArgs<Cathedra> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var cathedra = e.ModifiedEntity;
            _cathedras.RemoveSingle(c => c.Model.Equals(cathedra));
        }
    }
}