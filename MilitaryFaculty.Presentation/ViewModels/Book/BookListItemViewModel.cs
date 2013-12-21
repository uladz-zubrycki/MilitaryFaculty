using System;
using System.Globalization;
using MilitaryFaculty.Domain;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Presentation.Custom;
using MilitaryFaculty.Presentation.Infrastructure;

namespace MilitaryFaculty.Presentation.ViewModels
{
    public class BookListItemViewModel : ListItemViewModel<Book>
    {
        #region Type Static Members

        public static Func<Book, BookListItemViewModel> FromModel()
        {
            return book => new BookListItemViewModel(book);
        }  

        #endregion // Type Static Members

        #region Class Properties
        
        public override string PrimaryInfo
        {
            get { return Model.PagesCount.ToString(CultureInfo.InvariantCulture) + "стр."; }
        }

        public override string SecondaryInfo
        {
            get { return Model.Name; }
        }

        #endregion // Class Properties

        #region Class Constructors

        public BookListItemViewModel(Book model)
            : base(model)
        {
            InitCommands();

            TooltipViewModel = new BookExtraInfoViewModel(Model);
        }

        #endregion // Class Constructors

        #region Class Private Methods

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

            return new ImagedCommandViewModel(Do.Book.Remove,
                                              Model, tooltip, imageSource);
        }

        private ImagedCommandViewModel CreateBrowseBookCommand()
        {
            const string tooltip = "Подробно";
            const string imageSource = @"..\..\Content\details.png";

            return new ImagedCommandViewModel(Browse.Book.Details,
                                              Model, tooltip, imageSource);
        }

        #endregion // Class Private Methods
    }
}