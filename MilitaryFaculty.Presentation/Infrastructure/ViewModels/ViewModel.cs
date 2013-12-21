using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        #region Class Fields

        private object tag;

        #endregion // Class Fields

        #region Class Properties

        public virtual string Title { get; protected set; }
        public ObservableCollection<CommandViewModel> Commands { get; private set; }

        public virtual object Tag
        {
            get { return tag; }
            set
            {
                SetValue(() => tag, null); // just some hack, for DataTrigger
                SetValue(() => tag, value);
            }
        }

        #endregion // Class Properties

        #region Class Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // Class Events

        #region Class Constructors

        protected ViewModel()
        {
            Commands = new ObservableCollection<CommandViewModel>();
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected bool SetValue<TField>(Expression<Func<TField>> evaluator, TField value, [CallerMemberName] string propertyName = null)
        {
            if (evaluator == null)
            {
                throw new ArgumentNullException("evaluator");
            }

            if (String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            var body = (MemberExpression)evaluator.Body;
            var fieldInfo = (FieldInfo)body.Member;

            var oldValue = (TField)fieldInfo.GetValue(this);

            if (value == null || !value.Equals(oldValue))
            {
                fieldInfo.SetValue(this, value);
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

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