using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public class ViewModel : INotifyPropertyChanged
    {
        #region Class Fields

        private object tag;

        #endregion // Class Fields

        #region Class Properties

        public string Title { get; protected set; }
        public ObservableCollection<CommandViewModel> Commands { get; private set; }

        public virtual object Tag
        {
            get { return tag; }
            set
            {
                if (value == Tag)
                {
                    return;
                }

                tag = value;
        
                OnPropertyChanged();
            }
        }

        #endregion // Class Properties

        #region Class Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // Class Events

        #region Class Constructors

        public ViewModel()
        {
            Commands = new ObservableCollection<CommandViewModel>();
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // Class Protected Methods
    }
}