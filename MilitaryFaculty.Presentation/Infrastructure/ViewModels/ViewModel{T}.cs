using System;

namespace MilitaryFaculty.Presentation.Infrastructure
{
    /// <summary>
    /// Strong-typed base ViewModel class.
    /// </summary>
    /// <typeparam name="T">Contained model type.</typeparam>
    public abstract class ViewModel<T> : ViewModel
        where T : class
    {
        #region Class Properties

        public T Model { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        protected ViewModel()
        {
            // Empty
        }

        protected ViewModel(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            Model = model;
        }

        #endregion // Class Constructors
    }
}