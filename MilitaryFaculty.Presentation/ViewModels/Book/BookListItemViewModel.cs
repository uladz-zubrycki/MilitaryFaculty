using System;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookListItemViewModel : ViewModel<Book>
    {
        #region Type Static Members

        public static Func<Book, BookListItemViewModel> FromModel()
        {
            return book => new BookListItemViewModel(book);
        }  

        #endregion // Type Static Members

        #region Class Properties

        public BookInfoViewModel InfoViewModel { get; private set; }
        public BookExtraInfoViewModel ExtraInfoViewModel { get; private set; }

        #endregion // Class Properties

        #region Class Constructors

        public BookListItemViewModel(Book model)
            : base(model)
        {
            InitViewModels();
            InitCommands();
        }

        #endregion // Class Constructors

        #region Class Private Methods

        private void InitViewModels()
        {
            InfoViewModel = new BookInfoViewModel(Model);
            ExtraInfoViewModel = new BookExtraInfoViewModel(Model);
        }

        private void InitCommands()
        {
            Commands.AddRange(new[]
                              {
                                  CreateBrowseBookCommand(),
                                  CreateRemoveBookCommand(),
                              });
        }

        private ImagedCommandViewModel CreateRemoveBookCommand()
        {
            const string tooltip = "Удалить книгу";
            const string imageSource = @"..\Content\remove.png";

            return new ImagedCommandViewModel(GlobalAppCommands.RemoveBook,
                                              Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseBookCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(GlobalNavCommands.BrowseBookDetails,
                                              Model, tooltip, imageSource);
        }

        #endregion // Class Private Methods
    }
}