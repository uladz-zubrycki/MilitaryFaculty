using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Presentation.ViewModels;
using MilitaryFaculty.Presentation.Widgets.TreeView.Events;

namespace MilitaryFaculty.Presentation.Widgets.TreeView
{
    public abstract class TreeViewModel : ViewModel, ITreeViewModel
    {
        private ITreeItemViewModel _selected;

        public abstract IEnumerable<ITreeItemViewModel> Items { get; }

        public ITreeItemViewModel Selected
        {
            get { return _selected; }
            set
            {
                var oldValue = _selected;

                if (SetValue(() => _selected, value))
                {
                    OnSelectedItemChanged(_selected, oldValue);
                }
            }
        }

        public event EventHandler<SelectedChangedEventArgs> SelectedItemChanged;

        public IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return Items.SelectMany(item => item.Find(predicate));
        }

        protected void OnSelectedItemChanged(ITreeItemViewModel newValue, ITreeItemViewModel oldValue)
        {
            var handler = SelectedItemChanged;

            if (handler != null)
            {
                handler(null, new SelectedChangedEventArgs(newValue, oldValue));
            }
        }
    }
}