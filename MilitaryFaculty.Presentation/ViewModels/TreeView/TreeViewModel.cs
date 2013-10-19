using System;
using System.Collections.Generic;
using System.Linq;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class TreeViewModel : ViewModel, ITreeViewModel
    {
        #region Class Fields

        private ITreeItemViewModel selected;

        #endregion // Class Fields

        #region Class Properties

        public abstract IEnumerable<ITreeItemViewModel> Items { get; }

        public ITreeItemViewModel Selected
        {
            get { return selected; }
            set
            {
                if (value == Selected)
                {
                    return;
                }

                var oldValue = selected;
                selected = value;

                OnPropertyChanged();
                OnSelectedItemChanged(selected, oldValue);
            }
        }

        #endregion // Class Properties

        #region Class Events

        public event EventHandler<SelectedChangedEventArgs> SelectedItemChanged;

        #endregion // Class Events

        #region Class Protected Methods

        protected void OnSelectedItemChanged(ITreeItemViewModel newValue, ITreeItemViewModel oldValue)
        {
            var handler = SelectedItemChanged;

            if (handler != null)
            {
                handler(null, new SelectedChangedEventArgs(newValue, oldValue));
            }
        }

        #endregion // Class Protected Methods

        #region Class Public Methods

        public IEnumerable<ITreeItemViewModel> Find(Func<ITreeItemViewModel, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return Items.SelectMany(item => item.Find(predicate));
        }

        #endregion // Class Public Methods
    }
}