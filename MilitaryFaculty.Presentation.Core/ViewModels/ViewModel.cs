using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Core.ViewBehaviours;

namespace MilitaryFaculty.Presentation.Core.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private object _tag;

        public virtual string Title { get; protected set; }
        public ObservableCollection<CommandViewModel> Commands { get; private set; }

        public virtual object Tag
        {
            get { return _tag; }
            set
            {
                SetValue(() => _tag, null); // just some hack, for DataTrigger
                SetValue(() => _tag, value);
            }
        }

        protected ViewModel()
        {
            Commands = new ObservableCollection<CommandViewModel>();
            InjectBehaviours();
        }

        private void InjectBehaviours()
        {
            var behaviours = GetBehaviours();
            behaviours.ForEach(b => b.Inject(this));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual IEnumerable<IViewBehaviour> GetBehaviours()
        {
            return Enumerable.Empty<IViewBehaviour>();
        }

        protected bool SetValue<TField>(Expression<Func<TField>> evaluator, TField value,
                                        [CallerMemberName] string propertyName = null)
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