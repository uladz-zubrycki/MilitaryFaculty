using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MilitaryFaculty.Presentation.Core.ViewModels
{
    /// <summary>
    ///     Strong-typed base ViewModel class.
    /// </summary>
    /// <typeparam name="T">Contained model type.</typeparam>
    public abstract class ViewModel<T> : ViewModel
        where T : class
    {
        public T Model { get; private set; }

        protected ViewModel()
        {
            // Empty
        }

        protected ViewModel(T model)
            : this()
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;
        }

        protected void SetModelProperty<TProperty>(Expression<Func<T, TProperty>> evaluator, TProperty value)
        {
            if (evaluator == null)
            {
                throw new ArgumentNullException("evaluator");
            }

            var body = (MemberExpression) evaluator.Body;
            var propInfo = (PropertyInfo) body.Member;
            var target = GetMember(body, Model);

            var oldValue = (TProperty) propInfo.GetValue(target);

            if (value.Equals(oldValue))
            {
                return;
            }

            propInfo.SetValue(target, value);
            OnPropertyChanged(propInfo.Name);
        }

        private static object GetMember(MemberExpression expression, T target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            object member = target;

            while (expression.Expression is MemberExpression)
            {
                expression = (MemberExpression) expression.Expression;
                var propInfo = (PropertyInfo) expression.Member;
                member = propInfo.GetValue(member);
            }

            return member;
        }
    }
}