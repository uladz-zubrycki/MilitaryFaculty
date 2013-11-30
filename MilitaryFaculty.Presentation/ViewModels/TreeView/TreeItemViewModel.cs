using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class TreeItemViewModel<T> : ViewModel<T>, ITreeItemViewModel
        where T : class
    {
        #region Class Static Fields

        /// <summary>
        /// Dummy child for lazy children loading.
        /// </summary>
        private static readonly ITreeItemViewModel FakeChild = new TreeItemViewModel<object>();

        #endregion // Class Static Fields

        #region Class Fields

        private bool isSelected;
        private bool isExpanded;
        protected readonly TreeViewModel Owner;

        #endregion // Class Fields

        #region Class Properties

        public ITreeItemViewModel Parent { get; private set; }
        public ObservableCollection<ITreeItemViewModel> Children { get; private set; }

        object ITreeItemViewModel.Model
        {
            get { return Model; }
        }

        private bool ChildrenLoaded
        {
            get { return !(Children.Contains(FakeChild)); }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                SetValue(() => this.isSelected, value);
                Owner.Selected = this;

                if (isSelected && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                SetValue(() => this.isExpanded, value);

                if (!ChildrenLoaded)
                {
                    InitChildren();
                }

                if (isExpanded && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

        #endregion // Class Properties

        #region Class Constructors

        private TreeItemViewModel()
        {
            Owner = null;
            Parent = null;
        }

        protected TreeItemViewModel(T model, TreeViewModel owner,
                                    ITreeItemViewModel parent, bool isLazyLoadingEnabled)
            : base(model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            this.Owner = owner;
            this.Parent = parent;

            Children = new ObservableCollection<ITreeItemViewModel>();

            if (isLazyLoadingEnabled)
            {
                Children.Add(FakeChild);
            }
        }

        #endregion // Class Constructors

        #region Class Public methods

        public IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            if (predicate(this))
            {
                yield return this;
            }

            if (!ChildrenLoaded)
            {
                InitChildren();
            }

            foreach (var child in Children)
            {
                foreach (var match in child.Find(predicate))
                {
                    yield return match;
                }
            }
        }

        #endregion // Class Public methods

        #region Class Protected methods

        protected virtual IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            throw new NotSupportedException();
        }

        #endregion // Class Protected methods

        #region Class Private methods

        private void InitChildren()
        {
            Children.Clear();
            Children.AddRange(LoadChildren());
        }

        #endregion //Class Private methods
    }
}