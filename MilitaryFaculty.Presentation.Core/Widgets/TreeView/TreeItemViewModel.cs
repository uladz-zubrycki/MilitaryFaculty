using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewModels;

namespace MilitaryFaculty.Presentation.Core.Widgets.TreeView
{
    public class TreeItemViewModel<T> : ViewModel<T>, ITreeItemViewModel
        where T : class
    {
        /// <summary>
        ///     Dummy child for lazy children loading.
        /// </summary>
        private static readonly ITreeItemViewModel FakeChild = new TreeItemViewModel<object>();

        // ReSharper disable UnassignedField.Compiler
        protected readonly TreeViewModel Owner;
        private bool _isExpanded; // accessed via SetValue method
        private bool _isSelected; // accessed via SetValue method
        // ReSharper restore UnassignedField.Compiler

        private bool ChildrenLoaded
        {
            get { return !(Children.Contains(FakeChild)); }
        }

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

            Owner = owner;
            Parent = parent;

            Children = new ObservableCollection<ITreeItemViewModel>();

            if (isLazyLoadingEnabled)
            {
                Children.Add(FakeChild);
            }
        }

        public ITreeItemViewModel Parent { get; private set; }
        public ObservableCollection<ITreeItemViewModel> Children { get; private set; }

        object ITreeItemViewModel.Model
        {
            get { return Model; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetValue(() => _isSelected, value);
                Owner.Selected = this;

                if (_isSelected && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                SetValue(() => _isExpanded, value);

                if (!ChildrenLoaded)
                {
                    InitChildren();
                }

                if (_isExpanded && Parent != null)
                {
                    Parent.IsExpanded = true;
                }
            }
        }

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

        protected virtual IEnumerable<ITreeItemViewModel> LoadChildren()
        {
            throw new NotSupportedException();
        }

        private void InitChildren()
        {
            Children.Clear();
            Children.AddRange(LoadChildren());
        }
    }
}