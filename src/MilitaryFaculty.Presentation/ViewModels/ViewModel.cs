using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using MilitaryFaculty.Presentation.Annotations;
using MilitaryFaculty.Presentation.Widgets.Command;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        [UsedImplicitly] private object _tag;

        public ObservableCollection<CommandViewModel> Commands { get; private set; }
        public virtual string Title { get; protected set; }

        protected ViewModel()
        {
            Commands = new ObservableCollection<CommandViewModel>();
        }

        public virtual object Tag
        {
            get { return _tag; }
            set
            {
                SetValue("Tag", () => _tag, null); // just some hack, for DataTrigger
                SetValue("Tag", () => _tag, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // TODO should use lambda in another way. no string property names
        protected bool SetValue<TField>(string propertyName,
                                        Expression<Func<TField>> evaluator,
                                        TField value)
        {
            if (evaluator == null)
            {
                throw new ArgumentNullException("evaluator");
            }

            if (String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }

            var body = (MemberExpression) evaluator.Body;
            var fieldInfo = (FieldInfo) body.Member;
            var oldValue = (TField) fieldInfo.GetValue(this);

            if (!Equals(value, oldValue))
            {
                fieldInfo.SetValue(this, value);
                OnPropertyChanged(propertyName);

                return true;
            }

            return false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}