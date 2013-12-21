using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using MilitaryFaculty.Data.Contract;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class FacultyTreeViewModel : TreeViewModel
    {
        #region Class Fields

        private readonly IRepository<Professor> professorRepository;
        private readonly IRepository<Cathedra> cathedraRepository;

        private ObservableCollection<CathedraTreeItemViewModel> cathedras;

        private string searchString;
        private IEnumerator<ITreeItemViewModel> searchEnumerator;

        #endregion // Class Fields

        #region Class Properties

        public ICommand SearchCommand { get; private set; }

        public override IEnumerable<ITreeItemViewModel> Items
        {
            get { return Cathedras; }
        }

        public ObservableCollection<CathedraTreeItemViewModel> Cathedras
        {
            get
            {
                if (cathedras == null)
                {
                    InitCathedras();
                }

                return cathedras;
            }
        }

        public string SearchString
        {
            get { return searchString; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var withoutSpaces = value.MergeSpaces();
                withoutSpaces = withoutSpaces.TrimStart();

                if (withoutSpaces == searchString)
                {
                    return;
                }

                searchString = withoutSpaces;
                OnPropertyChanged();

                searchEnumerator = null;
            }
        }

        #endregion // Class Properties

        #region Class Constructors

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

            this.professorRepository = professorRepository;
            this.cathedraRepository = cathedraRepository;

            cathedraRepository.EntityCreated += OnCathedraCreated;
            cathedraRepository.EntityDeleted += OnCathedraDeleted;

            SearchCommand = new Command(OnSearch, CanSearch);
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void OnSearch()
        {
            if (searchEnumerator == null || !searchEnumerator.MoveNext())
            {
                searchEnumerator = GetSearchEnumerator();
            }

            var cur = searchEnumerator.Current;
            
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

        #endregion // Class Protected Methods

        #region Class Private Methods

        private void InitCathedras()
        {
            var converter = CathedraTreeItemViewModel.FromModel(this, professorRepository);

            var items = cathedraRepository.Table
                                          .Select(converter)
                                          .ToList();

            cathedras = new ObservableCollection<CathedraTreeItemViewModel>(items);
        }

        private Func<ITreeItemViewModel, bool> GetSearchCriteria()
        {
            Func<ITreeItemViewModel, bool> criteria = (item) =>
            {
                var title = item.Title;
                var match = Regex.Match(title, SearchString, RegexOptions.IgnoreCase);

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
            var converter = CathedraTreeItemViewModel.FromModel(this, professorRepository);
            
            Cathedras.Add(converter(cathedra));
        }

        private void OnCathedraDeleted(object sender, ModifiedEntityEventArgs<Cathedra> e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            var cathedra = e.ModifiedEntity;
            cathedras.RemoveSingle(c => c.Model.Equals(cathedra));
        }

        #endregion // Class Private Methods
    }
}