using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookViewModel : ComplexViewModel<Book>
    {
        #region Class Properties

        public BookInfoViewModel InfoViewModel { get; private set; }
        public BookExtraInfoViewModel ExtraInfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public BookViewModel(Book model)
            : this(model, EditViewMode.Display)
        {
            // Empty
        }

        public BookViewModel(Book model, EditViewMode mode)
            : base(model)
        {
            const string title = "Информация об учебнике";

            Title = title;
            InitViewModels(mode);
        }

        #endregion // Class Constructors

        #region Class Protected Methods

        protected void InitViewModels(EditViewMode mode)
        {
            InfoViewModel = new BookInfoViewModel(Model, mode);
            ExtraInfoViewModel = new BookExtraInfoViewModel(Model, mode);

            ViewModels.Add(InfoViewModel);
            ViewModels.Add(ExtraInfoViewModel);
        }

        #endregion // Class Protected Methods
    }
}