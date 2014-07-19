using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MilitaryFaculty.Common;
using MilitaryFaculty.Data;
using MilitaryFaculty.Data.Events;
using MilitaryFaculty.Presentation.Commands;
using MilitaryFaculty.Presentation.Widgets.TreeView;

namespace MilitaryFaculty.Application.ViewModels
{
    public class FacultyTreeViewModel : TreeViewModel
    {
        private readonly IRepository<Domain.Cathedra> _cathedraRepository;
        private readonly IRepository<Domain.Professor> _professorRepository;
        private readonly Lazy<ObservableCollection<CathedraTreeItemViewModel>> _cathedras;

        private IEnumerator<ITreeItemViewModel> _searchEnumerator;
        private string _searchString;

        public ICommand SearchCommand { get; private set; }

        public ObservableCollection<CathedraTreeItemViewModel> Cathedras
        {
            get { return _cathedras.Value; }
        }

        public override IEnumerable<ITreeItemViewModel> Items
        {
            get { return Cathedras; }
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

        public FacultyTreeViewModel(IRepository<Domain.Professor> professorRepository,
                                    IRepository<Domain.Cathedra> cathedraRepository)
        {
            if (professorRepository == null)
            {
                throw new ArgumentNullException("professorRepository");
            }

            if (cathedraRepository == null)
            {
                throw new ArgumentNullException("cathedraRepository");
            }

            _cathedras = Lazy.Create(CreateCathedrasViewModel);

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

        private ObservableCollection<CathedraTreeItemViewModel> CreateCathedrasViewModel()
        {
            var convert = CathedraTreeItemViewModel.FromModel(this, _professorRepository);
            var cathedras = _cathedraRepository.Table.Select(convert);

            var result = new ObservableCollection<CathedraTreeItemViewModel>(cathedras);

            return result;
        }

        private Func<ITreeItemViewModel, bool> GetSearchCriteria()
        {
            Func<ITreeItemViewModel, bool> criteria =
                item =>
                {
                    var title = item.Title;
                    var match = Regex.Match(title, SearchString,
                                            RegexOptions.IgnoreCase);

                    return match.Success;
                };

            return criteria;
        }

        private void OnCathedraCreated(object sender, ModifiedEntityEventArgs<Domain.Cathedra> e)
        {
            var cathedra = e.ModifiedEntity;
            var cathedraViewModel = CathedraTreeItemViewModel.FromModel(
                model: cathedra,
                owner: this,
                professorRepository: _professorRepository);

            Cathedras.Add(cathedraViewModel);
        }

        private void OnCathedraDeleted(object sender, ModifiedEntityEventArgs<Domain.Cathedra> e)
        {
            var cathedra = e.ModifiedEntity;
            Cathedras.RemoveSingle(c => c.Model.Equals(cathedra));
        }
    }
}